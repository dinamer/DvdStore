using System.ComponentModel.DataAnnotations;

namespace DvdStore.ViewModels
{

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

   
}
