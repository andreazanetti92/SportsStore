using SportsStore.DataProvider.Interfaces;
using SportsStore.Models;

namespace SportsStore.DataProvider.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private StoreDBContext _ctx;
        public StoreRepository(StoreDBContext context)
        {
            _ctx = context;
        }

        public IQueryable<Product> Products => _ctx.Products;
    }
}
