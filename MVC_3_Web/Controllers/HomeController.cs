using Microsoft.AspNetCore.Mvc;
using MVC_3.ViewModels;
using MVC_3.Serives;
using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace MVC_3.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedSerives scoped01;
        private readonly IScopedSerives scoped02;
        private readonly ISingletonSerives singleton01;
        private readonly ISingletonSerives singleton02;
        private readonly ITransientServies transient01;
        private readonly ITransientServies transient02;

        public HomeController(

            ILogger<HomeController> logger,
			 IScopedSerives scoped01,
            IScopedSerives scoped02,
            ISingletonSerives singleton01,
            ISingletonSerives singleton02,
            ITransientServies transient01,
            ITransientServies transient02)
           
        {
            _logger = logger;
            this.scoped01 = scoped01;
            this.scoped02 = scoped02;
            this.singleton01 = singleton01;
            this.singleton02 = singleton02;
            this.transient01 = transient01;
            this.transient02 = transient02;
        }

        public string? TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"scoped01:{this.scoped01.GetGuid()}\n");
            builder.Append($"scoped02:{this.scoped02.GetGuid()}\n\n");
            builder.Append($"singleton01:{this.singleton01.GetGuid()}\n");
            builder.Append($"singleton02:{this.singleton02.GetGuid()}\n\n");
            builder.Append($"transient01:{this.transient01.GetGuid()}\n");
            builder.Append($"transient02:{this.transient02.GetGuid()}\n\n");

            return builder.ToString();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
