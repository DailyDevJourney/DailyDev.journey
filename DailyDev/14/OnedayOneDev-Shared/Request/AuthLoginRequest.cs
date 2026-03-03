using System;
using System.Collections.Generic;
using System.Text;

namespace OnedayOneDev_Shared.Request
{
    public record AuthLoginRequest(string userName,string password);

    public record AuthLoginResponse(string access_token, string token_type, int expires_in);
}
