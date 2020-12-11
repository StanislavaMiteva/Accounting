namespace AccountingProject.Web.ViewModels.DocumentTypes
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class EditDocumentTypeInputModel :
        CreateDocumentTypeInputModel,
        IMapFrom<DocumentType>
    {
        public int Id { get; set; }
    }
}
