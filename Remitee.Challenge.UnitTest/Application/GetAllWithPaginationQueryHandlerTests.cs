using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using Remitee.Challenge.Infraestructure.Data;
using Remitee.Challenge.Infraestructure.Repositories;
using Remitee.Challenge.Application.Books.Queries.GetAllBooks;
using System.Threading.Tasks;
using System.Threading;

public class GetAllWithPaginationQueryHandlerTests
{
    private ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task Should_Return_Paginated_List()
    {
        // Arrange
        var context = CreateDbContext();
        context.Books.AddRange(
            new Remitee.Challenge.Domain.Book { Titulo = "A", Descripcion = "Desc", AñoPublicacion = 2000 },
            new Remitee.Challenge.Domain.Book { Titulo = "B", Descripcion = "Desc2", AñoPublicacion = 2001 }
        );
        await context.SaveChangesAsync();

        var repo = new BookRepository(context);
        var handler = new GetAllWithPaginationQueryHandler(repo);

        var query = new GetAllWithPaginationQuery { PageNumber = 1, PageSize = 1 };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Items.Should().HaveCount(1);
        result.TotalCount.Should().Be(2);
    }
}
