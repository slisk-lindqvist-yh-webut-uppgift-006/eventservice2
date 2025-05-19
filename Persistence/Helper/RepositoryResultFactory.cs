using Persistence.Models;

namespace Persistence.Helper;

#region ChatGPT Advice

    public static class RepositoryResultFactory
    {
        public static RepositoryResult<T> Error<T>(int statusCode, string error)
        {
            return new RepositoryResult<T>
            {
                Succeeded = false,
                StatusCode = statusCode,
                Error = error,
                Result = default
            };
        }

        public static RepositoryResult<T> Success<T>(T data, int statusCode = 200, string? message = null)
        {
            return new RepositoryResult<T>
            {
                Succeeded = true,
                StatusCode = statusCode,
                Result = data,
                Message = message
            };
        }

        // Optional: Success without result, for void-like operations
        public static RepositoryResult<T> Success<T>(int statusCode = 200, string? message = null)
        {
            return new RepositoryResult<T>
            {
                Succeeded = true,
                StatusCode = statusCode,
                Result = default,
                Message = message
            };
        }
    }

#endregion