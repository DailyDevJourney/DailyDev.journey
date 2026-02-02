using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDayOneDev_DayEleven
{
    public class FakeDateTimeProvider : IDateTimeProvider
    {
        public DateTime Today { get; set; }
    }
}
