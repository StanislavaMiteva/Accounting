namespace AccountingProject.Web.ViewModels.Transactions
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SearchInputModel
    {
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Type of the document")]
        public int? DocumentTypeId { get; set; }

        [Display(Name = "Counterparty Name")]
        public int? CounterpartyId { get; set; }

        public string Folder { get; set; }

        [Display(Name = "Consecutive Number")]
        public string ConsecutiveNumber { get; set; }

        public decimal? Amount { get; set; }

        [Display(Name = "Creator")]
        public string CreatorId { get; set; }

        public string Description { get; set; }
    }
}
