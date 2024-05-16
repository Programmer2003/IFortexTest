using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context) => _context = context;

        public Task<User> GetUser()
        {
            return _context.Users.FirstAsync(u =>
                u.Id == _context.Orders
                .GroupBy(o => o.UserId)
                .OrderByDescending(o => o.Sum(o => o.Quantity * o.Price))
                .Select(r => r.Key)
                .First()
            );
        }

        public Task<List<User>> GetUsers()
        {
            return _context.Users
                .Where(u =>
                    u.Orders.Any(o => o.Status == OrderStatus.Paid && o.CreatedAt.Year == 2010)
                 )
                .ToListAsync();
        }
    }
}
