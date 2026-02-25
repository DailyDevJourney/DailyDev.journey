namespace OnedayOneDev_Shared.Request
{
    public class TaskGetRequest : BaseRequest
    {

       public int? id { get; set; }
       public string? Title { get; set; }
       public string? DueDate { get; set; }
       public bool? IsCompleted { get; set; }

        public Filter? _filter { get; set; } = null;

    }
}
