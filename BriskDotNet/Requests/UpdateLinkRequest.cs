using Newtonsoft.Json;

namespace BriskDotNet.Requests
{
	public class UpdateLinkRequest
	{
		[JsonProperty("slug")]
		public string Slug { get; set; }

		[JsonProperty("url")]
		public string URL { get; set; }

		[JsonProperty("ttl")]
		public int TTL { get; set; }

		public UpdateLinkRequest(string slug)
		{
			this.Slug = slug;
			this.TTL = -1;
		}
	}
}