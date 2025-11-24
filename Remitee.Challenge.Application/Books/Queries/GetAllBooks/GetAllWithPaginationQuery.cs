using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Remitee.Challenge.Application.Books.Dto;
using Remitee.Challenge.Application.Common.Wrappers;
using Remitee.Challenge.Domain.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remitee.Challenge.Application.Books.Queries.GetAllBooks
{
    public record GetAllWithPaginationQuery : IRequest<PaginatedList<BookDto>>
    {
        public string? Titulo { get; set; } = string.Empty;

        public string? Descripcion { get; set; } = string.Empty;

        public int? AñoPublicacion { get; set; }

        [Required]
        public int PageNumber { get; init; } = 1;
        [Required]
        public int PageSize { get; init; } = 10;
    }



    public class GetAllWithPaginationQueryHandler : IRequestHandler<GetAllWithPaginationQuery, PaginatedList<BookDto>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllWithPaginationQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<PaginatedList<BookDto>> Handle(GetAllWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = _bookRepository.GetAllQueryable(request.Titulo, request.Descripcion, request.AñoPublicacion);
            
            var dtoQuery = query.ProjectToType<BookDto>(); //  Mapster
            
            var result = await PaginatedList<BookDto>.CreateAsync(dtoQuery, request.PageNumber, request.PageSize);

            return result;
        }
    }
}

