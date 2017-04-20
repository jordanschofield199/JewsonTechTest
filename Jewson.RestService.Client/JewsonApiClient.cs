using System;
using System.Collections.Generic;
using Jewson.Logging;
using Jewson.Logging.Interfaces;
using Jewson.RestService.Business.DTO;
using Jewson.RestService.Client.Interfaces;

namespace Jewson.RestService.Client
{
    /// <summary>
    /// Implementation of an API client that is used to call the WebService
    /// </summary>
    public class JewsonApiClient : IJewsonApiClient
    {
        private readonly IWebApiClient _webApiClient;
        private readonly ILogger _logger = LogManager.GetLogger<JewsonApiClient>();

        public JewsonApiClient(IWebApiClient webApiClient)
        {
            _webApiClient = webApiClient;
        }

        /// <summary>
        /// Get all branches from service
        /// </summary>
        /// <returns></returns>
        public IList<BranchDTO> GetAllBranches()
        {
            try
            {
                var apiResponse = _webApiClient.Get<ApiResponse<IList<BranchDTO>>>(Urls.Branches);
                return apiResponse.Data;
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return new List<BranchDTO>();
            }
        }

        /// <summary>
        /// Get single branch based on number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public BranchDTO GetByNumber(int number)
        {
            try
            {
                var apiResponse = _webApiClient.Get<ApiResponse<BranchDTO>>($"{Urls.BranchesById}?id={number}");
                return apiResponse.Data;
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return default(BranchDTO);
            }
        }

        /// <summary>
        /// Get all branches based on pagination criteria
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public ApiResponse<IList<BranchDTO>> GetPagedResults(int take, int skip, string orderBy = null, string orderDir = null, string search = null)
        {
            try
            {
                var apiResponse =
                    _webApiClient.Get<ApiResponse<IList<BranchDTO>>>(
                        $"{Urls.BranchesByPaging}?take={take}&skip={skip}&orderby={orderBy}&orderdir={orderDir}&search={search}");

                return apiResponse;
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return new ApiResponse<IList<BranchDTO>>
                {
                    Data = new List<BranchDTO>(),
                    Success = false,
                    TotalRecords = 0,
                    Records = 0
                };
            }
        }
    }
}
