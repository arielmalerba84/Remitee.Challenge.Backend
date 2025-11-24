using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remitee.Challenge.Application.Common.Exceptions
{
   public class RestApiException : Exception
    {
        public RestApiException()
        {
        }

        public RestApiException(string message) : base(message)
        {
        }

        public RestApiException(string message, Exception innerException) : base(message, innerException)
        {
        }


    }
}