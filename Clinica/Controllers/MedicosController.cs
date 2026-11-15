using Microsoft.AspNetCore.Authorization;      // ← NUEVO
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinica.Models;

namespace Clinica.Controllers
{
    [Authorize]                                 // ← NUEVO: sin sesión no se entra a ninguna acción...
    public class MedicosController : Controller
    {
        private readonly ClinicaContext _context;

        public MedicosController(ClinicaContext context)
        {
            _context = context;
        }

        // GET: Medicos
        [AllowAnonymous]                        // ← NUEVO: ...salvo a la lista
        public async Task<IActionResult> Index()
        {
            var medicos = _context.Medico.Include(m => m.Especialidad);
            return View(await medicos.ToListAsync());
        }

        // GET: Medicos/Details/5
        [AllowAnonymous]                        // ← NUEVO: ...y al detalle
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var medico = await _context.Medico
                .Include(m => m.Especialidad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null) return NotFound();

            return View(medico);
        }

        // GET: Medicos/Create
        public IActionResult Create()
        {
            CargarEspecialidades();
            return View();
        }

        // POST: Medicos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,NumeroLicencia,AniosExperiencia,EspecialidadId")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CargarEspecialidades(medico.EspecialidadId);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var medico = await _context.Medico.FindAsync(id);
            if (medico == null) return NotFound();

            CargarEspecialidades(medico.EspecialidadId);
            return View(medico);
        }

        // POST: Medicos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,NumeroLicencia,AniosExperiencia,EspecialidadId")] Medico medico)
        {
            if (id != medico.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            CargarEspecialidades(medico.EspecialidadId);
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var medico = await _context.Medico
                .Include(m => m.Especialidad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null) return NotFound();

            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medico = await _context.Medico.FindAsync(id);
            if (medico != null)
            {
                _context.Medico.Remove(medico);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
            return _context.Medico.Any(e => e.Id == id);
        }

        // Llena la lista desplegable: guarda el Id y muestra el Nombre
        private void CargarEspecialidades(object? seleccionada = null)
        {
            ViewData["EspecialidadId"] = new SelectList(
                _context.Especialidad.OrderBy(e => e.Nombre), "Id", "Nombre", seleccionada);
        }
    }
}