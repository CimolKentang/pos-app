using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Constants;

namespace inovasyposmobile.Models.Requests
{
    public class SearchSortFilterPagingModel
    {
        public SearchSortFilterPagingModel()
        {
            IsValueDisPlay = false;
            IsCount = true;
            PageIndex = 0;
            PageSize = 10;
            RecordStatus = RecordStatusConstant.Active;
            SortDir = SortDirectionConstant.Desc;
        }

        /// <summary>
        /// Record Status Filtering, If Empty will filter by "Active"
        /// </summary>
        public string? RecordStatus { get; set; }

        /// <summary>
        /// Search text
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        /// Sort By Property name
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Sort direction ASC/DESC
        /// </summary>
        public string? SortDir { get; set; }

        /// <summary>
        /// Index of page
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Number Of Item per page
        /// </summary>
        public int PageSize { get; set; }

        // Count Total Items
        public bool IsCount { get; set; }

        public bool IsValueDisPlay { get; set; }
    }
}