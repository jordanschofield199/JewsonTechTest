using System.Collections.Generic;
using Jewson.RestService.Business.DTO;
using Jewson.RestService.Client;
using Jewson.RestService.Client.Interfaces;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Jewson.Tests.Unit.Client
{
    [TestFixture]
    public class JewsonApiClientTests
    {
        private JewsonApiClient _client;
        private IWebApiClient _webApiClient;

        [SetUp]
        public void Setup()
        {
            _webApiClient = Substitute.For<IWebApiClient>();
            _client = new JewsonApiClient(_webApiClient);
        }

        [Test]
        public void GetAllBranches_calls_client_with_correct_url()
        {
            //  Arrange
            _webApiClient.Get<ApiResponse<IList<BranchDTO>>>(Arg.Any<string>()).ReturnsNull();

            //  Act
            _client.GetAllBranches();

            //  Assert
            _webApiClient.Received(1).Get<ApiResponse<IList<BranchDTO>>>(Arg.Is("branches"));
        }

        [Test]
        public void GetByNumber_calls_client_with_correct_url()
        {
            //  Arrange
            int number = 1;
            _webApiClient.Get<ApiResponse<BranchDTO>>(Arg.Any<string>()).ReturnsNull();

            //  Act
            _client.GetByNumber(number);

            //  Assert
            _webApiClient.Received(1).Get<ApiResponse<BranchDTO>>(Arg.Is("branches/getbyid?id=1"));
        }

        [Test]
        public void GetPagedResults_calls_client_with_correct_url()
        {
            //  Arrange
            _webApiClient.Get<ApiResponse<IList<BranchDTO>>>(Arg.Any<string>()).ReturnsNull();

            //  Act
            _client.GetPagedResults(10, 0, "Number", "DESC", "Northampton");

            //  Assert
            _webApiClient.Received(1).Get<ApiResponse<IList<BranchDTO>>>(Arg.Is("branches/getpaged?take=10&skip=0&orderby=Number&orderdir=DESC&search=Northampton"));
        }
    }
}
