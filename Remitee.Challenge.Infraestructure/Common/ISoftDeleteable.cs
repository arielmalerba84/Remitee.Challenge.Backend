using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remitee.Challenge.Infraestructure.Common
{
     public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}


