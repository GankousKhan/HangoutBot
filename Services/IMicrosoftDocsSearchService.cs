using System.Threading.Tasks;
using HangoutBot.Models;

namespace HangoutBot.Services
{
    public interface IMicrosoftDocsSearchService
    {
        Task<MicrosoftDocsSearch> GetTopSearchResultsAsync(string term, int numOfResults);
    }
}