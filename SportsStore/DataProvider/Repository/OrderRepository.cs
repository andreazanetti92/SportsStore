using Microsoft.EntityFrameworkCore;
using SportsStore.DataProvider.Interfaces;
using SportsStore.Models;

namespace SportsStore.DataProvider.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private StoreDBContext _context;

        public OrderRepository(StoreDBContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);


        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.Id == 0) _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
