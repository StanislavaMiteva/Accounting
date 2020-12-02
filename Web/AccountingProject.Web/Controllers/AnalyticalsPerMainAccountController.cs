namespace AccountingProject.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.AnalyticalAccounts;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticalsPerMainAccountController : ControllerBase
    {
        private readonly IAnalyticalAccountsService analyticalAccountsService;

        public AnalyticalsPerMainAccountController(IAnalyticalAccountsService analyticalAccountsService)
        {
            this.analyticalAccountsService = analyticalAccountsService;
        }

        [HttpGet("{id}")]
        public IEnumerable<AnalyticalAccountPartViewModel> Get(int id)
        {
            return this.analyticalAccountsService
                .GetAllByMainAccountId<AnalyticalAccountPartViewModel>(id)
                .OrderBy(x => x.Name);
        }
    }
}
