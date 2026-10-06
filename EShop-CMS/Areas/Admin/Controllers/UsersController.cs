using EShop_CMS.DataBase.Contracts;
using EShop_CMS.DataBase.Model;
using EShop_CMS.DataBase.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCMS.Application.Security;

namespace EShop_CMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy ="AdminOnly")]
    public class UsersController : Controller
    {
        #region Ctor
        IUserRepository _userrep;
        public UsersController(IUserRepository userRep)
        {
            _userrep = userRep;
        }
        #endregion
        [Route("UserManage")]
        public IActionResult Index()
        {
            var result = _userrep.GetAllUsers();
            return View(result);
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            _userrep.AddUser(user);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteUser(int id)
        {
            _userrep.DeleteUser(id);
            return RedirectToAction("Index");
        }
        
        public IActionResult EditUser(int Id)
        {
            var user = _userrep.GetUserById(Id);
            if (user == null)
                return NotFound();

            user.PassWord = ""; 
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(int id, [Bind("UserName,PassWord,Email,IsAdmin,Id,CreateDate,DateModified,IsDelete")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(user.PassWord))
            {
                ModelState.Remove(nameof(user.PassWord));
            }

            if (ModelState.IsValid)
            {
                var lastuser = _userrep.GetUserByIdNoTracking(id); // بدون Tracking
                if (lastuser == null)
                {
                    return NotFound();
                }

                if (string.IsNullOrWhiteSpace(user.PassWord))
                {
                    user.PassWord = lastuser.PassWord;
                }
                else
                {
                    user.PassWord = PasswordHasher.HashPassword(user.PassWord);
                }

                user.DateModified = DateTime.Now;
                _userrep.UpdateUser(user);
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }
    }
}
