﻿using Infrastructure.Security.Authentication.Cookie.Handlers;
using Infrastructure.Security.Model;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Threading;
using System.Threading.Tasks;

namespace Presentation.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CookieHandler sessionProvider;

        public HomeController(CookieHandler sessionProvider)
        {
            this.sessionProvider = sessionProvider;
        }

        public IActionResult Hata()
        {
            return View();
        }

        [Authorize(Roles = "StandardUser")]
        public async Task<IActionResult> Index(CancellationTokenSource cancellationTokenSource)
        {
            AuthenticatedUser user = await sessionProvider.GetLoggedInUserAsyc(cancellationTokenSource);

            return View();
        }

        [Authorize]
        [Route(nameof(Yetkisiz))]
        public IActionResult Yetkisiz()
        {
            return View();
        }
    }
}
