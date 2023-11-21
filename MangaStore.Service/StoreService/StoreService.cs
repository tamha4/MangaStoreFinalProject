using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MangaStore.Data;
using MangaStore.Model.StoreModel;
using Microsoft.EntityFrameworkCore;

namespace MangaStore.Service.StoreService
{
    public class StoreService : IStoreService
    {
        private readonly MangaStoreDbContext _context;
        public StoreService(MangaStoreDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateStore(StoreCreate model)
        {
            if (!_context.Mangas.Any(m => m.Id == model.MangaId))
            {
                return false;
            }

            Store store = new Store()
            {
                Name = model.Name,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                MangaId = model.MangaId,
            };

            _context.Stores.Add(store);
            return await _context.SaveChangesAsync() == 1;
        }


        public Task<bool> DeleteStore(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<StoreListItem>> GetAllStores()
        {
            List<StoreListItem> stores = await _context.Stores
                .Include(s => s.Mangas)
                .Select(s => new StoreListItem()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    PhoneNumber = s.PhoneNumber,
                    MangaId = s.MangaId,
                    MangaName = s.MangaName,
                }).ToListAsync();
            return stores;
        }

        public Task<StoreDetail> GetAllStoresById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStores(StoreEdit model)
        {
            throw new NotImplementedException();
        }
    }
}