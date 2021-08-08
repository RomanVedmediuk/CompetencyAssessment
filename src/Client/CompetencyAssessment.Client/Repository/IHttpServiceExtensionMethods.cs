namespace CompetencyAssessment.Client.Repository
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class HttpServiceExtensionMethods
    {
        public static async Task<T> GetHelper<T>(this IHttpService httpService, string url)
        {
            var response = await httpService.Get<T>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }
    }
}
