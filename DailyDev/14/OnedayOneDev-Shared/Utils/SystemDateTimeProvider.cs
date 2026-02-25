using OnedayOneDev_Shared.Utils.Interface;

namespace OnedayOneDev_Shared.Utils
{
    public  class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime Today => DateTime.Today;
    }
}
