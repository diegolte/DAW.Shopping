using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UESAN.Shopping.Core.Entities;
using UESAN.Shopping.Core.Interfaces;
using UESAN.Shopping.Infrastructure.Data;

namespace UESAN.Shopping.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreDbContext _dbContext;

        public UserRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> SignUp(User user)
        {
            await _dbContext.User.AddAsync(user);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<User> SignIn(String email)
        {
            return await _dbContext
                        .User
                        .Where(x => x.Email == email)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext
                         .User
                         .Where(x => x.IsActive == true)
                         .ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _dbContext
                        .User
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(User user)
        {
            _dbContext.User.Update(user);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var user = await _dbContext
                            .User
                            .Where(x => x.Id == id)
                            .FirstOrDefaultAsync();
            if (user == null)
                return false;

            user.IsActive = false;
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
