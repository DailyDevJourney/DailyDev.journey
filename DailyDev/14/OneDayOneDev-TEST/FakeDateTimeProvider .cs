using OneDayOneDev.Utils;

namespace OneDayOneDev_Test
{
    public class FakeDateTimeProvider : IDateTimeProvider
    {
        public DateTime Today { get; set; }
    }
}
