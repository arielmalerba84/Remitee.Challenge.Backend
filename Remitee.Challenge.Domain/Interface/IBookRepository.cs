using Remitee.Challenge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remitee.Challenge.Domain.Interface
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        IQueryable<Book> GetAllQueryable(string? titulo = null, string? descripcion = null, int? añoPublicacion = null);

    }
}
