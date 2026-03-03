using System;
using System.Collections.Generic;
using System.Text;

namespace OnedayOneDev_Shared.Identification
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
    }
}
