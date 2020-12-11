namespace AccountingProject.Web.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;

    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.GLAccounts;
    using AccountingProject.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class ListMainAccountsViewComponent : ViewComponent
    {
        private readonly IMainAccountsService mainAccountsService;

        public ListMainAccountsViewComponent(IMainAccountsService mainAccountsService)
        {
            this.mainAccountsService = mainAccountsService;
        }

        public IViewComponentResult Invoke(string typeOfAccount, int mainAccountId)
        {
            if (typeOfAccount == "all")
            {
                var viewModel = new ListOfMainAccountsViewModel
                {
                    MainAccountId = mainAccountId,
                    MainAccounts = this.mainAccountsService
                            .GetAllAsKeyValuePairs(),
                };
                return this.View(viewModel);
            }
            else if (typeOfAccount == "inventory")
            {
                var viewModel = new ListOfMainAccountsViewModel
                {
                    MainAccountId = mainAccountId,
                    MainAccounts = this.mainAccountsService
                            .GetInventoryAccounts<MainAccountPartViewModel>()
                            .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), $"{x.Code} {x.Name}")),
                };
                return this.View(viewModel);
            }

            return this.View();
        }
    }
}
