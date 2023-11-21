using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MangaStore.Data;
using MangaStore.Model.ImageModel;
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

        public async Task<List<ImageCreate>> GetImages()
        {
            return await _context.Images.Select(i => new ImageCreate { ImageData = i.ImageData }).ToListAsync();
        }

    }
}
