using System;
using System.Collections.Generic;
using System.Linq;
using Jewson.Data.Interfaces;
using Jewson.Logging;
using Jewson.Logging.Interfaces;
using Jewson.RestService.Business.DTO;
using Jewson.RestService.Business.Interfaces;
using System.Linq.Dynamic;

namespace Jewson.RestService.Business.DataRetrieval
{
    /// <summary>
    /// Business layer data provider which calls into a chosen repository. This implementation uses the static data repo.
    /// All responses are mapped from data objects into DTO's
    /// </summary>
    public class DataProvider : IDataProvider
    {
        private readonly IDataRepository _dataRepository;
        private readonly ILogger _logger = LogManager.GetLogger<DataProvider>();

        public DataProvider(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        /// <summary>
        /// Returns all branches found in the data repo
        /// </summary>
        public IList<BranchDTO> GetAllBranches()
        {
            try
            {
                var branches = _dataRepository.GetAllBranches();
                Mapping.Mapping.Configure();

                return branches.Select(x => Mapping.Mapping.Mapper.Map<BranchDTO>(x)).ToList();
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Failed to get all branches from data provider");
                return new List<BranchDTO>();
            }
        }

        /// <summary>
        /// Return single branch based on the number or null if it cannot be found or if there was an error
        /// </summary>
        /// <param name="number">Number</param>
        public BranchDTO GetBranchByNumber(int number)
        {
            try
            {
                var branch = _dataRepository.GetBranchByNumber(number);
                Mapping.Mapping.Configure();

                return Mapping.Mapping.Mapper.Map<BranchDTO>(branch);
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"Failed to get branch with number {number} from data provider");
                return null;
            }
        }

        /// <summary>
        /// Filters all branches and allows pagination including ordering and searching
        /// </summary>
        /// <param name="take">Number of records to return</param>
        /// <param name="skip">Number of records to skip</param>
        /// <param name="orderBy">Property to order by</param>
        /// <param name="orderDir">ASC or DESC</param>
        /// <param name="search">Value to search properties</param>
        public PagedResponse<BranchDTO> GetPagedBranches(int take, int skip, string orderBy = null,
            string orderDir = "ASC", string search = null)
        {
            try
            {
                var branches = _dataRepository.GetAllBranches();

                var response = new PagedResponse<BranchDTO>();

                // Could potentially split this out into seperate function since this method should really orchestrate and keep to single responsibility.                
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    branches =
                        branches.Where(
                            x =>
                                x.Name.ToLower().Contains(search) || x.Address1.ToLower().Contains(search) || x.Town.ToLower().Contains(search) ||
                                x.County.ToLower().Contains(search) || x.Postcode.ToLower().Contains(search) ||
                                x.Telephone1.Contains(search) || x.Email.ToLower().Contains(search) ||
                                x.BranchManager.ToLower().Contains(search)).ToList();
                }

                if (!string.IsNullOrEmpty(orderBy))
                {
                    branches = branches.OrderBy($"{orderBy} {orderDir}").ToList();
                }

                response.TotalRecords = branches.Count;
                branches = branches.Skip(skip).Take(take).ToList();

                Mapping.Mapping.Configure();

                response.Data = branches.Select(x => Mapping.Mapping.Mapper.Map<BranchDTO>(x)).ToList();
                return response;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Failed to get paged branches from data provider");
                return new PagedResponse<BranchDTO> {Data = new List<BranchDTO>()};
            }
        }
    }
}
