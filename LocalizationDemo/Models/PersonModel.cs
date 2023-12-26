using System.ComponentModel.DataAnnotations;

namespace LocalizationDemo.Models
{
    public class PersonModel
    {
        //[Display(Name = "First Name")]
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Range(6, 140, ErrorMessage = "Please enter your age.")]
        public int Age { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }
    }
}