namespace AccountingProject.Web.ViewModels.DocumentTypes
{
    using System.ComponentModel.DataAnnotations;

    public class CreateDocumentTypeInputModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "The field Name must be between 2 and 50 characters long.")]
        [MaxLength(50, ErrorMessage = "The field Name must be between 2 and 50 characters long.")]
        public string Name { get; set; }
    }
}
