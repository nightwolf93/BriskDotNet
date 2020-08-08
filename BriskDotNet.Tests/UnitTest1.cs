using NUnit.Framework;

namespace BriskDotNet.Tests
{
    public class Tests
    {
        private BriskClient client;

        [SetUp]
        public void Setup()
        {
            this.client = new BriskClient("master", "changeme");
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}