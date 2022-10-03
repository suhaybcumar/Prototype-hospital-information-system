using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using PHIMS.Data;
using PHIMS.Models;
using System.Security.Principal;
using System.Numerics;

namespace PHIMS.Controllers
{
    [Authorize(Roles ="Super Admin")]
    public class AdminsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager ;
        private readonly UserManager<IdentityUser> _userManager;
        public int ID;
        public AdminsController(ApplicationDbContext context , RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult Home()
        {
            string today = DateTime.Now.ToString("dddd").ToLower();
            var result = _context.Doctor.Include(u => u.Admin).Where(u=>u.Working_days.Contains(today) &&u.Available==true );
            return View( result.ToList());

        }
  
        [AllowAnonymous]
        public IActionResult Login( )
        {
            var UserId = _userManager.GetUserId(User);
            if (UserId == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            else
            {
                if(User.IsInRole("Doctor"))
                {
                    var subUser = _context.Doctor.FirstOrDefault(u => u.UserId == UserId);
                    
                    if (subUser == null)
                    {
                        return Redirect("/Home/Index");
                    }
                    else
                    {

                        var getID = _context.Doctor.FirstOrDefault(u=>u.UserId==UserId).Doctor_Id; 
                        HttpContext.Session.SetInt32("test", getID); 
                        return RedirectToAction("Home");
                    }
                }
                if(User.IsInRole("User")|| User.IsInRole("User Read"))
                {
                    var subUser = _context.User.FirstOrDefault(u => u.UserId == UserId);

                    if (subUser == null)
                    {
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        var getID = _context.User.FirstOrDefault(u => u.UserId == UserId).User_Id;
                        HttpContext.Session.SetInt32("test", getID);
                        return RedirectToAction("Home");
                    }
                }
                if(User.IsInRole("Admin"))
                {
                    var subUser = _context.Admin.FirstOrDefault(u => u.UserId == UserId);

                    if (subUser == null)
                    {
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        var getID = _context.Admin.FirstOrDefault(u => u.UserId == UserId).Admin_Id;
                        HttpContext.Session.SetInt32("test", getID);
                        return RedirectToAction("Home");
                    }
                }
                else
                {
                    return RedirectToAction("Home"); 
                }
            }  
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
              return _context.Admin != null ? 
                          View(await _context.Admin.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Admin'  is null.");
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            if (id == null || _context.Admin == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.Admin_Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }
        
        // GET: Admins/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Name");
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Admin_Id,F_Name,M_Name,L_Name,Phone_Number,Email,DOB,R_Date,Gender,UserId")] Admin admin)
        {
            if (!ModelState.IsValid)
            {
                var check = _context.Admin.FirstOrDefault(u => u.UserId == admin.UserId);
                var check1 = _context.Doctor.FirstOrDefault(u => u.UserId == admin.UserId);
                var check2 = _context.User.FirstOrDefault(u => u.UserId == admin.UserId);
                if (check!=null || check1!=null || check2!=null)
                {
                    TempData[SD.Error] = "This Account Already Assing to another Admin ";
                    return RedirectToAction(nameof(Index));
                }
                _context.Add(admin);
                await _context.SaveChangesAsync();
                TempData[SD.Success] = "Admin Saved Seccessfully";
                TempData[SD.Type] = "Saved";
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name",admin.UserId);
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Admin == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Admin_Id,F_Name,M_Name,L_Name,Phone_Number,Email,DOB,R_Date,Gender,UserId")] Admin admin)
        {
            if (id != admin.Admin_Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                    TempData[SD.Type] = "Updated";
                    TempData[SD.Success] ="Admin Updated Successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Admin_Id))
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
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Admin == null)
            {
                return NotFound();
            }
            var check=_context.Doctor.FirstOrDefault(u=>u.Admin_Id == id);
            if(check!=null)
            {
                TempData[SD.Error] = "This Admin Can not Deleted";
                return RedirectToAction("Index");
            }
            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.Admin_Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Admin == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Admin'  is null.");
            }
            var admin = await _context.Admin.FindAsync(id);
            if (admin != null)
            {
                _context.Admin.Remove(admin);
            }
            TempData[SD.Success] = "Admin Deleted Seccessfully";
            TempData[SD.Type] = "Deleted";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
          return (_context.Admin?.Any(e => e.Admin_Id == id)).GetValueOrDefault();
        }
    }
}
