using System;
namespace Pokatun.API
{
    public sealed class ApiException : Exception
    {
        public string ErrorCode { get; private set; }

        public ApiException(string errorCode)
        {
            if (string.IsNullOrWhiteSpace(errorCode))
            {
                throw new ArgumentNullException(nameof(errorCode));
            }

            ErrorCode = errorCode;
        }
    }
}
