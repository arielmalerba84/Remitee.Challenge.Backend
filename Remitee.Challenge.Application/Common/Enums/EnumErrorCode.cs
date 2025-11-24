using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remitee.Challenge.Application.Common.Enums
{
    public enum EnumErrorCode
    {
        NOT_FOUND = 404,
        BAD_REQUEST = 400,
        UNAUTHORIZED = 401,
        FORBIDDEN = 403,
        REQUIRED_FIELD = 422,
        USED_FIELD = 423,
        FIELD_NOT_EXIST = 424,
        BUSINESS_LOGIC_ERROR = 425
    }
}
