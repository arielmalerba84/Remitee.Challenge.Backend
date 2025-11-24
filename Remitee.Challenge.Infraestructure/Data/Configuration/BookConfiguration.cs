using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Remitee.Challenge.Domain;

namespace Remitee.Challenge.Infraestructure.Data.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Titulo).HasMaxLength(100).IsUnicode(false).IsRequired();
            builder.Property(p => p.Descripcion).HasMaxLength(500).IsUnicode(false);
            builder.Property(p => p.AñoPublicacion).HasMaxLength(10).IsUnicode(false).IsRequired();
        }
    }

}
