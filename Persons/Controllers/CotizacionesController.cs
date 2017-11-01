using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persons.Data;
using Persons.Models;

using BCU_Cotizaciones;
using Newtonsoft.Json;

namespace Persons.Controllers
{
    public class CotizacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CotizacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cotizaciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cotizaciones.Include(c => c.Moneda);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cotizaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotizaciones = await _context.Cotizaciones
                .Include(c => c.Moneda)
                .SingleOrDefaultAsync(m => m.IDCotizacion == id);
            if (cotizaciones == null)
            {
                return NotFound();
            }

            return View(cotizaciones);
        }

        // GET: Cotizaciones/Create
        public IActionResult Create()
        {
            ViewData["IDMoneda"] = new SelectList(_context.Monedas.OrderBy(m => m.Sigla), "IDMoneda", "Sigla");
            return View();
        }

        // POST: Cotizaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDCotizacion,IDMoneda,Fecha,Cotizacion")] Cotizaciones cotizaciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cotizaciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDMoneda"] = new SelectList(_context.Monedas.OrderBy(m => m.Sigla), "IDMoneda", "Sigla", cotizaciones.IDMoneda);
            return View(cotizaciones);
        }


        [HttpPost]
        public async Task<IActionResult> ObtenerCotizacionesBCUAsync([Bind("IDMoneda,Fecha,Cotizacion")] Cotizaciones cotizaciones)
        {

            wsbcucotizacionesin DatosIn = new wsbcucotizacionesin();
            DatosIn.FechaDesde = cotizaciones.Fecha; // new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);  //ViewData["Fecha"];
            DatosIn.FechaHasta = cotizaciones.Fecha;


            short[] misMonedas = new short[1];

            //traigo las monedas, para obtener el código de BCU
            var monedas = await _context.Monedas
                .SingleOrDefaultAsync(m => m.IDMoneda == cotizaciones.IDMoneda);

            misMonedas[0] = (short)monedas.CodBCU;
            DatosIn.Moneda = misMonedas;

            wsbcucotizacionesout DatosOut = new wsbcucotizacionesout();

            var miRequest = new ExecuteRequest(DatosIn);
            var miResponse = new ExecuteResponse();

            wsbcucotizacionesSoapPortClient SoapClient = new wsbcucotizacionesSoapPortClient();
            ExecuteResponse x = await SoapClient.ExecuteAsync(DatosIn);

            var miRespuestaStatusMensaje = x.Salida.respuestastatus.mensaje;
            var miRespuestaStatusCodigo = x.Salida.respuestastatus.codigoerror;
            var miRespuestaStatus = x.Salida.respuestastatus.status;

            //Console.WriteLine(x.Salida.datoscotizaciones[0].TCC);

            //ViewData["Cotizacion"] = x.Salida.datoscotizaciones[0].TCC;

            var miAnswer = JsonConvert.SerializeObject(new { tc = x.Salida.datoscotizaciones[0].TCC, status = miRespuestaStatus, error = miRespuestaStatusCodigo, mensaje = miRespuestaStatusMensaje });

            return Json(miAnswer); 
        }






        // GET: Cotizaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotizaciones = await _context.Cotizaciones.SingleOrDefaultAsync(m => m.IDCotizacion == id);
            if (cotizaciones == null)
            {
                return NotFound();
            }
            ViewData["IDMoneda"] = new SelectList(_context.Monedas.OrderBy(m => m.Sigla), "IDMoneda", "Sigla", cotizaciones.IDMoneda);
            return View(cotizaciones);
        }

        // POST: Cotizaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDCotizacion,IDMoneda,Fecha,Cotizacion")] Cotizaciones cotizaciones)
        {
            if (id != cotizaciones.IDCotizacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cotizaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CotizacionesExists(cotizaciones.IDCotizacion))
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
            ViewData["IDMoneda"] = new SelectList(_context.Monedas.OrderBy(m => m.Sigla), "IDMoneda", "Sigla", cotizaciones.IDMoneda);
            return View(cotizaciones);
        }

        // GET: Cotizaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotizaciones = await _context.Cotizaciones
                .Include(c => c.Moneda)
                .SingleOrDefaultAsync(m => m.IDCotizacion == id);
            if (cotizaciones == null)
            {
                return NotFound();
            }

            return View(cotizaciones);
        }

        // POST: Cotizaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cotizaciones = await _context.Cotizaciones.SingleOrDefaultAsync(m => m.IDCotizacion == id);
            _context.Cotizaciones.Remove(cotizaciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CotizacionesExists(int id)
        {
            return _context.Cotizaciones.Any(e => e.IDCotizacion == id);
        }
    }
}
