using System.Collections.Generic;
using System.Threading.Tasks;
using MangaStore.Data;
using MangaStore.Model.GenreTypeModel;
using MangaStore.Model.MangaModel;
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

        [ActionName("Details")]
        public async Task<IActionResult> GenreTypeDetail(int id)
        {
            GenreTypeDetail genreTypeDetail = await _service.GetGenreTypeById(id);

            if (genreTypeDetail is null)
                return RedirectToAction(nameof(Index));

            return View(genreTypeDetail);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            GenreTypeEdit genreTypeEdit = await _service.GetTypeEditById(id);

            if(genreTypeEdit is null)
                return RedirectToAction(nameof(Index));

            return View(genreTypeEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GenreTypeEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            bool isSuccess = await _service.UpdateGenreType(model);

            if (!isSuccess)
                return View(model);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            GenreTypeDetail genreTypeDetail = await _service.GetGenreTypeById(id);

            if(genreTypeDetail is null)
                return RedirectToAction(nameof(Index));

            return View(genreTypeDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, GenreTypeDetail model)
        {
            GenreTypeDetail genreTypeDetail = await _service.GetGenreTypeById(model.Id);

            if(genreTypeDetail is null)
                return RedirectToAction(nameof(Index));

            bool isSuccess = await _service.DeleteGenreType(model.Id);

            if(!isSuccess)
                return RedirectToAction(nameof(Index));
            
            return RedirectToAction(nameof(Index));
        }

    }
}
