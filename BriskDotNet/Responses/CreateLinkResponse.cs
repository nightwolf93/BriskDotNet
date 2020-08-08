using Newtonsoft.Json;

namespace BriskDotNet.Responses
{
    public class CreateLinkResponse
    {
        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }
}