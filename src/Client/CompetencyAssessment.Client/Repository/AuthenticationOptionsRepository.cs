namespace CompetencyAssessment.Client.Repository
{
    using System.Threading.Tasks;
    using CompetencyAssessment.Shared.DTOs;

    public class AuthenticationOptionsRepository
    {
        private readonly IHttpService httpService;
        private const string BaseUrl = "api/AuthenticationOptions";

        public AuthenticationOptionsRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<AuthenticationOptionsDto> Get()
        {
            return await this.httpService.GetHelper<AuthenticationOptionsDto>(BaseUrl);
        }
    }
}
