using OnedayOneDev_Shared.DataWindow;
using System.Runtime.CompilerServices;

namespace OnedayOneDev_Shared.ResultData
{
    public class PageResult
    {

        
        public int ActualPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItem { get; set; }
        public int TotalPages { get; set; }

        public Filter? _Filter { get; set; } = null;

        public List<TaskItem>? tasks { get; set; }


    }

    public static class PageResultExtension
    { 
        public static PageResult ConvertToPageResult(this IEnumerable<TaskItem> source , int page,int pageSize, Filter? Filter = null)
        {
            //Valeur defaut
            if(page < 1) page = 1;
            if(pageSize < 1) pageSize = 100;

            var totalItem = source.Count();
            var totalPages = (int)Math.Ceiling(totalItem / (double)pageSize);

            var tasks = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            if (tasks.Count == 0)
            {
                tasks = null;
            }
            return new PageResult {
                tasks = tasks,
                ActualPage = page,
                PageSize = pageSize,
                TotalItem = totalItem,
                TotalPages = totalPages,
                _Filter = Filter
            };


        }
        
    }
}
