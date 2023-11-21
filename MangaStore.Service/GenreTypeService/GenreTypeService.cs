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

        public Task<bool> DeleteGenreType(int id)
        {
            throw new NotImplementedException();
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

        public Task<GenreTypeDetail> GetGenreTypeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateGenreType(GenreTypeEdit model)
        {
            throw new NotImplementedException();
        }
    }
}