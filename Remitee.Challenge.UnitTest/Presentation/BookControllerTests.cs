using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Remitee.Challenge.API.Controllers;
using Remitee.Challenge.Application.Books.Command.CreateBook;
using Remitee.Challenge.Application.Books.Dto;
using Remitee.Challenge.Application.Common.Wrappers;

namespace Remitee.Challenge.Tests.API;

public class BookControllerTests
{
    [Fact]
    public async Task AddBook_Should_Return_BookDto()
    {
        var senderMock = new Mock<ISender>();

        senderMock
            .Setup(s => s.Send(It.IsAny<CreateBookCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ResultResponse<BookDto>
            {
                Data = new BookDto { Id = 1, Titulo = "Test" },
                Success = true
            });


        var controller = new BookController(senderMock.Object);

        var command = new CreateBookCommand
        {
            Titulo = "Test",
            Descripcion = "Test Desc",
            AñoPublicacion = 2000
        };

        var result = await controller.AddBook(senderMock.Object, command);

        result.Data!.Titulo.Should().Be("Test");
    }
}
