﻿namespace AccountingProject.Web.ViewModels.GLAccounts
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class MainAccountPartViewModel : IMapFrom<GLAccount>
    {
        public int Id { get; set; }

        public int Code { get; set; }

        public string Name { get; set; }
    }
}
