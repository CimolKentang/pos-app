using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Models.Responses
{
    public class BaseResponse<T>
    {
        /// <summary>
        /// Api Request Success or Not
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// List Of Message
        /// </summary>
        public List<string>? Messages { get; set; }

        /// <summary>
        /// Object Data From The API Request
        /// </summary>
        public T? Data { get; set; }
    }
}