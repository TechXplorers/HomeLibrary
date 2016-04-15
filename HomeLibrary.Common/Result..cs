namespace HomeLibrary.Common
{
    public class Result
    {
        public string Message { get; set; }
        public bool IsError { get; set; }

        public static Result Success(string message = "")
        {
            return new Result
            {
                Message = message,
                IsError = false
            };
        }

        public static Result Error(string message = "")
        {
            return new Result
            {
                Message = message,
                IsError = true
            };
        }

        public static Result<T> Success<T>(T value, string message = "")
        {
            return new Result<T>
            {
                Value = value,
                Message = message,
                IsError = false
            };
        }

        public static Result<T> Error<T>(T value, string message = "")
        {
            return new Result<T>
            {
                Value = value,
                Message = message,
                IsError = true
            };
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }
    }
}
