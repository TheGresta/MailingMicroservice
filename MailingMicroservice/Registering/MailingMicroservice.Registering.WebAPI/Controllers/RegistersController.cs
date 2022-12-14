using MailingMicroservice.Registering.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MailingMicroservice.Registering.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            var result = await Mediator.Send(registerCommand);
            return Ok(result);
        }
    }
}
