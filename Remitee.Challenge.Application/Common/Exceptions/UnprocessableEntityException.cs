using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remitee.Challenge.Application.Common.Exceptions
{
    public class UnprocessableEntityException : Exception
    {
        public UnprocessableEntityException() { }

        public UnprocessableEntityException(string message) : base(message) { }

        public UnprocessableEntityException(string message, Exception innerException) : base(message, innerException) { }
    }
}

