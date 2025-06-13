using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Models.Auth
{
    public class TokenModel
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; } = new DateTime();
        public bool NeedTelegramToLogin { get; set; }
        public bool IsValidLogin { get; set; }
    }
}