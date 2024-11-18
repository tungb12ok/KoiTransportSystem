using ApplicationCore.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly KoiFishTransportationDBContext _context;

        public UserRepository(KoiFishTransportationDBContext context)
        {
            _context = context;
        }

        public User GetByEmail(string email)
        {
            return _context.Users
                .Include(x => x.Role)
                .FirstOrDefault(u => u.Email == email);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
