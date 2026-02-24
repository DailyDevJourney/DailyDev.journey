using OneDayOneDev.DataWindow;

namespace OneDayOneDev.Api.ValueObject
{
    public class PageResult
    {

        public List<TaskItem>? tasks { get; set; }
        public int ActualPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItem { get; set; }
        public int TotalPages { get; set; }

        public Filter? _Filter { get; set; } = null;
        

    }
}
