using EShop_CMS.DataBase.Model;

namespace EShop_CMS.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int OutOfStockCount { get; set; }
        public int TotalGroups { get; set; }
        public int TotalUsers { get; set; }

        public List<Product> RecentProducts { get; set; }
        public List<ProductGroup> Groups { get; set; }
    }
}
