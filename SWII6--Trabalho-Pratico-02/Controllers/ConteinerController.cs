using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tp2tuller.Models;
using System.Threading.Tasks;
using System.Linq;
using tp2tuller.Data;

namespace tp2tuller.Controllers
{
    public class ConteinerController : Controller
    {
        private readonly TPDbContext _context;

        public ConteinerController(TPDbContext context)
        {
            _context = context;
        }

        // GET: Conteiner
        public async Task<IActionResult> Index()
        {
            var conteiners = await _context.Containers.Include(c => c.BL).ToListAsync();
            return View(conteiners);
        }

        // GET: Conteiner/Create
        public IActionResult Create()
        {
            ViewBag.BLs = _context.BLs.ToList(); // Passando a lista de BLs para o dropdown
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,Numero,Tipo,Tamanho,ID_BL,BL")] Conteiner conteiner)
        {
            conteiner.BL = _context.BLs.Find(conteiner.ID_BL);

            if (ModelState.IsValid)
            {
                _context.Add(conteiner);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Passar a lista de BLs para a View
            ViewBag.BLs = _context.BLs.ToList();
            return View(conteiner);
        }


        // Método GET para editar
        public IActionResult Edit(int id)
        {
            var conteiner = _context.Containers.Include(c => c.BL).FirstOrDefault(c => c.ID == id);
            if (conteiner == null)
            {
                return NotFound();
            }
            ViewBag.BLs = _context.BLs.ToList();
            return View(conteiner);
        }

        // Método POST para salvar as alterações do contêiner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Numero,Tipo,Tamanho,ID_BL")] Conteiner conteiner)
        {
            if (id != conteiner.ID)
            {
                return NotFound(); // Verificar se o ID do contêiner é válido
            }

            var blExistente = _context.BLs.Find(conteiner.ID_BL);
            if (blExistente == null)
            {
                ModelState.AddModelError("ID_BL", "BL não encontrado.");
                ViewBag.BLs = _context.BLs.ToList();
                return View(conteiner); // Retorna a view com o erro de BL não encontrado
            }

            // Atribuir o BL existente ao contêiner
            conteiner.BL = blExistente;

            if (ModelState.IsValid)
            {
                try
                {
                    // Atualizar o contêiner no banco de dados
                    _context.Update(conteiner);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index)); // Redireciona para a lista
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConteinerExists(conteiner.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Se o ModelState não for válido, retornar para a view com os dados e erros
            ViewBag.BLs = _context.BLs.ToList();
            return View(conteiner);
        }

        // POST: Conteiner/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var conteiner = await _context.Containers.FindAsync(id);
            if (conteiner != null)
            {
                _context.Containers.Remove(conteiner);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ConteinerExists(int id)
        {
            return _context.Containers.Any(e => e.ID == id);
        }

        public IActionResult Relatorio()
        {
            // Buscar a lista de BLs com os seus containers relacionados
            var blsComContainers = _context.BLs
                .Include(bl => bl.Containers)
                .ToList();

            return View(blsComContainers);
        }

    }
}
