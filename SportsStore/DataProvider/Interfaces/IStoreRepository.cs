using SportsStore.Models;

namespace SportsStore.DataProvider.Interfaces
{
    public interface IStoreRepository
    {
        public IQueryable<Product> Products { get; }
    }
}
