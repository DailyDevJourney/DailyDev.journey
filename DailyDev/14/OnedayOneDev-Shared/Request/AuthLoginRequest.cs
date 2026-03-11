using System;
using System.Collections.Generic;
using System.Text;

namespace OnedayOneDev_Shared.Request
{
    public record AuthLoginRequest(string Username,string Password);

    public class AuthLoginResponse
    {
        public string access_token {  get; set; } = string.Empty;
        public string token_type { get; set; } = string.Empty;
        public int expires_in { get; set; }
    }
}
