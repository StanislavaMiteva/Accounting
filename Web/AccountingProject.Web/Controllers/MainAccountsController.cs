using AccountingProject.Services.Data;
using AccountingProject.Web.ViewModels.GLAccounts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingProject.Web.Controllers
{
    public class MainAccountsController : Controller
    {
        private readonly IGLAccountsService gLAccountsService;

        public MainAccountsController(IGLAccountsService gLAccountsService)
        {
            this.gLAccountsService = gLAccountsService;
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

            await this.gLAccountsService.CreateAsync(input);

            // TODO: Redirect to all info page
            return this.Redirect("/");
        }
    }
}
