namespace OneDayOneDev.Api.Request
{
    public class BaseRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
}
