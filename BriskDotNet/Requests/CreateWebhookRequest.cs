using Newtonsoft.Json;

namespace BriskDotNet.Requests
{
	public class CreateWebhookRequest
	{
		[JsonProperty("url")]
		public string URL { get; set; }

		[JsonProperty("bindings")]
		public string[] Bindings { get; set; }

		public CreateWebhookRequest(string url, params string[] bindings)
		{
			this.URL = url;
			this.Bindings = bindings;
		}
	}
}