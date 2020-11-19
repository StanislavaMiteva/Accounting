namespace AccountingProject.Web.ViewModels.Counterparties
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCounterpartyInputModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "The field Name must be between 2 and 100 characters long.")]
        [MaxLength(100, ErrorMessage = "The field Name must be between 2 and 100 characters long.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "The field VAT must contain only digits.")]
        [MinLength(15, ErrorMessage = "The field VAT must be between 15 and 20 characters long.")]
        [MaxLength(20, ErrorMessage = "The field VAT must be between 15 and 20 characters long.")]
        public string VAT { get; set; }

        [MinLength(5, ErrorMessage = "The field Address must be between 5 and 200 characters long.")]
        [MaxLength(200, ErrorMessage = "The field Address must be between 5 and 200 characters long.")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City Name")]
        [MinLength(2, ErrorMessage = "The field City Name must be between 2 and 50 characters long.")]
        [MaxLength(50, ErrorMessage = "The field City Name must be between 2 and 50 characters long.")]
        public string CityName { get; set; }
    }
}
