using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BriskDotNet.Exceptions;
using BriskDotNet.Requests;
using BriskDotNet.Responses;
using Newtonsoft.Json;

namespace BriskDotNet
{
	public class BriskClient
	{
		private HttpClient client;

		private readonly string baseUrl;
		private readonly string clientId;
		private readonly string clientSecret;

		/// <summary>
		/// Initialze a new BriskAPI client
		/// </summary>
		/// <param name="baseUrl">The base url of your brisk server</param>
		/// <param name="clientId">The ClientID (can be the master)</param>
		/// <param name="clientSecret">The ClientSecret (can be the master)</param>
		public BriskClient(string baseUrl, string clientId, string clientSecret)
		{
			this.client = new HttpClient();
			this.baseUrl = baseUrl;
			if (this.baseUrl.EndsWith("/"))
			{
				this.baseUrl = this.baseUrl.Substring(0, this.baseUrl.Length - 2);
			}
			this.clientId = clientId;
			this.clientSecret = clientSecret;

			// Add default auth headers
			this.client.DefaultRequestHeaders.Add("x-client-id", this.clientId);
			this.client.DefaultRequestHeaders.Add("x-client-secret", this.clientSecret);
		}

		/// <summary>
		/// Create a link
		/// </summary>
		/// <param name="request">Create link request body</param>
		/// <returns>The link slug and url</returns>
		public async Task<CreateLinkResponse> CreateLink(CreateLinkRequest request)
		{
			var jsonBody = JsonConvert.SerializeObject(request);
			var response = await this.client.PutAsync(string.Format("{0}/api/v1/link", this.baseUrl),
				new StringContent(jsonBody, Encoding.UTF8, "application/json"));

			// Check for errors
			switch (response.StatusCode)
			{
				case HttpStatusCode.Unauthorized: throw new BriskUnauthorizedException();
				case HttpStatusCode.BadRequest: throw new BriskBadRequestException("Check if your url is valid, the slug length can be between 3-20 and the ttl is maybe too high for this server");
				case HttpStatusCode.Conflict: throw new BriskBadRequestException("The server can't generate a link with this slug size, try to change it");
			}

			var responseBody = JsonConvert.DeserializeObject<CreateLinkResponse>(await response.Content.ReadAsStringAsync());
			return responseBody;
		}

		/// <summary>
		/// Delete a link
		/// </summary>
		/// <param name="slug">The link slug</param>
		/// <returns>Is the link has been deleted</returns>
		public async Task<bool> DeleteLink(string slug)
		{
			var jsonBody = JsonConvert.SerializeObject(new { slug = slug });
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Delete,
				RequestUri = new Uri(string.Format("{0}/api/v1/link", this.baseUrl)),
				Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
			};
			var response = await this.client.SendAsync(request);

			// Check for errors
			switch (response.StatusCode)
			{
				case HttpStatusCode.Unauthorized: throw new BriskUnauthorizedException();
				case HttpStatusCode.BadRequest: return false;
			}

			return true;
		}

		/// <summary>
		/// Update a link
		/// </summary>
		/// <param name="body">The update payload</param>
		/// <returns>If the update has been done</returns>
		public async Task<bool> UpdateLink(UpdateLinkRequest body)
		{
			var jsonBody = JsonConvert.SerializeObject(body);
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Patch,
				RequestUri = new Uri(string.Format("{0}/api/v1/link", this.baseUrl)),
				Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
			};
			var response = await this.client.SendAsync(request);

			switch (response.StatusCode)
			{
				case HttpStatusCode.Unauthorized: throw new BriskUnauthorizedException();
				case HttpStatusCode.BadRequest: return false;
			}

			return true;
		}

		/// <summary>
		/// Create a new webhook
		/// </summary>
		/// <param name="body">The webhook request</param>
		/// <returns>Is the webhook has been registered</returns>
		public async Task<bool> CreateWebhook(CreateWebhookRequest body)
		{
			var jsonBody = JsonConvert.SerializeObject(body);
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Put,
				RequestUri = new Uri(string.Format("{0}/api/v1/webhook", this.baseUrl)),
				Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
			};
			var response = await this.client.SendAsync(request);

			switch (response.StatusCode)
			{
				case HttpStatusCode.Unauthorized: throw new BriskUnauthorizedException();
				case HttpStatusCode.Conflict: return false;
				case HttpStatusCode.BadRequest: return false;
			}

			return true;
		}

		/// <summary>
		/// Create a new credential pair
		/// </summary>
		/// <param name="clientId">The ClientID</param>
		/// <param name="clientSecret">The ClientSecret</param>
		/// <returns>Is the credential has been created with success</returns>
		public async Task<bool> CreateCredential(string clientId, string clientSecret)
		{
			var jsonBody = JsonConvert.SerializeObject(new { client_id = clientId, client_secret = clientSecret });
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Put,
				RequestUri = new Uri(string.Format("{0}/api/v1/credential", this.baseUrl)),
				Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
			};
			var response = await this.client.SendAsync(request);

			switch (response.StatusCode)
			{
				case HttpStatusCode.Unauthorized: throw new BriskUnauthorizedException();
				case HttpStatusCode.BadRequest: return false;
			}

			return true;
		}
	}
}
