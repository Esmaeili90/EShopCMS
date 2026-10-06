using EShop_CMS.DataBase.Context;
using EShop_CMS.DataBase.Contracts;
using EShop_CMS.DataBase.Model;
using Microsoft.EntityFrameworkCore;
using MyCMS.Application.Security;

namespace EShop_CMS.DataBase.Repository
{
    public class UserRepository : IUserRepository
    {
        DefaultContext _context;
        public UserRepository(DefaultContext context)
        {
            _context = context;
        }
        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public void DeleteUser(int userId)
        {
            var user=GetUserById(userId);
            DeleteUser(user);
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserByUsernameOrEmail(string usernameOrEmail)
        {
            return _context.Users.FirstOrDefault(u =>
     !u.IsDelete &&
     (u.Email == usernameOrEmail || u.UserName == usernameOrEmail));
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public bool IsExistUsername(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public User GetUserByIdNoTracking(int id)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
        }
    }
}
