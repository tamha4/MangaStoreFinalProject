using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MangaStore.Model.GenreTypeModel;

namespace MangaStore.Service.GenreTypeService
{
    public interface IGenreTypeService
    {
        Task<bool> CreateGenreType(GenreTypeCreate model);
        Task<List<GenreTypeListItem>> GetAllGenreTypes();
        Task<GenreTypeDetail> GetGenreTypeById(int id);
        Task<GenreTypeEdit> GetTypeEditById(int id);
        Task<bool> UpdateGenreType(GenreTypeEdit model);
        Task<bool> DeleteGenreType(int id);
    }
}