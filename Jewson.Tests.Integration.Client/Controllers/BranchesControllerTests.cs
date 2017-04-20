using System.Collections.Generic;
using System.Linq;
using Jewson.RestService.Business.DTO;
using Jewson.RestService.Client;
using NUnit.Framework;

namespace Jewson.Tests.Integration.Client.Controllers
{
    [TestFixture]
    public class BranchesControllerTests
    {
        private JewsonApiClient _apiClient;
        private AppSettingsConfiguration _appSettings;
        private WebApiClient _webApiClient;

        [SetUp]
        public void Setup()
        {
            _appSettings = new AppSettingsConfiguration();
            _webApiClient = new WebApiClient(_appSettings);
            _apiClient = new JewsonApiClient(_webApiClient);
        }

        [Test]
        public void GetAllBranches_returns_correct_number_of_records()
        {
            //  Arrange

            //  Act
            var result = _apiClient.GetAllBranches();

            //  Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(566, result.Count);
        }

        [Test]
        [TestCase(101, ExpectedResult = "Harpenden")]
        [TestCase(106, ExpectedResult = "Grimsby Pyewipe")]
        public string GetById_returns_correct_record(int number)
        {
            //  Arrange
            
            //  Act
            var result = _apiClient.GetByNumber(number);

            //  Assert
            Assert.IsNotNull(result);
            return result.Name;
        }

        [Test]
        public void GetPaged_returns_correct_number_of_records()
        {
            //  Arrange
            int take = 10;
            int skip = 0;
            string orderBy = "Number";
            string orderDir = "DESC";
            string search = "Northampton";

            //  Act
            ApiResponse<IList<BranchDTO>> result = _apiClient.GetPagedResults(take, skip, orderBy, orderDir, search);

            //  Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Data.Count);
            Assert.AreEqual("Northampton", result.Data.First().Name);
        }
    }
}
