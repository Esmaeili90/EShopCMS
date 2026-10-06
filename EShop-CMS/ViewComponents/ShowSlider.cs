using EShop_CMS.DataBase.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EShop_CMS.Components
{
    public class ShowSlider : ViewComponent
    {
        IProductRepository _proRepository;
        public ShowSlider(IProductRepository pro)
        {
            _proRepository = pro;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_proRepository.FindInSliders());
        }
    }
}
