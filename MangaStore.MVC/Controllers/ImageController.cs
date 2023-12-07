using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MangaStore.Model.ImageModel;
using MangaStore.Service.ImageService;
using MangaStore.Data;
using Microsoft.EntityFrameworkCore;
using MangaStore.Model.MangaModel;

namespace MangaStore.MVC.Controllers
{
    public class ImageController : Controller
    {
        private readonly MangaStoreDbContext _context;

        private readonly IImageService _service;

        public ImageController(IImageService service, MangaStoreDbContext context)
        {
            _context = context;
            _service = service;
        }

        //* Index

        public async Task<IActionResult> Index()
        {
            var images = await _service.GetImages();
            return View(images);
        }

        //* Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file, MangaCreate mangaCreateModel)
        {
            int? createdImageId = null;

            // Your existing code for creating an image
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

                    createdImageId = await _service.CreateImage(imageCreateModel);
                }
            }

            mangaCreateModel.ImageId = (int)createdImageId;

            return RedirectToAction("Index");
        }

        //* Details

        public async Task<IActionResult> Details(int id)
        {
            ImageDetail imageDetail = await _service.GetImageById(id);

            if (imageDetail is null)
                return RedirectToAction(nameof(Index));

            return View(imageDetail);
        }

        //* Updating

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

        //* Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ImageDetail imageDetail = await _service.GetImageById(id);

            if (imageDetail is null)
                return RedirectToAction(nameof(Index));

            return View(imageDetail);
        }

        private bool IsImageAssocited(int imageId)
        {
            bool isAssocitedWithManga = _context.Mangas.Any( m => m.ImageId == imageId);

            return isAssocitedWithManga;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, ImageDetail model)
        {
            ImageDetail imageDetail = await _service.GetImageById(id);

            if (imageDetail is null)
                return RedirectToAction(nameof(Index));

            // If the image is associated with manga, prompt the user to confirm
            bool isAssocitedWithManga = await _service.IsImageReplaced(id);

            if (isAssocitedWithManga)
            {
                // If the image is associated with manga, prompt the user to confirm
                return View("ConfirmReplace", imageDetail);
            }

            // If the image is not associated with manga, proceed with deletion
            bool isSuccess = await _service.DeleteIMage(model.Id);

            if (!isSuccess)
                return RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check if the image is associated with any manga
            bool isAssocitedWithManga = await _service.IsImageReplaced(id);

            if (isAssocitedWithManga)
            {
                // Update associated mangas to have null ImageId
                await _service.UpdateAssociatedMangas(id, newImageId: null);

                // Proceed with deletion after updating mangas
                bool isSuccess = await _service.DeleteIMage(id);

                if (!isSuccess)
                    return RedirectToAction(nameof(Index));

                return RedirectToAction(nameof(Index));
            }

            // If the image is not associated with manga, proceed with deletion without updating mangas
            bool isSuccessNoUpdate = await _service.DeleteIMage(id);

            if (!isSuccessNoUpdate)
                return RedirectToAction(nameof(Index));

            return RedirectToAction(nameof(Index));
        }

    }
}
