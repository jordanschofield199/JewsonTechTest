using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Jewson.RestService.Business.DTO;
using Jewson.RestService.Business.Interfaces;

namespace Jewson.RestService.Controllers
{
    /// <summary>
    /// Retrieve branch data
    /// </summary>
    [Route("api/branches/")]
    public class BranchesController : ApiController
    {
        private readonly IDataProvider _dataProvider;
        public BranchesController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        /// <summary>
        /// Get all branches
        /// </summary>
        /// <returns>List of all branches found in repo</returns>
        /// <response code="200">OK with collection of branches</response>
        /// <response code="500">Internal Server Error with exception</response>
        [ResponseType(typeof(IList<BranchDTO>))]
        public IHttpActionResult Get()
        {
            try
            {
                var data = _dataProvider.GetAllBranches();
                return Ok(new ApiResponse<IList<BranchDTO>> {Success = true, Data = data, Records = data.Count, TotalRecords = data.Count});
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        /// <summary>
        /// Gets a single branch by number
        /// </summary>
        /// <param name="id">Number</param>
        /// <returns>Single branch</returns>
        /// <response code="200">OK with collection of branches</response>
        /// <response code="500">Internal Server Error with exception</response>
        /// <response code="404">Record could not be found</response>
        [ResponseType(typeof(BranchDTO))]
        [Route("api/branches/getbyid")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var data = _dataProvider.GetBranchByNumber(id);
                if (data != null)
                {
                    return Ok(new ApiResponse<BranchDTO> {Success = true, Data = data, Records = 1, TotalRecords = 1});
                }

                return NotFound();
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
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
        /// <returns></returns>
        [ResponseType(typeof(IList<BranchDTO>))]
        [Route("api/branches/getpaged")]
        public IHttpActionResult GetPaged(int take, int skip, string orderBy = null, string orderDir = "ASC", string search = null)
        {
            try
            {
                var response = _dataProvider.GetPagedBranches(take, skip, orderBy, orderDir, search);
                return
                    Ok(new ApiResponse<IList<BranchDTO>>
                    {
                        Success = true,
                        Data = response.Data,
                        TotalRecords = response.TotalRecords,
                        Records = response.Records
                    });
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        public IHttpActionResult Put(BranchDTO branch)
        {
            //  Would implement model state validation and return BadRequest if it fails
            //  Would lookup the current record based on the posted ID, if this cannot be found return suitable error message
            
            throw new NotImplementedException();
        }

        public IHttpActionResult Post(BranchDTO branch)
        {
            //  Would implement model state validation and return BadRequest if it fails
            //  Would need to map DTO to valid branch data object before inserting
            //  Would return the newly created record which would have been mapped back into a DTO and should now include the incremented ID
            throw new NotImplementedException();
        }

        public IHttpActionResult Delete(int id)
        {
            //  Would lookup the current record based on the posted ID, if this cannot be found return suitable error message
            //  Would just return status code and a success flag, no need to return any data object
            throw new NotImplementedException();
        }
    }
}
