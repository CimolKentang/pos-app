using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Exceptions
{
    public class InternetException : Exception
    {
        public InternetException(string message): base(message) {}
    }
}