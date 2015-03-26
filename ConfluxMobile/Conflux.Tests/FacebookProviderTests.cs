using System.Threading.Tasks;
using Conflux.Connectivity;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Conflux.Tests
{
    [TestClass]
    public class FacebookProviderTests
    {
        [TestMethod]
        public async Task WhenUserDataRetrieved_NamePopulated()
        {
            
             FacebookDataAccess fbDataAccess;

             var fbProviderMock = new FacebookProviderMock();
             fbDataAccess = new FacebookDataAccess(fbProviderMock);

            var userInfo = await fbDataAccess.GetUserNameInfoAsync(null);

            Assert.AreEqual(userInfo.FirstName, "John");
            Assert.AreEqual(userInfo.LastName, "Doe");

        }
    }
}
