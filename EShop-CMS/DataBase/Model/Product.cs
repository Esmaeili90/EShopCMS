using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;

namespace EShop_CMS.DataBase.Model
{
    public class Product:BaseModel
    {
        [Required]
        public string Title { get; set; }
        public string? Brand { get; set; }
        [Required]
        public string Description { get; set; }
        public string? ImgName { get; set; }
        public int Price { get; set; }
        public bool InSlider { get; set; }
        [ForeignKey("GroupId")]
        public ProductGroup? group { get; set; }
        public bool IsPresent { get; set; }


    }
}
