using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Models.Auth
{
    public class LoginRequestModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}