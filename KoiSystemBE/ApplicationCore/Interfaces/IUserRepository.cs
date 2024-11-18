using ApplicationCore.Dto;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUserRepository
    {
        User GetByEmail(string email);
        void Add(User user);
    }
}
