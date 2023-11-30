using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MangaStore.Model.ImageModel;
using MangaStore.Service.ImageService;

namespace MangaStore.MVC.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _service;

        public ImageController(IImageService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var images = await _service.GetImages();
            return View(images);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var imageData = memoryStream.ToArray();

                    var imageCreateModel = new ImageCreate
                    {
                        ImageData = imageData
                    };

                    await _service.CreateImage(imageCreateModel);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            ImageDetail imageDetail = await _service.GetImageById(id);

            if (imageDetail is null)
                return RedirectToAction(nameof(Index));

            return View(imageDetail);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            ImageEdit imageEdit = await _service.GetImageEditById(id);

            if(imageEdit is null)
                return RedirectToAction(nameof(Index));

            return View(imageEdit);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(ImageEdit model, IFormFile file)
        {

            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var imageData = memoryStream.ToArray();

                    // Update the existing image with the new data
                    model.ImageData = imageData;
                    bool isSuccess = await _service.UpdateImage(model);

                    if (!isSuccess)
                        return View(model);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ImageDetail imageDetail = await _service.GetImageById(id);

            if(imageDetail is null)
                return RedirectToAction(nameof(Index));

            return View(imageDetail);
        
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, ImageDetail model)
        {
            ImageDetail imageDetail = await _service.GetImageById(id);

            if (imageDetail is null)
                return RedirectToAction(nameof(Index));

            bool isSuccess = await _service.DeleteIMage(model.Id);

            if(!isSuccess)
                return RedirectToAction(nameof(Index));
            
            return RedirectToAction(nameof(Index));
            
        }



    }
}
