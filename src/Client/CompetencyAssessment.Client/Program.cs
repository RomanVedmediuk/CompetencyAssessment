namespace CompetencyAssessment.Client
{
    using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Repository;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            await ConfigureServicesAsync(builder.Services, builder.HostEnvironment);
            await builder.Build().RunAsync();
        }

        private static async Task ConfigureServicesAsync(IServiceCollection services, IWebAssemblyHostEnvironment webAssemblyHostEnvironment)
        {
            services.AddHttpClient("CompetencyAssessment.ServerAPI.Private",
                client => client.BaseAddress = new Uri(webAssemblyHostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            services.AddHttpClient("CompetencyAssessment.ServerAPI.Public",
                client => client.BaseAddress = new Uri(webAssemblyHostEnvironment.BaseAddress));

            // Supply HttpClient instances that include access tokens when making requests to the server project
            services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("CompetencyAssessment.ServerAPI.Private"));

            services.AddScoped<IHttpService, HttpService>();

            await ConfigureMsalAuthenticationAsync(services);
        }

        private static async Task ConfigureMsalAuthenticationAsync(IServiceCollection services)
        {
            services.AddScoped(provider =>
            {
                var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("CompetencyAssessment.ServerAPI.Public");
                return new AuthenticationOptionsRepository(new HttpService(client));
            });

            var sp = services.BuildServiceProvider();
            var configRepository = sp.GetRequiredService<AuthenticationOptionsRepository>();
            var (clientId, authority, validateAuthority) = await configRepository.Get();

            services.AddMsalAuthentication(options =>
            {
                var authenticationOptions = options.ProviderOptions.Authentication;
                authenticationOptions.ClientId = clientId;
                authenticationOptions.Authority = authority;
                authenticationOptions.ValidateAuthority = validateAuthority;

                options.ProviderOptions.DefaultAccessTokenScopes.Add($"api://{clientId}/access_as_user");
            });
        }
    }
}
