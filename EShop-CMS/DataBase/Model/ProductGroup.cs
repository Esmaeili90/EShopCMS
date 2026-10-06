using System.ComponentModel.DataAnnotations;

namespace EShop_CMS.DataBase.Model
{
    public class ProductGroup:BaseModel
    {
        [Required(ErrorMessage ="Enter product group.")]
        public string GroupTitle { get; set; }
        public List<Product>? products { get; set; }

    }
}
