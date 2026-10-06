using EShop_CMS.DataBase.Context;
using EShop_CMS.DataBase.Contracts;
using EShop_CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EShop_CMS.Controllers
{
    public class HomeController : Controller
    {
        IProductGroupRepository _grorep;
        IProductRepository _prorep;
        
        public HomeController(IProductRepository ProductRep, IProductGroupRepository grorep)
        {
            _prorep = ProductRep;
            _grorep = grorep;
        }
        public IActionResult Index()
        {
            return View(_prorep.OrderProductsDec());
        }

      
        public IActionResult ShowByGroup(int groupId)
        {
            var Specgroup= _grorep.GetGroupById(groupId);
            ViewBag.Group = Specgroup.GroupTitle;
            var result= _prorep.GetAllProducts().Where(p => p.group == Specgroup).ToList();
            return View(result);
        }
    }
}
