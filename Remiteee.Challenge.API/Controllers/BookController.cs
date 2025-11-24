using MediatR;
using Microsoft.AspNetCore.Mvc;
using Remitee.Challenge.Application.Books.Command.CreateBook;
using Remitee.Challenge.Application.Books.Dto;
using Remitee.Challenge.Application.Books.Queries.GetAllBooks;
using Remitee.Challenge.Application.Common.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace Remitee.Challenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ISender _sender;

        public BookController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetAllWithPagination")]
        [SwaggerOperation("Obtener todos los libros", "Devuelve paginated list de los libros")]
        public async Task<PaginatedList<BookDto>> GetAllWithPagination(ISender sender, [FromQuery] GetAllWithPaginationQuery query)
        {
            return await sender.Send(query);
        }

        [HttpPost("AddBook")]
        [SwaggerOperation("inserta un nuevo libro", "Devuelve el libro creado")]
        public async Task<ResultResponse<BookDto>> AddBook(ISender sender, [FromBody] CreateBookCommand command)
        {
            return await sender.Send(command);
        }

    }
}
