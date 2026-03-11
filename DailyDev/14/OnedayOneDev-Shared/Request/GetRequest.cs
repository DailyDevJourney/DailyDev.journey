using OnedayOneDev_Shared.Identification;

namespace OnedayOneDev_Shared.Request
{
    public class GetRequest : BaseRequest
    {

       public int? id { get; set; }
       public string? Title { get; set; }
       public string? DueDate { get; set; }
       public bool? IsCompleted { get; set; }

        public Filter? _filter { get; set; } = null;

    }

    public class GetUserRequest : BaseRequest
    {

        public int? id { get; set; }
        public string? UserName { get; set; }
        public UserRole? UserRole { get; set; }

    }
}
