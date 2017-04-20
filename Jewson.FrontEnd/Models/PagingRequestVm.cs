using System.Collections.Generic;
using System.Linq;

namespace Jewson.FrontEnd.Models
{
    public class PagingRequestVm
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public int Draw { get; set; }
        public Dictionary<string, string> Search { get; set; }
        public Dictionary<string, string>[] Order { get; set; }
        public Dictionary<string, string>[] Columns { get; set; }
        public string SearchValue => Search != null && Search.Any() ? Search["value"] : null;
        public bool IsOrderByDesc => Order != null && Order.Any() && Order[0]["dir"] == "desc";
        public OrderByName? OrderBy => Order != null && Order.Any() ? GetOrderByColumnNumber(Order[0]["column"]) : null;

        private OrderByName? GetOrderByColumnNumber(string columnNumber)
        {
            switch (columnNumber)
            {
                case "0":
                    return OrderByName.Number;
                case "1":
                    return OrderByName.Name;
                case "2":
                    return OrderByName.Address1;
                case "3":
                    return OrderByName.Town;
                case "4":
                    return OrderByName.County;
                case "5":
                    return OrderByName.Potcode;
            }
            return null;
        }
    }

    public enum OrderByName
    {
        Number = 0,
        Name = 1,
        Address1 = 2,
        Town = 3,
        County = 4,
        Potcode = 5
    }
}