using System.ComponentModel.DataAnnotations;

namespace EShop_CMS.DataBase.Model
{
    public class User:BaseModel
    {
        [Required(ErrorMessage ="Please enter your username.")]
        [MaxLength(250)]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter your password.")]
        [MaxLength(250)]
        [MinLength(8)]
        public string PassWord { get; set; }
        [Required(ErrorMessage = "Please enter your E-Mail.")]
        [MaxLength(250)]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public float? CreditValue { get; set; } = 100;
        public bool IsVerifyed { get; set; } = false;
    }
}
