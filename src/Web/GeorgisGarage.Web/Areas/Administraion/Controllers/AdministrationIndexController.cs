using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeorgisGarage.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeorgisGarage.Web.Areas.Administraion.Controllers
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
