using System.Collections.Generic;
using System.Threading.Tasks;
using MangaStore.Model.ImageModel;
using MangaStore.Model.MangaModel;
using Microsoft.AspNetCore.Http;

namespace MangaStore.Service.ImageService
{
    public interface IImageService
    {
        Task<bool> CreateImage(ImageCreate model);
        Task<List<ImageCreate>> GetImages();

        Task<ImageDetail> GetImageById(int id);

        Task<ImageEdit> GetImageEditById(int id);
        Task<bool> UpdateImage(ImageEdit model);
        Task<bool> DeleteIMage(int id);
    }
}
