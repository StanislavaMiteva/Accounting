﻿namespace AccountingProject.Web.Controllers
{
    using System.Threading.Tasks;

    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.GLAccounts;
    using Microsoft.AspNetCore.Mvc;

    public class MainAccountsController : Controller
    {
        private readonly IGLAccountsService mainAccountsService;

        public MainAccountsController(IGLAccountsService mainAccountsService)
        {
            this.mainAccountsService = mainAccountsService;
        }

        // MainAccounts/Create
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMainAccountInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.mainAccountsService.CreateAsync(input);

            // TODO: Redirect to all info page
            return this.Redirect("/");
        }
    }
}
