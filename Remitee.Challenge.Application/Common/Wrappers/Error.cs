using Remitee.Challenge.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remitee.Challenge.Application.Common.Wrappers
{
    public class Error
    {
        public Error(ErrorCodeResponse errorCode, string? description = null, string? fieldName = null)
        {
            ErrorCode = errorCode;
            Description = description;
            FieldName = fieldName;
        }

        public ErrorCodeResponse ErrorCode { get; set; }
        public string? FieldName { get; set; }
        public string? Description { get; set; }
    }
}
