using System;

namespace BriskDotNet
{
    public class BriskClient
    {
        private readonly string clientId;
        private readonly string clientSecret;

        public BriskClient(string clientId, string clientSecret) {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }
    }
}
