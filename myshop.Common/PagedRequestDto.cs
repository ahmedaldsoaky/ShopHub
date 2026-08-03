using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Common
{
    public class PagedRequestDto
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }

        public string? SortColumn { get; set; }

        public string? SortDirection { get; set; } = "asc";
    }
}
