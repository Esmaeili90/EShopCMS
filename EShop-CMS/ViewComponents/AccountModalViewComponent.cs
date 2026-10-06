using System.Security.Claims;
using EShop_CMS.DataBase.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EShop_CMS.ViewComponents
{
    public class AccountModalViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepo;

        public AccountModalViewComponent(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public IViewComponentResult Invoke()
        {
            // Not logged in? Render nothing.
            if (!(UserClaimsPrincipal?.Identity?.IsAuthenticated ?? false))
            {
                return Content(string.Empty);
            }

            var idClaim = UserClaimsPrincipal.FindFirst("UserId");
            if (idClaim == null || !int.TryParse(idClaim.Value, out int userId))
            {
                return Content(string.Empty);
            }

            var user = _userRepo.GetUserByIdNoTracking(userId);
            if (user == null)
            {
                return Content(string.Empty);
            }

            return View("_AccountModal", user);
        }
    }
}
