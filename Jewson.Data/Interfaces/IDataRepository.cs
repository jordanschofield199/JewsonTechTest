using System.Collections.Generic;
using Jewson.Data.DataModels;

namespace Jewson.Data.Interfaces
{
    public interface IDataRepository
    {
        IList<Branch> GetAllBranches();
        Branch GetBranchByNumber(int number);
    }
}
