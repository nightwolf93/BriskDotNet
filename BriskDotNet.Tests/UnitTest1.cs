using System.Threading.Tasks;
using BriskDotNet.Requests;
using BriskDotNet.Responses;
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
			var response = await this.client.CreateLink(new CreateLinkRequest("https://github.com/nightwolf93/brisk", 30000, 5));
			if (response.Slug.Length == 5)
			{
				Assert.Pass("Link created");
			}
			else
			{
				Assert.Fail("Slug not valid");
			}
		}

		[Test]
		public async Task TestUpdateLink()
		{
			var link = await this.client.CreateLink(new CreateLinkRequest("https://github.com/nightwolf93/brisk", 30000, 5));
			var response = await this.client.UpdateLink(new UpdateLinkRequest(link.Slug)
			{
				URL = "https://github.com/nightwolf93/brisk",
				TTL = 60000
			});
			if (response)
			{
				Assert.Pass("Link updated");
			}
			else
			{
				Assert.Fail("Can't update the link");
			}
		}

		[Test]
		public async Task TestDeleteLink()
		{
			var link = await this.client.CreateLink(new CreateLinkRequest("https://github.com/nightwolf93/brisk", 30000, 5));
			var response = await this.client.DeleteLink(link.Slug);
			if (response)
			{
				Assert.Pass("Link deleted");
			}
			else
			{
				Assert.Fail("Can't delete the link");
			}
		}

		[Test]
		public async Task TestCreateWebhook()
		{
			var response = await this.client.CreateWebhook(new CreateWebhookRequest("http://localhost:3000/webhook",
				"new_link", "visit_link", "update_link"));
			if (response)
			{
				Assert.Pass("Webhook created");
			}
			else
			{
				Assert.Fail("Can't create the webhook");
			}
		}

		[Test]
		public async Task TestCreateCredential()
		{
			var response = await this.client.CreateCredential("test", "test_secret");
			if (response)
			{
				Assert.Pass("Credential created");
			}
			else
			{
				Assert.Fail("Can't create the credential");
			}
		}
	}
}