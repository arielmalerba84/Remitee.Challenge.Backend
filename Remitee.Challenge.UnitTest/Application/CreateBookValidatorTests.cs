using Xunit;
using Moq;
using FluentAssertions;
using Remitee.Challenge.Application.Books.Command.CreateBook;
using Remitee.Challenge.Domain.Interface;
using Remitee.Challenge.Domain;
using System.Threading.Tasks;

public class CreateBookValidatorTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly CreateBookValidator _validator;

    public CreateBookValidatorTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _validator = new CreateBookValidator(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Should_Fail_When_Title_Is_Empty()
    {
        var command = new CreateBookCommand { Titulo = "", Descripcion = "Desc", AñoPublicacion = 2020 };

        var result = await _validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == "Titulo");
    }

    [Fact]
    public async Task Should_Fail_When_Title_Already_Exists()
    {
        _bookRepositoryMock
            .Setup(r => r.GetOneAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>()))
            .ReturnsAsync(new Book());

        var command = new CreateBookCommand
        {
            Titulo = "Duplicado",
            Descripcion = "Desc",
            AñoPublicacion = 2020
        };

        var result = await _validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("Ya existe un libro"));
    }

    [Fact]
    public async Task Should_Pass_When_Data_Is_Valid()
    {
        _bookRepositoryMock
            .Setup(r => r.GetOneAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>()))
            .ReturnsAsync((Book?)null);

        var command = new CreateBookCommand
        {
            Titulo = "Nuevo",
            Descripcion = "Desc buena",
            AñoPublicacion = 2020
        };

        var result = await _validator.ValidateAsync(command);

        result.IsValid.Should().BeTrue();
    }
}
