using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HangoutBot.Models;
using HangoutBot.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HangoutBot
{
    public class Program
    {
        private static HttpClient _client = new HttpClient();
        private static IServiceProvider _serviceProvider;

        public static async Task Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("Nothing to search for.");
                return;
            }

            RegisterServices();
            var scope = _serviceProvider.CreateScope();

            MicrosoftDocsSearch searchResults = await scope.ServiceProvider.GetService<Commands>()
                .RunMicrosoftSearchAsync(
                    string.Join("%20", args).Replace("\"", string.Empty),
                    3);

            foreach (var result in searchResults.Results)
            {
                Console.WriteLine(result.Title);
                Console.WriteLine(result.Description);
                Console.WriteLine($"{result.Url}\n");
            }

            Console.WriteLine($"Do not see what you are looking for? More search results here: {searchResults.NextLink}");
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddHttpClient<IMicrosoftDocsSearchService, MicrosoftDocsSearchService>((provider, client) =>
            {
                client.BaseAddress = new Uri(@"https://docs.microsoft.com/api/search");
            });
            services.AddSingleton<Commands>();
            _serviceProvider = services.BuildServiceProvider(true);
        }
    }
}