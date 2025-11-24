using Mapster;
using MediatR;
using Remitee.Challenge.Application.Books.Dto;
using Remitee.Challenge.Application.Common.Exceptions;
using Remitee.Challenge.Application.Common.Wrappers;
using Remitee.Challenge.Domain;
using Remitee.Challenge.Domain.Interface;
using Remitee.Challenge.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remitee.Challenge.Application.Books.Command.CreateBook
{
    public record CreateBookCommand : IRequest<ResultResponse<BookDto>>
    {
        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public int AñoPublicacion { get; set; }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, ResultResponse<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ResultResponse<BookDto>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var bookToAdd = command.Adapt<Book>();
                await _bookRepository.AddAsync(bookToAdd);

                return  bookToAdd.Adapt<BookDto>();

            }
            catch (Exception ex)
            {
                throw new CommandException("Error al guardar datos",ex);
            }
           
        }
    }
}
