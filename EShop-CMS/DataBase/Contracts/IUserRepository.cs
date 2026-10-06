using EShop_CMS.DataBase.Model;

namespace EShop_CMS.DataBase.Contracts
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        User GetUserByUsernameOrEmail(string usernameOrEmail);
        int AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        void DeleteUser(User user);
        bool IsExistEmail(string email);
        bool IsExistUsername(string userName);
        public User GetUserByIdNoTracking(int id);
    }
}
