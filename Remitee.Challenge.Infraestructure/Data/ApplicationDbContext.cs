using Microsoft.EntityFrameworkCore;
using Remitee.Challenge.Domain;
using Remitee.Challenge.Infraestructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remitee.Challenge.Infraestructure.Data
{
    public class ApplicationDbContext : AppDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books => Set<Book>();
    }
}
