using System;
using System.Collections.Generic;
using System.Text;

namespace OneDayOneDev
{
    
    public class Filter
    {
        public string SearchDirection { get; set; } = "ASC";

        public bool? IsCompleted { get; set; } = null;

        public string? DateFrom { get; set; } = null;
        public string? DateTo { get; set; } = null;
        

    }
}
