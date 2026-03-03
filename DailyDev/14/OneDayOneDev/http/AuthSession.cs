using System;
using System.Collections.Generic;
using System.Text;

namespace OneDayOneDev.http
{
    public class AuthSession
    {
        public static string? AccessToken { get; private set; }

        public static bool IsAuthentificated => !string.IsNullOrWhiteSpace(AccessToken);

        public static void SetToken(string Token) => AccessToken = Token;

        public static void Clear() => AccessToken = null;
    }
}
