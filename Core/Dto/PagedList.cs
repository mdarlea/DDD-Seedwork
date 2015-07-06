using System;
using System.Collections.Generic;

namespace Swaksoft.Core.Dto
{
    public class PagedList
    {
        public IEnumerable<object> Content { get; set; }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalRecords / PageSize); }
        }
    } 
}
