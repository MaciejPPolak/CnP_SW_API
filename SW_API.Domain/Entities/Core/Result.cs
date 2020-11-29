namespace SW_API.Domain.Entities
{
    /// <summary>
    /// Operation Result
    /// </summary>
    /// <typeparam name="T">Expected return value</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Has request been completed without errors
        /// </summary>
        public bool Succeeded;
        /// <summary>
        /// Value returned by operation
        /// </summary>
        public T Value;
        /// <summary>
        /// Error that occured during operation
        /// </summary>
        public Error Error;

        /// <summary>
        /// Generate new success result
        /// </summary>
        public static Result<T> Success(T result)
        {
            return new Result<T>() { Succeeded = true, Value = result };
        }


        /// <summary>
        /// Generate new success result
        /// </summary>
        public static Result<T> Success()
        {
            return new Result<T>() { Succeeded = true };
        }

        /// <summary>
        /// Generate new error result
        /// </summary>
        public static Result<T> Failure(Error err)
        {
            return new Result<T>() { Succeeded = false, Error = err };
        }

    }
}
