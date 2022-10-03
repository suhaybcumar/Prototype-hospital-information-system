using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PHIMS.Data;
using PHIMS.Models;

namespace PHIMS.Controllers
{
    [Authorize(Roles ="Admin,Super Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        public UsersController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        // GET: Users
        [Authorize(Roles ="Super Admin,Admin")]
        public async Task<IActionResult> Index()
        {
            if(User.IsInRole("Admin"))
            {
                var applicationDbContext = _context.User.Include(u => u.Admin).Where(p=>p.Admin_Id==HttpContext.Session.GetInt32("test"));
                ViewData["Admin_Id"] = new SelectList(_context.Admin, "Admin_Id", "F_Name");
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.User.Include(u => u.Admin);
                ViewData["Admin_Id"] = new SelectList(_context.Admin, "Admin_Id", "F_Name");
                return View(await applicationDbContext.ToListAsync());
            }
            
        }

        // GET: Users/Details/5
        [Authorize(Roles = "Admin,Super Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Admin)
                .FirstOrDefaultAsync(m => m.User_Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("User_Id,F_Name,M_Name,L_Name,Phone_Number,Email,DOB,R_Date,Gender,Admin_Id,UserId")] User user)
        {
            if (!ModelState.IsValid)
            {
                var check = _context.Admin.FirstOrDefault(u => u.UserId == user.UserId);
                var check1 = _context.Doctor.FirstOrDefault(u => u.UserId == user.UserId);
                var check2 = _context.User.FirstOrDefault(u => u.UserId == user.UserId);
                if (check != null || check1 != null || check2 != null)
                {
                    TempData[SD.Error] = "This Account Already Assing to another User";
                    return RedirectToAction(nameof(Index));
                }
                user.Admin_Id = (int)HttpContext.Session.GetInt32("test");
                _context.Add(user);
                await _context.SaveChangesAsync();
                TempData[SD.Success] = "Saved Seccuessfully ";
                TempData[SD.Type] = "Users";
                return RedirectToAction(nameof(Index));
            }
            ViewData["Admin_Id"] = new SelectList(_context.Admin, "Admin_Id", "F_Name", user.Admin_Id);
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.Admin_Id = new SelectList(_context.Admin, "Admin_Id", "F_Name", user.Admin_Id);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("User_Id,F_Name,M_Name,L_Name,Phone_Number,Email,DOB,R_Date,Gender,Admin_Id,UserId")] User user)
        {
            _signInManager.SignOutAsync();
            if (id != user.User_Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    user.Admin_Id = (int)HttpContext.Session.GetInt32("test");
                    _context.Update(user);
                    TempData[SD.Success] = "Updated Seccuessfully ";
                    TempData[SD.Type] = "Users";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.User_Id))
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
            ViewData["Admin_Id"] = new SelectList(_context.Admin, "Admin_Id", "F_Name", user.Admin_Id);
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }
            var check = _context.Pateint.FirstOrDefault(u => u.User_Id == id);
            if (check != null)
            {
                TempData[SD.Error] = "This User Can't Deleted";
                return RedirectToAction("Index");
            }
            var user = await _context.User
                .Include(u => u.Admin)
                .FirstOrDefaultAsync(m => m.User_Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'ApplicationDbContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.User_Id == id)).GetValueOrDefault();
        }
    }
}
