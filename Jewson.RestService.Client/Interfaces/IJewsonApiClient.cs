using System.Collections.Generic;
using Jewson.RestService.Business.DTO;

namespace Jewson.RestService.Client.Interfaces
{
    public interface IJewsonApiClient
    {
        IList<BranchDTO> GetAllBranches();
        BranchDTO GetByNumber(int number);

        ApiResponse<IList<BranchDTO>> GetPagedResults(int take, int skip, string orderBy = null, string orderDir = null,
            string search = null);
    }
}
