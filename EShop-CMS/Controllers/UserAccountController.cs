using EShop_CMS.DataBase.Contracts;
using EShop_CMS.DataBase.Model;
using EShop_CMS.DataBase.Repository;
using EShop_CMS.DataBase.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using MyCMS.Application.Security;
using System.Security.Claims;

namespace EShop_CMS.Controllers
{
    public class UserAccountController : Controller
    {
        IUserRepository _userrep;
        public UserAccountController(IUserRepository userRep)
        {
            _userrep = userRep;
        }

        #region DoLogin

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);
            login.RememberMe = true;
            var user = _userrep.GetUserByUsernameOrEmail(login.EmailOrUserName);
            if (user == null)
            {
                ModelState.AddModelError("EmailOrUserName", "User not found.");
                return View(login);
            }

            if (!PasswordHasher.VerifyHashedPassword(user.PassWord, login.Password))
            {
                ModelState.AddModelError("EmailOrUserName", "User not found.");
                return View(login);
            }

            //Login
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim("UserId",user.Id.ToString()),
                new Claim("IsAdmin",user.IsAdmin.ToString())


            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };

            HttpContext.SignInAsync(principal, properties);
            return Redirect("/");
        }
        #endregion
        #region DoRegister
        [HttpGet]

        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (_userrep.IsExistUsername(registerVM.UserName))
            {
                ModelState.AddModelError("UserName", "Username isn't valid.");
                return View(registerVM);
            }

            if (_userrep.IsExistEmail(registerVM.Email.ToLower().Trim()))
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(registerVM);
            }
            User user = new User();
            user.UserName = registerVM.UserName;
            user.Email = registerVM.Email;
            user.DataCreated = DateTime.Now;
            user.PassWord = PasswordHasher.HashPassword(registerVM.Password);
            user.IsAdmin = false;
            user.IsDelete = false;
            _userrep.AddUser(user);



            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region UpdateProfile
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(
            int Id,
            string UserName,
            string Email,
            string CurrentPassword,
            string NewPassword,
            string ConfirmPassword)
        {
            var user = _userrep.GetUserById(Id);
            if (user == null)
            {
                return NotFound();
            }

            // Basic required-field checks
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Email))
            {
                TempData["AccountError"] = "Username and email are required.";
                return RedirectToAction("Index", "Home");
            }

            user.UserName = UserName;
            user.Email = Email;

            // Only touch the password if the user is actually trying to change it
            bool wantsPasswordChange = !string.IsNullOrWhiteSpace(NewPassword) || !string.IsNullOrWhiteSpace(ConfirmPassword);

            if (wantsPasswordChange)
            {
                if (string.IsNullOrWhiteSpace(CurrentPassword) || !PasswordHasher.VerifyHashedPassword(user.PassWord, CurrentPassword))
                {
                    TempData["AccountError"] = "Current password is incorrect.";
                    return RedirectToAction("Index", "Home");
                }

                if (NewPassword.Length < 8)
                {
                    TempData["AccountError"] = "New password must be at least 8 characters.";
                    return RedirectToAction("Index", "Home");
                }

                if (NewPassword != ConfirmPassword)
                {
                    TempData["AccountError"] = "New password and confirmation do not match.";
                    return RedirectToAction("Index", "Home");
                }

                user.PassWord = PasswordHasher.HashPassword(NewPassword);
            }

            _userrep.UpdateUser(user);

            TempData["AccountSuccess"] = "Your account was updated successfully.";
            return RedirectToAction("Index", "Home");
        }
        #endregion

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

    }
}
