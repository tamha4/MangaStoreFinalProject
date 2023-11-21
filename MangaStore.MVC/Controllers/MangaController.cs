using MangaStore.Model.MangaModel;
using MangaStore.Model.GenreTypeModel;
using MangaStore.Service.MangaS;
using Microsoft.AspNetCore.Mvc;
using MangaStore.Service.GenreTypeService;

namespace MangaStore.MVC.Controllers
{
    public class MangaController : Controller
    {
        private readonly IMangaService _service;
        private readonly IGenreTypeService _genreService;

        public MangaController(IMangaService service, IGenreTypeService genreService)
        {
            _service = service;
            _genreService = genreService;
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
    }
}