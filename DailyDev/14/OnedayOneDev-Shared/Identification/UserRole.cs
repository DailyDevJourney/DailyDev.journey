using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OneDayOneDev.Utils;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OnedayOneDev_Shared.Identification
{
    

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        ADMIN,USER
    }

    
}
