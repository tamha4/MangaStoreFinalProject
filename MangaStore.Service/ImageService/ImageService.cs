using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MangaStore.Data;
using MangaStore.Model.ImageModel;
using MangaStore.Model.MangaModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MangaStore.Service.ImageService
{
    public class ImageService : IImageService
    {
        private readonly MangaStoreDbContext _context;

        public ImageService(MangaStoreDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateImage(ImageCreate model)
        {
            Image image = new Image
            {
                ImageData = model.ImageData,
            };

            _context.Images.Add(image);
            return await _context.SaveChangesAsync() == 1;
                }
        public async Task<bool> DeleteIMage(int id)
        {
            var imageEntity = await _context.Images.FindAsync(id);

            if (imageEntity is null)
                return false;

            // Remove the Image entity
            _context.Images.Remove(imageEntity);

            return await _context.SaveChangesAsync() == 1;
        }


        public async Task<ImageDetail> GetImageById(int id)
        {
            ImageDetail imageDetail = await _context.Images
                .Where(m => m.Id == id)
                .Select(m => new ImageDetail()
                {
                    Id = m.Id,
                    ImageData = m.ImageData,
                }).FirstOrDefaultAsync();

            return imageDetail;
        }

        public async Task<ImageEdit> GetImageEditById(int id)
        {
            ImageEdit imageEdit = await _context.Images
                .Where(i => i.Id == id)
                .Select(i => new ImageEdit()
                {
                    Id = i.Id,
                    ImageData = i.ImageData,
                }).FirstOrDefaultAsync();
            
            return imageEdit;
        }

        public async Task<List<ImageCreate>> GetImages()
        {
            return await _context.Images
                .Select(i => new ImageCreate{
                    Id = i.Id, 
                    ImageData = i.ImageData 
                }).ToListAsync();
        }

        public async Task<bool> UpdateImage(ImageEdit model)
        {
            Image image = await _context.Images.FindAsync(model.Id);

            if (image is null)
                return false;
            image.Id = model.Id;
            image.ImageData = model.ImageData;

            int numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}
