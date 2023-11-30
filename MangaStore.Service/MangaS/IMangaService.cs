using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MangaStore.Data; 
using MangaStore.Model.MangaModel;

namespace MangaStore.Service.MangaS
{
    public interface IMangaService
    {
        Task<bool> CreateManga(MangaCreate model);
        Task<List<MangaListItem>> GetAllMangas();
        Task<MangaDetail> GetMangaById(int id);
        Task<MangaEdit> GetMangaEditById(int id);
        Task<bool> UpdateManga(MangaEdit model);
        Task<bool> DeleteManga(int id);
    }
}