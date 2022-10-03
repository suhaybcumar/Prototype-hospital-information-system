using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PHIMS.Data;
using PHIMS.Models;

namespace PHIMS.Controllers
{
    [Authorize(Roles ="Admin,Super Admin")]
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        // GET: Doctors
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.Doctor.Include(d => d.Admin).Where(p => p.Admin_Id == HttpContext.Session.GetInt32("test"));
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Doctor.Include(d => d.Admin);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: Doctors/Details/5
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doctor == null)
            {
                return NotFound();
            }
          
            var doctor = await _context.Doctor
                .Include(d => d.Admin)
                .FirstOrDefaultAsync(m => m.Doctor_Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Name");
            ViewData["Admin_Id"] = new SelectList(_context.Admin, "Admin_Id", "F_Name");
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Doctor_Id,F_Name,M_Name,L_Name,Phone_Number,Email,DOB,R_Date,Gender,Specializations,Working_days,Working_hours,National_Id,Employee_Id,Department,Available,Admin_Id,UserId")] Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                var check = _context.Admin.FirstOrDefault(u => u.UserId == doctor.UserId);
                var check1 = _context.Doctor.FirstOrDefault(u => u.UserId == doctor.UserId);
                var check2 = _context.User.FirstOrDefault(u => u.UserId == doctor.UserId);
                if (check != null || check1 != null || check2 != null)
                {
                    TempData[SD.Error] = "This Account Already Assing to another Dector";
                    return RedirectToAction(nameof(Index));
                }
                doctor.Admin_Id = (int)HttpContext.Session.GetInt32("test");
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                TempData[SD.Success] = "Doctor Register Successfully";
                TempData[SD.Type] = "Doctor";
                return RedirectToAction(nameof(Index));
            }
            ViewData["Admin_Id"] = new SelectList(_context.Admin, "Admin_Id", "F_Name", doctor.Admin_Id);
            TempData[SD.Error] = "Something Went Wrong";
            TempData[SD.Type] = "Doctor";
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewData["Admin_Id"] = new SelectList(_context.Admin, "Admin_Id", "F_Name", doctor.Admin_Id);
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Doctor_Id,F_Name,M_Name,L_Name,Phone_Number,Email,DOB,R_Date,Gender,Specializations,Working_days,Working_hours,National_Id,Employee_Id,Department,Available,Admin_Id,,UserId")] Doctor doctor)
        {
            if (id != doctor.Doctor_Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    doctor.Admin_Id = (int)HttpContext.Session.GetInt32("test");
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Updated Successfully";
                    TempData[SD.Type] = "Doctor";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Doctor_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            TempData[SD.Error] = "Something Went Wrong";
            TempData[SD.Type] = "Doctor";
            ViewData["Admin_Id"] = new SelectList(_context.Admin, "Admin_Id", "F_Name", doctor.Admin_Id);
            return View(doctor);
        }

        [Authorize(Roles = "Admin")]
        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doctor == null)
            {
                return NotFound();
            }
            var check = _context.Pateint.FirstOrDefault(u => u.Doctr_Id == id);
            if (check != null)
            {
                TempData[SD.Error] = "This Doctor Can't Deleted";
                return RedirectToAction("Index");
            }
            var doctor = await _context.Doctor
                .Include(d => d.Admin)
                .FirstOrDefaultAsync(m => m.Doctor_Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }
        [Authorize(Roles = "Admin")]
        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doctor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Doctor'  is null.");
            }
            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctor.Remove(doctor);
            }
            
            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Doctor Deleted Seccfully";
            TempData[SD.Type] = "Doctor";
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
          return (_context.Doctor?.Any(e => e.Doctor_Id == id)).GetValueOrDefault();
        }
    }
}
