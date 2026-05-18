using System.ComponentModel.DataAnnotations;

namespace Obs.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanici adi zorunludur.")]
        [Display(Name = "Kullanici adi")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sifre zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Sifre")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Beni hatirla")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
