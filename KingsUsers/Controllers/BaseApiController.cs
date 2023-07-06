
using KingsUsers.Filters;
using Microsoft.AspNetCore.Mvc;

namespace KingsUsers.Controllers
{
    [ApiController]
    [ApiExceptionFilter]
    [Route("api/[controller]")]

    public class BaseApiController : ControllerBase
    {
    }
}