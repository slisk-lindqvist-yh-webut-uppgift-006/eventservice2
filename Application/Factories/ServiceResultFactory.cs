using Application.Models;

namespace Application.Factories;

#region ChatGPT Advice per request

    public static class ServiceResultFactory
    {
        // For errors
        public static ServiceResult<T> Error<T>(int statusCode, string error)
        {
            return new ServiceResult<T>
            {
                Succeeded = false,
                StatusCode = statusCode,
                Error = error,
                Result = default
            };
        }

        public static ServiceResult Error(int statusCode, string error)
        {
            return new ServiceResult
            {
                Succeeded = false,
                StatusCode = statusCode,
                Error = error
            };
        }

        // For successes with data
        public static ServiceResult<T> Success<T>(T data, int statusCode = 200, string? message = null)
        {
            return new ServiceResult<T>
            {
                Succeeded = true,
                StatusCode = statusCode,
                Result = data,
                Message = message
            };
        }

        // For successes without data (void-like operations)
        public static ServiceResult Success(int statusCode = 200, string? message = null)
        {
            return new ServiceResult
            {
                Succeeded = true,
                StatusCode = statusCode,
                Message = message
            };
        }
    }

#endregion