namespace CompetencyAssessment.Server.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Shared.DTOs;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationOptionsController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AuthenticationOptionsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpGet]
        public Task<ActionResult<AuthenticationOptionsDto>> Get()
        {
            var section = this.configuration.GetSection("AzureAd");
            var data = new AuthenticationOptionsDto(
                section["ClientId"], 
                "https://login.microsoftonline.com/"+section["TenantId"],
                true);

            return Task.FromResult(new ActionResult<AuthenticationOptionsDto>(data));
        }
    }
}