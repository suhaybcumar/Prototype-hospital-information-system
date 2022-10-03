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
    public class ResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Results
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Result.Include(r => r.Doctor).Include(r => r.Pateint).Where(p => p.Doctor_ID == HttpContext.Session.GetInt32("test"));
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Results/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Result == null)
            {
                return NotFound();
            }

            var result = await _context.Result
                .Include(r => r.Doctor)
                .Include(r => r.Pateint)
                .FirstOrDefaultAsync(m => m.Id_Result == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Results/Create
        [Authorize(Roles ="Doctor")]
        public IActionResult Create()
        {
            ViewData["Pateint_ID"] = new SelectList(_context.Pateint, "Pateint_Id", "F_Name");
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Result,result,Advice,Pateint_ID,Doctor_ID")] Result result)
        {
            if (!ModelState.IsValid)
            {
                result.Doctor_ID = (int)HttpContext.Session.GetInt32("test");
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Pateint_ID"] = new SelectList(_context.Pateint, "Pateint_Id", "F_Name", result.Pateint_ID);
            return View(result);
        }

        // GET: Results/Edit/5
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Result == null)
            {
                return NotFound();
            }

            var result = await _context.Result.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["Pateint_ID"] = new SelectList(_context.Pateint, "Pateint_Id", "F_Name", result.Pateint_ID);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Result,result,Advice,Pateint_ID,Doctor_ID")] Result result)
        {
            if (id != result.Id_Result)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    result.Doctor_ID = (int)HttpContext.Session.GetInt32("test");
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.Id_Result))
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
            ViewData["Pateint_ID"] = new SelectList(_context.Pateint, "Pateint_Id", "F_Name", result.Pateint_ID);
            return View(result);
        }

        // GET: Results/Delete/5
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Result == null)
            {
                return NotFound();
            }

            var result = await _context.Result
                .Include(r => r.Doctor)
                .Include(r => r.Pateint)
                .FirstOrDefaultAsync(m => m.Id_Result == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Result == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Result'  is null.");
            }
            var result = await _context.Result.FindAsync(id);
            if (result != null)
            {
                _context.Result.Remove(result);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultExists(int id)
        {
          return (_context.Result?.Any(e => e.Id_Result == id)).GetValueOrDefault();
        }
    }
}
