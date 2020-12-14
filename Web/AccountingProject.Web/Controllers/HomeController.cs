namespace AccountingProject.Web.Controllers
{
    using System.Diagnostics;

    using AccountingProject.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult StatusCodeError(int errorCode)
        {
            if (errorCode == 404)
            {
                return this.View("StatusCodeError404");
            }
            else if (errorCode == 403)
            {
                return this.View("StatusCodeError403");
            }

            return this.View("StatusCodeErrorRest");
        }
    }
}
