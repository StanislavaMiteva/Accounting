namespace AccountingProject.Web.ViewModels.DocumentTypes
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class DocumentTypeViewModel : IMapFrom<DocumentType>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
