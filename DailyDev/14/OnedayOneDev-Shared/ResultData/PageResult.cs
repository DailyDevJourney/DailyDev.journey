using OnedayOneDev_Shared.DataWindow;
using OnedayOneDev_Shared.Identification;
using System.Runtime.CompilerServices;

namespace OnedayOneDev_Shared.ResultData
{
    public class PageResult<T>
    {

        
        public int ActualPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItem { get; set; }
        public int TotalPages { get; set; }

        public Filter? _Filter { get; set; } = null;

        public List<T>? itemsLists { get; set; }


    }

    public static class PageResultExtension
    { 
        public static PageResult<T> ConvertToPageResult<T>(this IEnumerable<T> source ,
            int page,
            int pageSize,
            Filter? Filter = null)
        {
            //Valeur defaut
            if(page < 1) page = 1;
            if(pageSize < 1) pageSize = 100;

            var totalItem = source.Count();
            var totalPages = (int)Math.Ceiling(totalItem / (double)pageSize);

            var Items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            if (Items.Count == 0)
            {
                Items = null;
            }
            return new PageResult<T> {

                itemsLists = Items,
                ActualPage = page,
                PageSize = pageSize,
                TotalItem = totalItem,
                TotalPages = totalPages,
                _Filter = Filter
            };


        }


    }
}
