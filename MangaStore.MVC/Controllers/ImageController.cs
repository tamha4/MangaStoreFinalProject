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

        public IActionResult Index()
        {
            var images = _service.GetImages();
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
    }
}
