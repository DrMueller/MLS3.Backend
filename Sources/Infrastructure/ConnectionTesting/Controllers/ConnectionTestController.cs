using Microsoft.AspNetCore.Mvc;

namespace Mmu.Mls3.WebApi.Infrastructure.ConnectionTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionTestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetConnectionTest()
        {
            return "Hello Connection!";
        }
    }
}