using Remitee.Challenge.Domain;
using Remitee.Challenge.Domain.Interface;
using Remitee.Challenge.Infraestructure.Common;
using Remitee.Challenge.Infraestructure.Data;


namespace Remitee.Challenge.Infraestructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _contextDB;

        public BookRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Book> GetAllQueryable(string? titulo = null, string? descripcion = null, int? añoPublicacion = null)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(titulo))
                query = query.Where(x => x.Titulo.Contains(titulo));

            if (!string.IsNullOrWhiteSpace(descripcion))
                query = query.Where(x => x.Descripcion.Contains(descripcion));

            if (añoPublicacion.HasValue && añoPublicacion.Value > 0)
                query = query.Where(x => x.AñoPublicacion == añoPublicacion.Value);

            return query;
        }
    }
}
