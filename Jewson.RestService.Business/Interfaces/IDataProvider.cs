using System.Collections.Generic;
using Jewson.RestService.Business.DTO;

namespace Jewson.RestService.Business.Interfaces
{
    public interface IDataProvider
    {
        IList<BranchDTO> GetAllBranches();
        BranchDTO GetBranchByNumber(int number);
        PagedResponse<BranchDTO> GetPagedBranches(int take, int skip, string orderBy = null, string orderDir = "ASC",
            string search = null);
    }
}
