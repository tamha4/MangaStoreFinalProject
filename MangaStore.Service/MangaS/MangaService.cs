using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO; // Add this using directive for Path
using System.Threading.Tasks;
using MangaStore.Model.MangaModel;
using MangaStore.Data;
using Microsoft.AspNetCore.Http;
namespace MangaStore.Service.MangaS
{
    public class MangaService : IMangaService
    {
        private readonly MangaStoreDbContext _context;
        public MangaService(MangaStoreDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateManga(MangaCreate model)
        {
            // Ensure the model state is valid before proceeding
            if (!ValidateModel(model))
                return false;

            var manga = new Manga()
            {
                Name = model.Name,
                Author = model.Author,
                Price = model.Price,
                ItemsInStock = model.ItemsInStock,
                GenreTypeId = model.GenreTypeId,
                ImageId = model.ImageId,
            };



            _context.Mangas.Add(manga);
            return await _context.SaveChangesAsync() == 1;
        }

        private bool ValidateModel(MangaCreate model)
        {
            // Add any additional validation logic here
            // Return false if validation fails

            return true;
        }

        private async Task<byte[]> ConvertImageFileToByteArray(IFormFile imageFile)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await imageFile.CopyToAsync(ms);
                return ms.ToArray();
            }
        }



        public Task<bool> DeleteManga(int id)
        {
            throw new NotImplementedException();
        }

    public async Task<List<MangaListItem>> GetAllMangas()
    {
        List<MangaListItem> mangas = await _context.Mangas
            .Include(s => s.Image)
            .Include(s => s.GenreType)
            .Include(s => s.Stores) 
            .Select(s => new MangaListItem()
            {
                Id = s.Id,
                Name = s.Name,
                Author = s.Author,
                Price = s.Price,
                ItemsInStock = s.ItemsInStock,
                GenreTypeId = s.GenreTypeId,
                GenreTypeName = s.GenreTypeName,
                ImageId = s.ImageId,
            }).ToListAsync();

        return mangas;
    }


        public Task<MangaDetail> GetMangaById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateManga(MangaEdit model)
        {
            throw new NotImplementedException();
        }
    }
}
