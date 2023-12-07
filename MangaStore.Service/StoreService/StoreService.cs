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


        public async Task<bool> DeleteStore(int id)
        {
            var storeEntity = await _context.Stores.FindAsync(id);

            if(storeEntity is null)
                return false;

            _context.Stores.Remove(storeEntity);

            return await _context.SaveChangesAsync() == 1;
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

        public async Task<StoreDetail> GetAllStoresById(int id)
        {
            StoreDetail storeDetail = await _context.Stores
                .Where(s => s.Id == id)
                .Select(s => new StoreDetail()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    PhoneNumber = s.PhoneNumber,
                    MangaId = s.MangaId,
                }).FirstOrDefaultAsync();
            
            return storeDetail;
        }

        public async Task<StoreEdit> GetStoreEditById(int id)
        {
            StoreEdit storeEdit = await _context.Stores
                .Where(s => s.Id == id)
                .Select(s => new StoreEdit()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    PhoneNumber = s.PhoneNumber,
                    MangaId = s.MangaId,
                }).FirstOrDefaultAsync();
            
            return storeEdit;
        }

        public async Task<bool> UpdateStores(StoreEdit model)
        {
            Store store = await _context.Stores.FindAsync(model.Id);

            if(store is null)
                return false;
            
            store.Name = model.Name;
            store.Address = model.Address;
            store.PhoneNumber = model.PhoneNumber;
            store.MangaId = model.MangaId;

            int numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }
    }
}