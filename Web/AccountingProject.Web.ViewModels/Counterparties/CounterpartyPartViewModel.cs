﻿namespace AccountingProject.Web.ViewModels.Counterparties
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class CounterpartyPartViewModel : IMapFrom<Counterparty>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
