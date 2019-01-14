using GeorgisGarage.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeorgisGarage.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Constants.AdminAndOwnerRoleAuth)]
    public class AdministrationIndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
