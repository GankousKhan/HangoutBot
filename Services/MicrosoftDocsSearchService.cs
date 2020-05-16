using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HangoutBot.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace HangoutBot.Services
{
    public class MicrosoftDocsSearchService : IMicrosoftDocsSearchService
    {
        private readonly HttpClient _client;

        public MicrosoftDocsSearchService(HttpClient client)
        {
            _client = client;
        }

        public async Task<MicrosoftDocsSearch> GetTopSearchResultsAsync(string terms, int numOfResults = 10)
        {
            var queryStrings = new Dictionary<string, string>();
            queryStrings.Add("search", terms);
            queryStrings.Add("locale", "en-us");
            queryStrings.Add("$skip", "0");
            queryStrings.Add("$top", $"{numOfResults}");

            var httpResponse = await _client.GetAsync(
                new Uri(QueryHelpers.AddQueryString(_client.BaseAddress.ToString(), queryStrings)),
                HttpCompletionOption.ResponseHeadersRead);

            try
            {
                httpResponse.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                Console.WriteLine("Uh oh something happened with your search. Try again later.");
                return null;
            }

            var content = await httpResponse.Content.ReadAsStringAsync();

            return MicrosoftDocsSearch.FromJson(content);
        }
    }
}