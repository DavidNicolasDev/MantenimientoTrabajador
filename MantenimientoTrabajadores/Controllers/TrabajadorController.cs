using MantenimientoTrabajadores.Models;
using MantenimientoTrabajadores.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoTrabajadores.Controllers
{
    public class TrabajadorController : Controller
    {
        private readonly TrabajadoresDataContext _context;

        public TrabajadorController(TrabajadoresDataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var trabajadores = _context.Trabajadores.Include(d => d.IdDepartamentoNavigation).Include(d=> d.IdProvinciaNavigation).Include(d=>d.IdDistritoNavigation);
            return View(await trabajadores.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Departamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewData["Provincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia");
            ViewData["Distrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrabajadorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trabajador = new Trabajadores()
                {
                    Nombres = model.Nombres,
                    TipoDocumento = model.TipoDocumento,
                    NumeroDocumento = model.NumeroDocumento,
                    Sexo = model.Sexo,
                    IdDepartamento = model.IdDepartamento,
                    IdProvincia = model.IdProvincia,
                    IdDistrito = model.IdDistrito
                };
                _context.Add(trabajador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento",model.IdDepartamento);
            ViewData["Provincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia",model.IdProvincia);
            ViewData["Distrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito",model.IdDistrito);
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null)
            {   
                return RedirectToAction(nameof(Index));
            }


            var editTrabajador = new TrabajadorViewModel()
            {
                Id = trabajador.Id,
                Nombres = trabajador.Nombres,
                TipoDocumento = trabajador.TipoDocumento,
                NumeroDocumento = trabajador.NumeroDocumento,
                Sexo = trabajador.Sexo,
                IdDepartamento = trabajador.IdDepartamento,
                IdProvincia = trabajador.IdProvincia,
                IdDistrito = trabajador.IdDistrito
            };

            ViewData["Departamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewData["Provincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia");
            ViewData["Distrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito");

            return View(editTrabajador);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TrabajadorViewModel model)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                trabajador.Id = model.Id;
                trabajador.Nombres = model.Nombres;
                trabajador.TipoDocumento = model.TipoDocumento;
                trabajador.NumeroDocumento = model.NumeroDocumento;
                trabajador.Sexo = model.Sexo;
                trabajador.IdDepartamento = model.IdDepartamento;
                trabajador.IdProvincia = model.IdProvincia;
                trabajador.IdDistrito = model.IdDistrito;
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento", model.IdDepartamento);
            ViewData["Provincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia", model.IdProvincia);
            ViewData["Distrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito", model.IdDistrito);
            return View(model);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null)
            {
                return RedirectToAction(nameof(Index));
            }
            _context.Trabajadores.Remove(trabajador);
            await  _context.SaveChangesAsync(true);
            return RedirectToAction(nameof(Index));
        }
    }
}
