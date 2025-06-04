using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Helpers
{
    public static class QueryStringHelper
    {
        public static string ToQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(obj)?.ToString())}";

            return "?" + string.Join("&", properties);
        }
    }
}