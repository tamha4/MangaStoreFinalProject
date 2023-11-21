using System.Collections.Generic;
using System.Threading.Tasks;
using MangaStore.Model.GenreTypeModel;
using MangaStore.Service.GenreTypeService;
using Microsoft.AspNetCore.Mvc;

namespace MangaStore.MVC.Controllers
{
    public class GenreTypeController : Controller
    {
        private readonly IGenreTypeService _service;

        public GenreTypeController(IGenreTypeService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<GenreTypeListItem> genreTypes = await _service.GetAllGenreTypes();
            return View(genreTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreTypeCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _service.CreateGenreType(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
