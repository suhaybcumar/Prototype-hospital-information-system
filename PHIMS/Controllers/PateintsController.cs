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
    [Authorize(Roles ="Admin,Super Admin,Doctor,User,User Read")]   
    public class PateintsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PateintsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pateints
        public async Task<IActionResult> Index()
        {
            if(User.IsInRole("Doctor"))
            {
                string date = DateTime.Now.ToString("M/d/yyyy");
                var applicationDbContext = _context.Pateint.Where(p=>p.R_Date==DateTime.Today && p.Approve==true ||p.Type=="Answer" &&p.State!="Finished").Where(p=>p.Doctr_Id==HttpContext.Session.GetInt32("test")).Include(p => p.Doctor).Include(p => p.User).OrderBy(p => p.Pateint_Id).Reverse();
                return View(await applicationDbContext.ToListAsync());
            }
            if(User.IsInRole("User"))
            {
                var applicationDbContext = _context.Pateint.Include(p => p.Doctor).Include(p => p.User).OrderByDescending(p => p.Pateint_Id);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Pateint.Include(p => p.Doctor).Include(p => p.User).OrderByDescending(p => p.Pateint_Id);
                return View(await applicationDbContext.ToListAsync());
            }
            
        }
       public IActionResult All()
        {
            var applicationDbContext = _context.Pateint.Include(p => p.Doctor).Include(p => p.User).OrderByDescending(p => p.Pateint_Id);
            return View(applicationDbContext.ToList());
        }

        // GET: Pateints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pateint == null)
            {
                return NotFound();
            }

            var pateint = await _context.Pateint
                .Include(p => p.Doctor)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Pateint_Id == id);
            if (pateint == null)
            {
                return NotFound();
            }

            return View(pateint);
        }
        [Authorize(Roles ="User")]
        // GET: Pateints/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> selectListItems = _context.Doctor.Select(u => new SelectListItem { Text = u.F_Name+" "+u.M_Name+" "+u.L_Name, Value = u.Doctor_Id.ToString() });
            ViewData["Doctr_Id"] = selectListItems;
           
            return View();
        }

        // POST: Pateints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pateint_Id,F_Name,M_Name,L_Name,Phone_Number,Email,DOB,R_Date,Gender,User_Id,Doctr_Id,Type,Approve,State,Fee")] Pateint pateint)
        {
            if (!ModelState.IsValid)
            {
                pateint.User_Id = (int)HttpContext.Session.GetInt32("test"); 
                if (Request.Form["Fee"]== "Free Of Charge")
                {
                    pateint.Type = "First Visit";
                    pateint.State = "waiting";
                    pateint.Approve = true;
                    _context.Add(pateint);
                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Pateint Saved Successfully";
                    TempData[SD.Type] = "Pateint";
                }
                else
                {
                    pateint.Type = "First Visit";
                    pateint.State = "waiting";
                    pateint.Approve = false;
                    _context.Add(pateint);
                    await _context.SaveChangesAsync();
                    TempData[SD.Success] = "Pateint Saved Successfully";
                    TempData[SD.Type] = "Pateint";
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Doctr_Id"] = new SelectList(_context.Doctor, "Doctor_Id", "F_Name", pateint.Doctr_Id);
            TempData[SD.Error] = "Something Went Wrong";
            TempData[SD.Type] = "Pateint";
            return View(pateint);
        }

        // GET: Pateints/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pateint == null)
            {
                return NotFound();
            }

            var pateint = await _context.Pateint.FindAsync(id);
            if (pateint == null)
            {
                return NotFound();
            }
            ViewData["Doctr_Id"] = new SelectList(_context.Doctor, "Doctor_Id", "F_Name", pateint.Doctr_Id);
            
            return View(pateint);
        }

        // POST: Pateints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pateint_Id,F_Name,M_Name,L_Name,Phone_Number,Email,DOB,R_Date,Gender,User_Id,Doctr_Id,Type,Approve,State,Fee")] Pateint pateint)
        {
            if (id != pateint.Pateint_Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    pateint.User_Id = (int)HttpContext.Session.GetInt32("test");
                    if (Request.Form["Type"] == "Answer")
                    {
                        pateint.Approve = true;
                        pateint.Type = "Answer";
                        _context.Update(pateint);
                        await _context.SaveChangesAsync();
                        TempData[SD.Success] = "Pateint Updated Successfully";
                        TempData[SD.Type] = "Pateint";
                    }
                    else
                    {
                        pateint.Type = "First Visit";
                        pateint.Approve = false ;
                        _context.Update(pateint);
                        await _context.SaveChangesAsync();
                        TempData[SD.Success] = "Pateint Updated Successfully";
                        TempData[SD.Type] = "Pateint";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PateintExists(pateint.Pateint_Id))
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
            ViewData["Doctr_Id"] = new SelectList(_context.Doctor, "Doctor_Id", "F_Name", pateint.Doctr_Id);
            TempData[SD.Error] = "Something Went Wrong";
            TempData[SD.Type] = "Pateint";
            return View(pateint);
        }

        // GET: Pateints/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pateint == null)
            {
                return NotFound();
            }

            var pateint = await _context.Pateint
                .Include(p => p.Doctor)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Pateint_Id == id);
            if (pateint == null)
            {
                return NotFound();
            }

            return View(pateint);
        }

        // POST: Pateints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pateint == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pateint'  is null.");
            }
            var pateint = await _context.Pateint.FindAsync(id);
            if (pateint != null)
            {
                _context.Pateint.Remove(pateint);
            }
            
            await _context.SaveChangesAsync();
            TempData[SD.Success] = "Pateint Deletd Successfully";
            TempData[SD.Type] = "Pateint";
            return RedirectToAction(nameof(Index));
        }

        private bool PateintExists(int id)
        {
          return (_context.Pateint?.Any(e => e.Pateint_Id == id)).GetValueOrDefault();
        }
      

        //Approve
        [Authorize(Roles="User")]
        public IActionResult Approve(int? id)
        {
            
            try
            {
                _context.Database.ExecuteSqlRaw($" Update \"Pateint\"  set \"Approve\"=true where \"Pateint_Id\"={id}");
                _context.SaveChanges();
                TempData[SD.Success] = "Pateint Approved Successfully";
                TempData[SD.Type] = "Pateint";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }
        //State
        [Authorize(Roles ="Doctor")]
        public IActionResult State(int? id)
        {
            try
            {
                var pateint=_context.Pateint.Find(id);
                if(pateint.State=="waiting")
                {
                    _context.Database.ExecuteSqlRaw($" Update \"Pateint\"  set \"State\"='inclinic' where \"Pateint_Id\"={id}");
                    _context.SaveChanges();
                    TempData[SD.Success] = "Pateint Inclinic is Started Successfully";
                    TempData[SD.Type] = "Pateint";
                    return RedirectToAction(nameof(Index));
                }
                else if(pateint.State=="inclinic")
                {
                    _context.Database.ExecuteSqlRaw($" Update \"Pateint\"  set \"State\"='Finished' , \"Approve\"=false where \"Pateint_Id\"={id}");
                    _context.SaveChanges();
                    TempData[SD.Success] = "Pateint Process Is Finished Successfully";
                    TempData[SD.Type] = "Pateint";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                throw;
            }
        }
        //Answer
        [Authorize(Roles="User")]
        public IActionResult Answer(int? id)
        {
            try
            {
                _context.Database.ExecuteSqlRaw($" Update \"Pateint\"  set  \"State\"='waiting' ,\"Approve\"=true ,\"Type\"='Answer' where \"Pateint_Id\"={id}");
                _context.SaveChanges();
                TempData[SD.Success] = "Pateint is Approved for Answered";
                TempData[SD.Type] = "Pateint";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
        }
    }
}
