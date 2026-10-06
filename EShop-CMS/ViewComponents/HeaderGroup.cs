using EShop_CMS.DataBase.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EShop_CMS.ViewComponents
{
    public class HeaderGroup : ViewComponent
    {
        private readonly IProductGroupRepository _prorep;
        public HeaderGroup(IProductGroupRepository repository)
        {
            _prorep = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_prorep.GetAllGroups());
        }
    }
}
