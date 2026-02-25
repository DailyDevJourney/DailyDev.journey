using OnedayOneDev_Shared.Utils.Interface;

namespace OneDayOneDev_Test
{
    public class FakeDateTimeProvider : IDateTimeProvider
    {
        public DateTime Today { get; set; }
    }
}
