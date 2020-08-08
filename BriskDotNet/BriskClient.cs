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
    }
}
