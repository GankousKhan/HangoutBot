using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HangoutBot.Models
{
    public partial class MicrosoftDocsSearch
    {
        [JsonProperty("results")] public Result[] Results { get; set; }

        [JsonProperty("@nextLink")] public Uri NextLink { get; set; }
    }

    public class Result
    {
        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("url")] public Uri Url { get; set; }

        [JsonProperty("description")] public string Description { get; set; }
    }

    public partial class MicrosoftDocsSearch
    {
        public static MicrosoftDocsSearch FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MicrosoftDocsSearch>(json, Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this MicrosoftDocsSearch self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
            }
        };
    }
}