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
    public class MonedasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MonedasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Monedas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Monedas.ToListAsync());
        }

        // GET: Monedas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monedasData = await _context.Monedas
                .SingleOrDefaultAsync(m => m.IDMoneda == id);
            if (monedasData == null)
            {
                return NotFound();
            }

            return View(monedasData);
        }

        // GET: Monedas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Monedas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDMoneda,Sigla,CodBCU,Nombre")] MonedasData monedasData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monedasData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monedasData);
        }

        // GET: Monedas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monedasData = await _context.Monedas.SingleOrDefaultAsync(m => m.IDMoneda == id);
            if (monedasData == null)
            {
                return NotFound();
            }
            return View(monedasData);
        }

        // POST: Monedas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDMoneda,Sigla,CodBCU,Nombre")] MonedasData monedasData)
        {
            if (id != monedasData.IDMoneda)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monedasData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonedasDataExists(monedasData.IDMoneda))
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
            return View(monedasData);
        }

        // GET: Monedas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monedasData = await _context.Monedas
                .SingleOrDefaultAsync(m => m.IDMoneda == id);
            if (monedasData == null)
            {
                return NotFound();
            }

            return View(monedasData);
        }

        // POST: Monedas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monedasData = await _context.Monedas.SingleOrDefaultAsync(m => m.IDMoneda == id);
            _context.Monedas.Remove(monedasData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonedasDataExists(int id)
        {
            return _context.Monedas.Any(e => e.IDMoneda == id);
        }
    }
}
