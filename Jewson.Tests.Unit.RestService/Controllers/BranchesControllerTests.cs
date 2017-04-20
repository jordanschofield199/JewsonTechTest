using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Jewson.RestService.Business.DTO;
using Jewson.RestService.Business.Interfaces;
using Jewson.RestService.Controllers;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Assert = NUnit.Framework.Assert;

namespace Jewson.Tests.Unit.RestService.Controllers
{
    [TestFixture]
    public class BranchesControllerTests
    {
        private IDataProvider _dataProvider;
        private BranchesController _controller;

        [SetUp]
        public void Setup()
        {
            _dataProvider = Substitute.For<IDataProvider>();
            _controller = new BranchesController(_dataProvider);
        }

        [Test]
        public void Get_calls_dataProvider_and_returns_apiresponse()
        {
            //  Arrange
            var data = new List<BranchDTO>();
            _dataProvider.GetAllBranches().Returns(data);

            //  Act
            var result = _controller.Get();

            //  Assert
            _dataProvider.Received(1).GetAllBranches();
            Assert.IsInstanceOf<OkNegotiatedContentResult<ApiResponse<IList<BranchDTO>>>>(result);
        }

        [Test]
        public void Get_returns_expected_data()
        {
            //  Arrange
            var data = new List<BranchDTO>{new BranchDTO()};
            _dataProvider.GetAllBranches().Returns(data);

            //  Act
            var result = _controller.Get() as OkNegotiatedContentResult<ApiResponse<IList<BranchDTO>>>;

            //  Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(1, result.Content.TotalRecords);
            Assert.IsTrue(result.Content.Success);
        }

        [Test]
        public void Get_on_exception_returns_internal_server_error()
        {
            //  Arrange
            _dataProvider.GetAllBranches().Throws(new Exception());

            //  Act
            var result = _controller.Get();

            //  Assert
            Assert.IsInstanceOf<ExceptionResult>(result);
        }

        [Test]
        public void GetById_calls_dataProvider_and_returns_apiresponse()
        {
            //  Arrange
            var number = 1;
            var data = new BranchDTO();
            _dataProvider.GetBranchByNumber(Arg.Any<int>()).Returns(data);

            //  Act
            var result = _controller.Get(1);

            //  Assert
            _dataProvider.Received(1).GetBranchByNumber(Arg.Is(number));
            Assert.IsInstanceOf<OkNegotiatedContentResult<ApiResponse<BranchDTO>>>(result);
        }

        [Test]
        public void GetById_returns_notFound_when_dataProvider_returns_null()
        {
            //  Arrange
            var number = 1;
            _dataProvider.GetBranchByNumber(Arg.Any<int>()).ReturnsNull();

            //  Act
            var result = _controller.Get(1);

            //  Assert
            _dataProvider.Received(1).GetBranchByNumber(Arg.Is(number));
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void GetById_on_exception_returns_internal_server_error()
        {
            //  Arrange
            _dataProvider.GetBranchByNumber(Arg.Any<int>()).Throws(new Exception());

            //  Act
            var result = _controller.Get(1);

            //  Assert
            Assert.IsInstanceOf<ExceptionResult>(result);
        }

        [Test]
        public void GetPaged_calls_dataProvider_and_returns_apiresponse()
        {
            //  Arrange
            int take = 10;
            int skip = 0;
            var data = new PagedResponse<BranchDTO>();
            _dataProvider.GetPagedBranches(take, skip).Returns(data);

            //  Act
            var result = _controller.GetPaged(take, skip);

            //  Assert
            _dataProvider.Received(1).GetPagedBranches(Arg.Is(take), Arg.Is(skip));
            Assert.IsInstanceOf<OkNegotiatedContentResult<ApiResponse<IList<BranchDTO>>>>(result);
        }

        [Test]
        public void GetPaged_populates_total_record_count()
        {
            //  Arrange
            int take = 10;
            int skip = 0;
            var data = new PagedResponse<BranchDTO>{Data = new List<BranchDTO>{new BranchDTO()}, TotalRecords = 1};
            _dataProvider.GetPagedBranches(take, skip).Returns(data);

            //  Act
            var result = _controller.GetPaged(take, skip) as OkNegotiatedContentResult<ApiResponse<IList<BranchDTO>>>;

            //  Assert
            _dataProvider.Received(1).GetPagedBranches(Arg.Is(take), Arg.Is(skip));
            Assert.IsInstanceOf<OkNegotiatedContentResult<ApiResponse<IList<BranchDTO>>>>(result);
            Assert.AreEqual(1, result.Content.TotalRecords);
            Assert.AreEqual(1, result.Content.Records);
        }
    }
}
