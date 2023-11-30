using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MangaStore.Data;
using MangaStore.Model.GenreTypeModel;
using Microsoft.EntityFrameworkCore;


namespace MangaStore.Service.GenreTypeService
{
    public class GenreTypeService : IGenreTypeService
    {
        private readonly MangaStoreDbContext _context;
        public GenreTypeService(MangaStoreDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateGenreType(GenreTypeCreate model)
        {
            GenreType genreType = new GenreType()
            {
                GenreName = model.GenreName,
            };
            _context.GenreType.Add(genreType);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteGenreType(int id)
        {
            var genreTypeEntity = await _context.GenreType.FindAsync(id);

            if(genreTypeEntity is null)
                return false;

            _context.GenreType.Remove(genreTypeEntity);

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<List<GenreTypeListItem>> GetAllGenreTypes()
        {
            List<GenreTypeListItem> genreTypes = await _context.GenreType
                .Select(m => new GenreTypeListItem()
                {
                    Id = m.Id,
                    GenreName = m.GenreName,
                }).ToListAsync();
            return genreTypes;
        }

        public async Task<GenreTypeDetail> GetGenreTypeById(int id)
        {
            GenreTypeDetail genreTypeDetail = await _context.GenreType
                .Where(m => m.Id == id)
                .Select(m => new GenreTypeDetail()
                {
                    Id = m.Id,
                    GenreName = m.GenreName,
                }).FirstOrDefaultAsync();

            return genreTypeDetail;
        }

        public async Task<bool> UpdateGenreType(GenreTypeEdit model)
        {
            GenreType genreType = await _context.GenreType.FindAsync(model.Id);

            if (genreType is null)
                return false;

            genreType.GenreName = model.GenreName;

            int numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<GenreTypeEdit> GetTypeEditById(int id)
        {
            GenreTypeEdit genreTypeEdit = await _context.GenreType
                .Where(m => m.Id == id)
                .Select(m => new GenreTypeEdit()
                {
                    Id = m.Id,
                    GenreName = m.GenreName,
                }).FirstOrDefaultAsync();

            return genreTypeEdit;

        }



    }   
}