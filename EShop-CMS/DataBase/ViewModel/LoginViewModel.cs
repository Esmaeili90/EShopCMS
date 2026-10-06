using System.ComponentModel.DataAnnotations;

namespace EShop_CMS.DataBase.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your username or E-Mail.")]
        public string EmailOrUserName { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool RememberMe { get; set; }
    }
}
