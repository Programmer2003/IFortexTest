using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context) => _context = context;

        public Task<Order> GetOrder()
        {
            return _context.Orders
                .OrderByDescending(o => o.CreatedAt)
                .FirstAsync(o => o.Quantity > 1);
        }

        public Task<List<Order>> GetOrders()
        {
            return _context.Orders
                .Where(o => o.User.Status == UserStatus.Active)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }
    }
}
