﻿namespace AccountingProject.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    using AccountingProject.Web.ViewModels.Identity;

    public class IndexViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
