using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception exception): base(message, exception) {}
    }
}