using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Conflux.Connectivity.GraphApi;

namespace Conflux.Tests
{
    [TestClass]
    public class FacebookClientTests
    {
        private IFacebookClient fbClient;

        [TestInitialize]
        public void InitializeTest()
        {
            fbClient = new FacebookClientMock();
        }

        [TestMethod]
        public async Task WhenUserDataRetrieved_NamePopulated()
        {
            var userInfo = await fbClient.GetUserNameInfoAsync();

            Assert.AreEqual(userInfo.FirstName, "John");
            Assert.AreEqual(userInfo.LastName, "Doe");

        }
    }
}
