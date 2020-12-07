namespace AccountingProject.Web.ViewComponents
{
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

        public IViewComponentResult Invoke(string typeOfAccount)
        {
            if (typeOfAccount == "all")
            {
                var viewModel = new ListOfMainAccountsViewModel
                {
                    MainAccounts = this.mainAccountsService
                            .GetAll<MainAccountPartViewModel>(),
                };
                return this.View(viewModel);
            }
            else if (typeOfAccount == "inventory")
            {
                var viewModel = new ListOfMainAccountsViewModel
                {
                    MainAccounts = this.mainAccountsService
                            .GetInventoryAccounts<MainAccountPartViewModel>(),
                };
                return this.View(viewModel);
            }

            return this.View();
        }
    }
}
