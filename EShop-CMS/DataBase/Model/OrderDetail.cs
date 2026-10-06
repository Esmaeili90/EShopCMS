using System.ComponentModel.DataAnnotations;

namespace EShop_CMS.DataBase.Model
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public int Price { get; set; }



        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
