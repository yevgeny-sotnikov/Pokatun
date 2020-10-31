using System;
using System.Collections.Generic;
using System.Linq;

namespace Pokatun.Data
{
    public class ServerResponce
    {
        public static ServerResponce ForError(string errorCode)
        {
            return new ServerResponce { Success = false, ErrorCodes = new List<string> { errorCode } };
        }

        public static ServerResponce ForErrors(params string[] errorCodes)
        {
            return new ServerResponce { Success = false, ErrorCodes = new List<string>(errorCodes) };
        }

        public static ServerResponce ForErrors(IEnumerable<string> errorCodes)
        {
            return new ServerResponce { Success = false, ErrorCodes = new List<string>(errorCodes) };
        }

        public bool Success { get; set; }

        public List<string> ErrorCodes { get; set; }
    }

    public sealed class ServerResponce<T> : ServerResponce
    {
        public T Data { get; set; }
    }
}
