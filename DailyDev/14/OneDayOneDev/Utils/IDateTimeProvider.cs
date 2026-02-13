using System.Globalization;

namespace OneDayOneDev.Utils
{
    public interface IDateTimeProvider
    {
        DateTime Today { get; }

        public static DateTime? ParseDate(string? date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return null;

            if (DateTime.TryParseExact(date, "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var result))
            {
                return result.Date;
            }
            if (DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var iso))
                return iso.Date;

            return null;
        }
    }


}
