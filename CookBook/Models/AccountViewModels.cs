using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookBook.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Display(Name = "Zapamti me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required (ErrorMessage ="Morate  uneti ime")]
        [StringLength(100)]
        [Display(Name ="Ime")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Morate  uneti prezime")]
        [StringLength(100)]
        [Display(Name = "Prezime")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Morate  uneti e-mail")]
        [EmailAddress (ErrorMessage ="E-mail mora biti u ispravnom formatu")]
        [System.Web.Mvc.Remote("CheckExistingEmail", "Account", HttpMethod = "POST", ErrorMessage = "Email već postoji")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Morate  uneti lozinku")]
        [StringLength(100, ErrorMessage = " {0} mora imati najmanje {2} karaktera.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Lozinke moraju biti duge najmanje 8 karaktera i zadovoljiti barem 3 od 4 sledeća uslova: jedno veliko slovo, jedno malo slovo, jedan broj i jedan specijalni karakter")]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Morate ponoviti lozinku")]
        [DataType(DataType.Password)]
        [Display(Name = "Ponovite lozinku")]
        [Compare("Password", ErrorMessage = "Lozinka i ponovljena lozinka se ne poklapaju.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Morate  uneti korisničko ime")]
        [StringLength(100)]
        [System.Web.Mvc.Remote("CheckExistingUserName", "Account", HttpMethod = "POST", ErrorMessage = "Korisničko ime već postoji")]
        [Display(Name = "Korisničko ime")]
        public string UserName { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
