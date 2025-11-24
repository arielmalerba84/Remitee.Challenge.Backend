using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remitee.Challenge.Domain
{
    
    public class Book
    {
        public int Id { get; set; } 

        public string Titulo { get; set; } = string.Empty;
        
        public string Descripcion { get; set; } = string.Empty;

        public int AñoPublicacion { get; set; } 
    }
}
