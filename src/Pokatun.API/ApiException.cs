using System;
using System.Collections.Generic;
using System.Linq;

namespace Pokatun.API
{
    public sealed class ApiException : Exception
    {
        public string[] ErrorCodes { get; private set; }

        public ApiException(string errorCode) : this(new string[] { errorCode })
        {
        }

        public ApiException(IEnumerable<string> errorCodes) : this(errorCodes.ToArray())
        {
        }

        public ApiException(params string[] errorCodes)
        {
            if (errorCodes == null)
            {
                throw new ArgumentNullException(nameof(errorCodes));
            }

            foreach (string code in errorCodes)
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    throw new ArgumentOutOfRangeException("error code can't be null or empty");
                }
            }

            ErrorCodes = errorCodes;
        }
    }
}
