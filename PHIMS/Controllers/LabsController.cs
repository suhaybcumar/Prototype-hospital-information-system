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
    [Authorize(Roles ="Doctor,Admin,Super Admin")]
    public class LabsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LabsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Labs
        public async Task<IActionResult> Index()
        {
            if(User.IsInRole("Doctor"))
            {
                var applicationDbContext = _context.Lab.Include(l => l.Doctor).Include(l => l.Pateint).Where(p=>p.Doctor_Id==HttpContext.Session.GetInt32("test"));
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Lab.Include(l => l.Doctor).Include(l => l.Pateint);
                return View(await applicationDbContext.ToListAsync());
            }
            
        }

        // GET: Labs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Lab == null)
            {
                return NotFound();
            }

            var lab = await _context.Lab
                .Include(l => l.Doctor)
                .Include(l => l.Pateint)
                .FirstOrDefaultAsync(m => m.Lab_Id == id);
            if (lab == null)
            {
                return NotFound();
            }

            return View(lab);
        }

        // GET: Labs/Create
        [Authorize(Roles ="Doctor")]
        public IActionResult Create()
        {
            ViewData["Pateint_Id"] = new SelectList(_context.Pateint, "Pateint_Id", "F_Name");
            return View();
        }

        // POST: Labs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Lab_Id,Prescription,Description,Doctor_Id,Pateint_Id")] Lab lab)
        {
            if (!ModelState.IsValid)
            {
                lab.Doctor_Id = (int)HttpContext.Session.GetInt32("test");
                lab.Lab_Id = Guid.NewGuid();
                _context.Add(lab);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Pateint_Id"] = new SelectList(_context.Pateint, "Pateint_Id", "F_Name", lab.Pateint_Id);
            return View(lab);
        }

        // GET: Labs/Edit/5
        [Authorize(Roles ="Doctor")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Lab == null)
            {
                return NotFound();
            }

            var lab = await _context.Lab.FindAsync(id);
            if (lab == null)
            {
                return NotFound();
            }
            ViewData["Pateint_Id"] = new SelectList(_context.Pateint, "Pateint_Id", "F_Name", lab.Pateint_Id);
            return View(lab);
        }

        // POST: Labs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Lab_Id,Prescription,Description,Doctor_Id,Pateint_Id")] Lab lab)
        {
            if (id != lab.Lab_Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    lab.Doctor_Id = (int)HttpContext.Session.GetInt32("test");
                    _context.Update(lab);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabExists(lab.Lab_Id))
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
            ViewData["Pateint_Id"] = new SelectList(_context.Pateint, "Pateint_Id", "F_Name", lab.Pateint_Id);
            return View(lab);
        }

        // GET: Labs/Delete/5
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Lab == null)
            {
                return NotFound();
            }

            var lab = await _context.Lab
                .Include(l => l.Doctor)
                .Include(l => l.Pateint)
                .FirstOrDefaultAsync(m => m.Lab_Id == id);
            if (lab == null)
            {
                return NotFound();
            }

            return View(lab);
        }

        // POST: Labs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Lab == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lab'  is null.");
            }
            var lab = await _context.Lab.FindAsync(id);
            if (lab != null)
            {
                _context.Lab.Remove(lab);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabExists(Guid id)
        {
          return (_context.Lab?.Any(e => e.Lab_Id == id)).GetValueOrDefault();
        }
    }
}
