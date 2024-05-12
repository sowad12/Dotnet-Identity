using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManager.Library.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "The New Password field is required")]
        [JsonProperty("New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "The Confirm New Password field is required")]
        [Compare(nameof(NewPassword), ErrorMessage = "Password did not match.")]
        [JsonProperty("Confirm New Password")]
        public string ConfirmNewPassword { get; set; }

        public string Token { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string ReturnUrl { get; set; }
    }
   
}
