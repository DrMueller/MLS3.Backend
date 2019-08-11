using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mls3.Localization.Areas.Localization.Services;

namespace Mmu.Mls3.WebApi.Areas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HelloController : ControllerBase
    {
        private readonly ILocalizationServiceFactory _localizationServiceFactory;

        public HelloController(ILocalizationServiceFactory localizationServiceFactory)
        {
            _localizationServiceFactory = localizationServiceFactory;
        }

        [HttpGet("{name}")]
        public ActionResult SayHello(string name)
        {
            var service1 = _localizationServiceFactory.CreateFor(GetType());
            var service2 = _localizationServiceFactory.CreateFor(GetType());
            var english = service1.Localize("Hello", name);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");
            var german = service1.Localize("Hello", name);

            return Ok(english);
        }
    }
}