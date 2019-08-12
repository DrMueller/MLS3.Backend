using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mmu.Mls3.Common.Areas.Localization.Services;
using Mmu.Mls3.WebApi.Areas.Web.Dtos;

namespace Mmu.Mls3.WebApi.Areas.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HelloController : ControllerBase
    {
        private const string LocalizationKeyHello = "Hello";

        private readonly ILocalizationServiceFactory _localizationServiceFactory;

        public HelloController(ILocalizationServiceFactory localizationServiceFactory)
        {
            _localizationServiceFactory = localizationServiceFactory;
        }

        [HttpGet("{name}")]
        public ActionResult<string> SayHello(string name)
        {
            var service1 = _localizationServiceFactory.CreateFor(GetType());
            var localizedHello = service1.Localize(LocalizationKeyHello, name);

            var response = new HelloResponseDto
            {
                HelloMessage = localizedHello
            };

            return Ok(response);
        }
    }
}