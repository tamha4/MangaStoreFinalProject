using System.Collections.Generic;
using System.Threading.Tasks;
using MangaStore.Model.ImageModel;
using MangaStore.Model.MangaModel;
using Microsoft.AspNetCore.Http;

namespace MangaStore.Service.ImageService
{
    public interface IImageService
    {
        Task<int?> CreateImage(ImageCreate model, bool isDuplicate = false);
        Task<List<ImageCreate>> GetImages();

        Task<ImageDetail> GetImageById(int id);

        Task<ImageEdit> GetImageEditById(int id);
        Task<bool> UpdateImage(ImageEdit model);

        Task<bool> ReplaceImage(int imageId, int? newImageId);
        Task<bool> DeleteIMage(int id);
        Task<bool> IsImageReplaced(int imageId);
Task<bool> UpdateAssociatedMangas(int imageId, int? newImageId);



    }
}
