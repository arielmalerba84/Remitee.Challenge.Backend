
using FluentAssertions;
using Moq;
using Remitee.Challenge.Application.Books.Command.CreateBook;
using Remitee.Challenge.Application.Common.Exceptions;
using Remitee.Challenge.Domain;
using Remitee.Challenge.Domain.Interface;


public class CreateBookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly CreateBookCommandHandler _handler;

    public CreateBookCommandHandlerTests()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new CreateBookCommandHandler(_bookRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Create_Book_And_Return_Dto()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            Titulo = "Test Book",
            Descripcion = "Descripcion",
            AñoPublicacion = 2020
        };

        _bookRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Book>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Titulo.Should().Be("Test Book");
        _bookRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_CommandException_When_Error_Occurs()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            Titulo = "Error Book",
            Descripcion = "Desc",
            AñoPublicacion = 2020
        };

        _bookRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Book>()))
            .ThrowsAsync(new Exception("DB Error"));

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<CommandException>();
    }
}
