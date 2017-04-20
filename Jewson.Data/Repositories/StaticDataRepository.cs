using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Jewson.Data.DataModels;
using Jewson.Data.Interfaces;
using Jewson.Logging;
using Jewson.Logging.Interfaces;
using Newtonsoft.Json;

namespace Jewson.Data.Repositories
{
    /// <summary>
    /// Implementation of a data repository to be used when dealing with static data that is stored within files.
    /// Implement a new repo if we want to work with a SQL repo for example but methods on the interface remain valid.
    /// </summary>
    public class StaticDataRepository : IDataRepository
    {
        private readonly ILogger _logger = LogManager.GetLogger<StaticDataRepository>();

        public IList<Branch> GetAllBranches()
        {
            return LoadDataFromFile();
        }

        public Branch GetBranchByNumber(int number)
        {
            var branches = LoadDataFromFile();
            return branches.FirstOrDefault(x => x.Number.Equals(number));
        }

        public IList<Branch> LoadDataFromFile()
        {
            try
            {
                var staticFilePath = ConfigurationManager.AppSettings["StaticData.FilePath"];
                var filePath = HttpContext.Current.Server.MapPath(staticFilePath);

                if (string.IsNullOrEmpty(filePath))
                {
                    throw new Exception("Could not determine file path for static data from app settings");
                }

                var fileContent = File.ReadAllText(filePath);
                var branches = JsonConvert.DeserializeObject<List<Branch>>(fileContent);

                return branches;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Failed to load data from file");
                return new List<Branch>();
            }
        }
    }
}
