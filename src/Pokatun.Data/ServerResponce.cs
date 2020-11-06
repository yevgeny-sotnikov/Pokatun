using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Pokatun.Data
{
    public class ServerResponce
    {
        public static ServerResponce ForError(string errorCode)
        {
            return new ServerResponce { ErrorCodes = new List<string> { errorCode } };
        }

        public static ServerResponce ForErrors(params string[] errorCodes)
        {
            return new ServerResponce { ErrorCodes = new List<string>(errorCodes) };
        }

        public static ServerResponce ForErrors(IEnumerable<string> errorCodes)
        {
            return new ServerResponce { ErrorCodes = new List<string>(errorCodes) };
        }

        [NotMapped]
        public virtual bool Success => false;

        public List<string> ErrorCodes { get; set; }
    }

    public sealed class ServerResponce<T> : ServerResponce
    {
        [NotMapped]
        public override bool Success => Data != null;

        public T Data { get; set; }
    }
}
