using System;
using Newtonsoft.Json;

namespace BriskDotNet.Requests
{
    public class CreateLinkRequest
    {
        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("ttl")]
        public int TTL { get; set; }

        [JsonProperty("slug_length")]
        public int SlugLength { get; set; }

        public CreateLinkRequest(string url, int ttl, int slugLength)
        {
            this.URL = url;
            this.TTL = ttl;
            this.SlugLength = slugLength;
        }
    }
}