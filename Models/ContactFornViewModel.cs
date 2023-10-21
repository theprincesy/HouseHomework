using System.ComponentModel.DataAnnotations;
public class ContactFormViewModel
{       
        [Required(ErrorMessage = "Please enter your name.")]
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter your message.")]
        [Display(Name = "Message")]
        public string? Message { get; set; }
}