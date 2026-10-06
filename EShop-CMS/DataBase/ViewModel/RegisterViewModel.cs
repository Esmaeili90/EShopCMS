using System.ComponentModel.DataAnnotations;

namespace EShop_CMS.DataBase.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter your E-Mail.")]
        [MaxLength(250)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your username.")]
        [MaxLength(250)]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter your password.")]
        [MaxLength(250)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please repeat your password.")]
        [MaxLength(250)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Paswords aren't match.")]
        public string RepeatPassword { get; set; }
    }
}
