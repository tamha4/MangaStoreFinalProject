using MangaStore.Model.StoreModel;


namespace MangaStore.Service.StoreService
{
    public interface IStoreService
    {
        Task<bool> CreateStore(StoreCreate model);
        Task<List<StoreListItem>> GetAllStores();
        Task<StoreDetail> GetAllStoresById(int id);
        Task<bool> UpdateStores(StoreEdit model);
        Task<bool> DeleteStore(int id);

        Task<StoreEdit> GetStoreEditById(int id);
    }
}