using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tp2tuller.Data;
using tp2tuller.Models;

public class BLController : Controller
{
    private readonly TPDbContext _context;

    public BLController(TPDbContext context)
    {
        _context = context;
    }

    // GET: Lista de BL
    public async Task<IActionResult> Index()
    {
        return View(await _context.BLs.Include(b => b.Containers).ToListAsync());
    }

    // GET: Create BL
    public IActionResult Create()
    {
        return View();
    }

    // POST: Create BL
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Numero,Consignee,Navio")] BL bl)
    {
        bl.ID = 0;

        if (ModelState.IsValid)
        {
            _context.Add(bl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // Redirecione para a lista ou página apropriada
        }
        return View(bl);
    }

    // GET: Edit BL
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var bl = await _context.BLs.FindAsync(id);
        if (bl == null) return NotFound();
        return View(bl);
    }

    // POST: Edit BL
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Numero,Consignee,Navio")] BL bl)
    {
        if (id != bl.ID) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(bl);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BLExists(bl.ID)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(bl);
    }

    // GET: BL/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var bl = await _context.BLs.FindAsync(id);
        if (bl == null)
        {
            return NotFound();
        }

        _context.BLs.Remove(bl);
        await _context.SaveChangesAsync();

        // Redirecionar de volta para a lista ou grid
        return RedirectToAction(nameof(Index));
    }

    // POST: Delete BL
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var bl = await _context.BLs.FindAsync(id);
        _context.BLs.Remove(bl);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BLExists(int id)
    {
        return _context.BLs.Any(e => e.ID == id);
    }
}
