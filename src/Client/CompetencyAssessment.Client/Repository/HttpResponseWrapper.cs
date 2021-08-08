namespace CompetencyAssessment.Client.Repository
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T response, bool success, HttpResponseMessage httpResponseMessage)
        {
            this.Success = success;
            this.Response = response;
            this.HttpResponseMessage = httpResponseMessage;
        }

        public bool Success { get; set; }
        public T Response { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }

        public async Task<string> GetBody()
        {
            return await this.HttpResponseMessage.Content.ReadAsStringAsync();
        }
    }
}
