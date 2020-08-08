using System.Threading.Tasks;
using NUnit.Framework;

namespace BriskDotNet.Tests
{
    public class Tests
    {
        private BriskClient client;

        [SetUp]
        public void Setup()
        {
            this.client = new BriskClient("http://localhost:3000", "master", "changeme");
        }

        [Test]
        public async Task TestCreateLink()
        {
            var response = await this.client.CreateLink(new Requests.CreateLinkRequest("https://github.com/nightwolf93/brisk", 30000, 5));
            if (response.Slug.Length == 5)
            {
                Assert.Pass("Link created");
            }
            else
            {
                Assert.Fail("Slug not valid");
            }
        }
    }
}