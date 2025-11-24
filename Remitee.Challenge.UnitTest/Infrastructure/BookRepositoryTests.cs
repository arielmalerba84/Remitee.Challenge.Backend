using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Remitee.Challenge.Domain;
using Remitee.Challenge.Infraestructure.Data;
using Remitee.Challenge.Infraestructure.Repositories;


namespace Remitee.Challenge.Tests.Infrastructure
{
    public class BookRepositoryTests
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task AddAsync_Should_Add_Book()
        {
            var context = CreateContext();
            var repo = new BookRepository(context);

            var book = new Book
            {
                Titulo = "Test",
                Descripcion = "Desc",
                AñoPublicacion = 2020
            };

            await repo.AddAsync(book);

            var saved = await context.Books.FirstOrDefaultAsync();

            saved.Should().NotBeNull();
            saved!.Titulo.Should().Be("Test");
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Book()
        {
            var context = CreateContext();
            var repo = new BookRepository(context);

            var book = new Book
            {
                Titulo = "Test",
                Descripcion = "Desc",
                AñoPublicacion = 2020
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            var result = await repo.GetByIdAsync(book.Id);

            result.Should().NotBeNull();
            result!.Id.Should().Be(book.Id);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Books()
        {
            var context = CreateContext();
            var repo = new BookRepository(context);

            context.Books.AddRange(
                new Book { Titulo = "A", Descripcion = "...", AñoPublicacion = 2000 },
                new Book { Titulo = "B", Descripcion = "...", AñoPublicacion = 2001 }
            );

            await context.SaveChangesAsync();

            var result = await repo.GetAllAsync();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Book()
        {
            var context = CreateContext();
            var repo = new BookRepository(context);

            var book = new Book
            {
                Titulo = "Old",
                Descripcion = "Old Desc",
                AñoPublicacion = 1990
            };

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();

            book.Titulo = "New";

            await repo.UpdateAsync(book);

            var updated = await context.Books.FindAsync(book.Id);

            updated!.Titulo.Should().Be("New");
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Book()
        {
            var context = CreateContext();
            var repo = new BookRepository(context);

            var book = new Book
            {
                Titulo = "ToDelete",
                Descripcion = "xx",
                AñoPublicacion = 1999
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            await repo.DeleteAsync(book);

            (await context.Books.ToListAsync()).Should().BeEmpty();
        }
    }
}
