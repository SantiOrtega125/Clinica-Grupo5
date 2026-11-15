using Microsoft.AspNetCore.Authorization;      // ← NUEVO
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Models;

[Authorize(Roles = "Director")]                 // ← NUEVO: solo el Director entra a las acciones...
public class EspecialidadesController : Controller
{
    private readonly ClinicaContext _context;

    public EspecialidadesController(ClinicaContext context)
    {
        _context = context;
    }

    // GET: Especialidades
    [AllowAnonymous]                            // ← NUEVO: ...salvo a la lista
    public async Task<IActionResult> Index()
    {
        return View(await _context.Especialidad.ToListAsync());
    }

    // GET: Especialidades/Details/5
    [AllowAnonymous]                            // ← NUEVO: ...y al detalle
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var especialidad = await _context.Especialidad
            .FirstOrDefaultAsync(m => m.Id == id);
        if (especialidad == null)
        {
            return NotFound();
        }

        return View(especialidad);
    }

    // GET: Especialidades/Create  (solo Director)
    public IActionResult Create()
    {
        return View();
    }

    // POST: Especialidades/Create  (solo Director)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion")] Especialidad especialidad)
    {
        if (ModelState.IsValid)
        {
            _context.Add(especialidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(especialidad);
    }

    // GET: Especialidades/Edit/5  (solo Director)
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var especialidad = await _context.Especialidad.FindAsync(id);
        if (especialidad == null)
        {
            return NotFound();
        }
        return View(especialidad);
    }

    // POST: Especialidades/Edit/5  (solo Director)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Descripcion")] Especialidad especialidad)
    {
        if (id != especialidad.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(especialidad);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspecialidadExists(especialidad.Id))
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
        return View(especialidad);
    }

    // GET: Especialidades/Delete/5  (solo Director)
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var especialidad = await _context.Especialidad
            .FirstOrDefaultAsync(m => m.Id == id);
        if (especialidad == null)
        {
            return NotFound();
        }

        return View(especialidad);
    }

    // POST: Especialidades/Delete/5  (solo Director)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var especialidad = await _context.Especialidad.FindAsync(id);
        if (especialidad != null)
        {
            _context.Especialidad.Remove(especialidad);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EspecialidadExists(int? id)
    {
        return _context.Especialidad.Any(e => e.Id == id);
    }
}