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
            if (!ValidateModel(model))
                return false;

            var manga = new Manga()
            {
                Name = model.Name,
                Author = model.Author,
                Description = model.Description,
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



        public async Task<bool> DeleteManga(int id)
        {
            var mangaEntity = await _context.Mangas.FindAsync(id);

            if (mangaEntity is null)
                return false;

            _context.Mangas.Remove(mangaEntity);

            return await _context.SaveChangesAsync() == 1;
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
                    Description = s.Description,
                    Price = s.Price,
                    ItemsInStock = s.ItemsInStock,
                    GenreTypeId = s.GenreTypeId,
                    GenreTypeName = s.GenreTypeName,
                    ImageId = s.ImageId,
                    ImageFile = s.ImageFile,
                }).ToListAsync();

            return mangas;
        }


        public async Task<MangaDetail> GetMangaById(int id)
        {
            MangaDetail mangaDetail = await _context.Mangas
                .Include( s => s.Image)
                .Include( s => s.GenreType)
                .Include( s => s.Stores)
                .Where( s => s.Id == id)
                .Select( s => new MangaDetail()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Author = s.Author,
                    Description = s.Description,
                    Price = s.Price,
                    ItemsInStock = s.ItemsInStock,
                    GenreTypeId = s.GenreTypeId,
                    GenreTypeName = s.GenreTypeName,
                    ImageId = s.ImageId,
                    ImageFile = s.ImageFile,
            }).FirstOrDefaultAsync();

            return mangaDetail;
        }

        public async Task<bool> UpdateManga(MangaEdit model)
        {
            try
            {
                Manga manga = await _context.Mangas.FindAsync(model.Id);

                if (manga == null)
                    return false;

                manga.Name = model.Name;
                manga.Author = model.Author;
                manga.Description = model.Description;
                manga.Price = model.Price;
                manga.ItemsInStock = model.ItemsInStock;
                manga.GenreTypeId = model.GenreTypeId;
                // manga.GenreTypeName = model.GenreTypeName;
                manga.ImageId = model.ImageId;
                // manga.ImageFile = model.ImageFile;

                int numberOfChanges = await _context.SaveChangesAsync();

                return numberOfChanges == 1;
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error updating manga: {ex.Message}");
                return false;
            }
        }


        public async Task<MangaEdit> GetMangaEditById(int id)
        {
            MangaEdit mangaEdit = await _context.Mangas
                        .Include( s => s.Image)
                        .Include( s => s.GenreType)
                        .Where( s => s.Id == id)
                        .Select( s => new MangaEdit()
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Author = s.Author,
                            Description = s.Description,
                            Price = s.Price,
                            ItemsInStock = s.ItemsInStock,
                            GenreTypeId = s.GenreTypeId,
                            // GenreTypeName = s.GenreTypeName,
                            ImageId = s.ImageId,
                            // ImageFile = s.ImageFile,
                })
                .FirstOrDefaultAsync();

            return mangaEdit;
        }


    }
}
