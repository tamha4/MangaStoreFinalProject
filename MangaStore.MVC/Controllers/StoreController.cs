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

        //* Index

        public async Task<IActionResult> Index()
        {
            List<StoreListItem> stores = await _service.GetAllStores();
            return View(stores);
        }

        //* Create

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

        //* Details

        [ActionName("Details")]
        public async Task<IActionResult> StoreDetail(int id)
        {
            StoreDetail storeDetail = await _service.GetAllStoresById(id);

            if(storeDetail is null)
                return RedirectToAction(nameof(Index));
            
            return View(storeDetail);
        }

        //* Updating

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            StoreEdit storeEdit = await _service.GetStoreEditById(id);

            if(storeEdit is null)
                return RedirectToAction(nameof(Index));

            return View(storeEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StoreEdit model)
        {
            if(!ModelState.IsValid)
                return View(model);
            
            bool isSuccess = await _service.UpdateStores(model);

            if(!isSuccess)
                return View(model);

            return RedirectToAction(nameof(Index));
        }

        //* Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            StoreDetail storeDetail = await _service.GetAllStoresById(id);

            if(storeDetail is null)
                return RedirectToAction(nameof(Index));

            return View(storeDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, StoreDetail model)
        {
            StoreDetail storeDetail = await _service.GetAllStoresById(model.Id);

            if(storeDetail is null)
                return RedirectToAction(nameof(Index));

            bool isSuccess = await _service.DeleteStore(model.Id);

            if(!isSuccess)
                return RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(Index));
        }

    }
}