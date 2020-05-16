using System;
using System.Threading.Tasks;
using HangoutBot.Models;
using HangoutBot.Services;

namespace HangoutBot
{
    public class Commands
    {
        private readonly IMicrosoftDocsSearchService _mdss;

        public Commands(IMicrosoftDocsSearchService mdss)
        {
            _mdss = mdss;
        }

        public async Task<MicrosoftDocsSearch> RunMicrosoftSearchAsync(string term, int numOfResults)
            => await _mdss.GetTopSearchResultsAsync(term, numOfResults);
    }
}