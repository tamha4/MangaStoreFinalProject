using System.Collections.Generic;
using System.Threading.Tasks;
using MangaStore.Model.ImageModel;

namespace MangaStore.Service.ImageService
{
    public interface IImageService
    {
        Task<bool> CreateImage(ImageCreate model);
        Task<List<ImageCreate>> GetImages();
    }
}
