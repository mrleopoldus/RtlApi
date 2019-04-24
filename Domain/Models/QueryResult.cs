using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class QueryResult
    {
        public Show[] Shows { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
    }
}
