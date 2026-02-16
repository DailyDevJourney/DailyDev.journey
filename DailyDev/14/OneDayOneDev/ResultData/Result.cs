namespace OneDayOneDev.Resultdata
{
    public class Result
    {
        public bool Success { get; }
        public string Message { get; }

        protected Result(bool success, string message   )
        {
            Success = success;
            Message = message;
        }

        public static Result Ok (string message = "")
        {
            return new Result(true, message);
        }
        public static Result Failed(string message )
        {
            return new Result(false, message);
        }
    }

    public class Result<T> : Result
    {
        public T? Data { get; }
        private Result(bool success, string message, T? data)
            : base(success, message)
        {
            Data = data;
        }

        public static Result<T> Ok(T Data,string message = "")
        {
            return new Result<T>(true, message,Data );
        }
        public static  Result<T> Failed(string message)
        {
            return new Result<T>(false, message,default);
        }

    }
}
