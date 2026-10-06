using EShop_CMS.DataBase.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EShop_CMS.ViewComponents
{
    public class Group : ViewComponent
    {
        private readonly IProductGroupRepository _prorep;
        public Group(IProductGroupRepository repository)
        {
            _prorep = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = _prorep.GetAllGroups()
    .OrderByDescending(g => g.products?.Count ?? 0)
    .Take(5)
    .ToList();
            return View(result);
        }

    }
}
