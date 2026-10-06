using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;

namespace EShop_CMS.DataBase.Model
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDelete { get; set; }
    }
}