using System.ComponentModel.DataAnnotations;

namespace EShop_CMS.DataBase.Model
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public int Sum { get; set; }

        public bool IsFinaly { get; set; }

        public bool IsCompleted { get; set; } = false;

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
