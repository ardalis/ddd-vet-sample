using Microsoft.AspNetCore.Mvc;

namespace FrontDesk.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
    }
}
