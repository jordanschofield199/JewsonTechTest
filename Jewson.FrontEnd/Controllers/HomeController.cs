using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Jewson.FrontEnd.Models;
using Jewson.Logging;
using Jewson.Logging.Interfaces;
using Jewson.RestService.Business.DTO;
using Jewson.RestService.Client.Interfaces;

namespace Jewson.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger = LogManager.GetLogger<HomeController>();
        private readonly IJewsonApiClient _apiClient;

        public HomeController(IJewsonApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetBranchData(PagingRequestVm request)
        {
            try
            {
                ApiResponse<IList<BranchDTO>> apiResponse = _apiClient.GetPagedResults(
                    request.Length,
                    request.Start,
                    request.OrderBy?.ToString() ?? "",
                    request.Order != null && request.Order.Any() ? request.Order[0]["dir"] : null,
                    request.Search != null && request.Search.Any() ? request.Search["value"] : null);

                return new JsonResult
                {
                    Data = new
                    {
                        data = apiResponse.Data,
                        recordsTotal = apiResponse.TotalRecords,
                        recordsFiltered = apiResponse.TotalRecords,
                        draw = request.Draw
                    }
                };
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return new JsonResult
                {
                    Data = new
                    {
                        data = new List<BranchDTO>(),
                        recordsTotal = 0,
                        recordsFiltered = 0,
                        draw = request.Draw
                    }
                };
            }
        }

        [HttpPost]
        public ActionResult SearchByNumber(int number)
        {
            try
            {
                var data = _apiClient.GetByNumber(number);
                return PartialView("_SearchByNumberResult", data);
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return PartialView("_SearchByNumberResult");
            }
        }
    }
}