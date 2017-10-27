using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persons.Data;
using Persons.Models;

namespace Persons.Controllers
{
    public class PersonsDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonsDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonsDatas
        public async Task<IActionResult> Index()
        {
            return View(await _context.PersonsData.ToListAsync());
        }

        // GET: PersonsDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personsData = await _context.PersonsData
                .SingleOrDefaultAsync(m => m.ID == id);
            if (personsData == null)
            {
                return NotFound();
            }

            return View(personsData);
        }

        // GET: PersonsDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonsDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Age,Sex")] PersonsData personsData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personsData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personsData);
        }

        // GET: PersonsDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personsData = await _context.PersonsData.SingleOrDefaultAsync(m => m.ID == id);
            if (personsData == null)
            {
                return NotFound();
            }
            return View(personsData);
        }

        // POST: PersonsDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Age,Sex")] PersonsData personsData)
        {
            if (id != personsData.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personsData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonsDataExists(personsData.ID))
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
            return View(personsData);
        }

        // GET: PersonsDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personsData = await _context.PersonsData
                .SingleOrDefaultAsync(m => m.ID == id);
            if (personsData == null)
            {
                return NotFound();
            }

            return View(personsData);
        }

        // POST: PersonsDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personsData = await _context.PersonsData.SingleOrDefaultAsync(m => m.ID == id);
            _context.PersonsData.Remove(personsData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonsDataExists(int id)
        {
            return _context.PersonsData.Any(e => e.ID == id);
        }
    }
}
