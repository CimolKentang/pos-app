using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Models.Responses
{
    public class SearchResponse<T>
    {
        public List<T>? Items { get; set; }

		/// <summary>
        /// Number or total item
        /// </summary>
		public int TotalItem { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }
    }
}