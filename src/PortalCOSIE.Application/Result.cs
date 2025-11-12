
namespace PortalCOSIE.Application
{
    public class Result<T>
    {
        public bool Succeeded { get; private set; }
        public T Value { get; private set; }
        public List<string> Errors { get; private set; } = new();

        private Result() { }

        public static Result<T> Success(T value)
        {
            return new Result<T> { Succeeded = true, Value = value };
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T> { Succeeded = false, Errors = new List<string> { error } };
        }

        public static Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T> { Succeeded = false, Errors = errors.ToList() };
        }
    }

}
