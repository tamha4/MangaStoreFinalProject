using MangaStore.Model.MangaModel;
using MangaStore.Model.GenreTypeModel;
using MangaStore.Service.MangaS;
using Microsoft.AspNetCore.Mvc;
using MangaStore.Service.GenreTypeService;
using MangaStore.Data;

namespace MangaStore.MVC.Controllers
{
    public class MangaController : Controller
    {
        private readonly MangaStoreDbContext _context;
        private readonly IMangaService _service;
        private readonly IGenreTypeService _genreService;

        public MangaController(IMangaService service, IGenreTypeService genreService, MangaStoreDbContext context)
        {
            _service = service;
            _genreService = genreService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<MangaListItem> mangas = await _service.GetAllMangas();
            return View(mangas);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.GenreTypes = _genreService.GetAllGenreTypes();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MangaCreate model)
        {      
            if(!ModelState.IsValid)
                return View(model);
            
            await _service.CreateManga(model);
            return RedirectToAction(nameof(Index));
        }

        [ActionName("Details")]
        public async Task<IActionResult> MangaDetail(int id)
        {
            MangaDetail mangaDetail = await _service.GetMangaById(id);

            if(mangaDetail == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(mangaDetail);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            MangaEdit mangaEdit = await _service.GetMangaEditById(id);

            if (mangaEdit is null)
                return RedirectToAction(nameof(Index));

            ViewBag.GenreTypes = _genreService.GetAllGenreTypes();
            return View(mangaEdit); // Using MangaEdit model for GET
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MangaEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            bool isSuccess = await _service.UpdateManga(model); // Using MangaEdit model for POST

            if (!isSuccess)
            {
                ViewBag.ErrorMessage = "Failed to update the manga";
                return View(model);
            }

            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            MangaDetail mangaDetail = await _service.GetMangaById(id);

            if (mangaDetail is null)
                return RedirectToAction(nameof(Index));

            return View(mangaDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, MangaDetail model)
        {
            MangaDetail mangaDetail = await _service.GetMangaById(model.Id);

            if (mangaDetail is null)
                return RedirectToAction(nameof(Index));

            bool isSuccess = await _service.DeleteManga(model.Id);

            if (!isSuccess)
            {
                ViewBag.ErrorMessage = "Failed to delete the manga";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


    }
}