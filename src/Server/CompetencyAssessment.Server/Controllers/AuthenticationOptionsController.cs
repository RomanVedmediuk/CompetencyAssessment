namespace CompetencyAssessment.Server.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Shared.DTOs;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationOptionsController : ControllerBase
    {
        [HttpGet]
        public Task<ActionResult<AuthenticationOptionsDto>> Get()
        {
            var data = new AuthenticationOptionsDto(
                "ec6823f9-9e4f-4ca8-82ae-8b59d032bcd6",
                "https://login.microsoftonline.com/1e1a8a51-d445-488a-8808-4547dc91fe96",
                true);

            return Task.FromResult(new ActionResult<AuthenticationOptionsDto>(data));
        }
    }
}