using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MangaStore.Model.StoreModel;
using MangaStore.Service.MangaS;
using MangaStore.Service.StoreService;
using Microsoft.AspNetCore.Mvc;

namespace MangaStore.MVC.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService _service;
        private readonly IMangaService _mangaService;

        public StoreController(IStoreService service, IMangaService mangaService)
        {
            _service = service;
            _mangaService = mangaService;
        }

        public async Task<IActionResult> Index()
        {
            List<StoreListItem> stores = await _service.GetAllStores();
            return View(stores);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Mangas = _mangaService.GetAllMangas();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(StoreCreate model)
        {
            if(!ModelState.IsValid)
                return View(model);

            await _service.CreateStore(model);
            return RedirectToAction(nameof(Index));
        }
    }
}