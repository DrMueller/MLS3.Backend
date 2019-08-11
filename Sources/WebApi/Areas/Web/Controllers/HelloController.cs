using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mls3.Localization.Areas.Localization.Services;
using Mmu.Mls3.WebApi.Areas.Web.Dtos;

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
        public ActionResult<string> SayHello(string name)
        {
            var service1 = _localizationServiceFactory.CreateFor(GetType());
            var service2 = _localizationServiceFactory.CreateFor(GetType());
            var localizedHello = service1.Localize("Hello", name);

            var response = new HelloResponseDto
            {
                HelloMessage = localizedHello
            };

            return Ok(response);
        }
    }
}