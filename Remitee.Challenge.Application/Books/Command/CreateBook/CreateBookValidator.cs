using FluentValidation;
using Remitee.Challenge.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remitee.Challenge.Application.Books.Command.CreateBook
{
    public class CreateBookValidator : AbstractValidator<CreateBookCommand>
    {
        private readonly IBookRepository _bookRepository;

        public CreateBookValidator(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;

            //Titulo
            RuleFor(x => x.Titulo)
               .NotEmpty().WithMessage("El campo Titulo es obligatorio.")
               .MaximumLength(100).WithMessage("El campo Titulo no debe superar los 100 caracteres.")
               .MustAsync(async (command, name, cancellation) =>
               {
                   var existingRol = await _bookRepository.GetOneAsync(r => r.Titulo.ToLower() == name.ToLower());
                   return existingRol == null;
               })
                    .WithMessage("Ya existe un libro con ese nombre en nuestros registros");


            //Descripcion
            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("El campo Descripcion es obligatorio.")
                .MaximumLength(150).WithMessage("El campo Descripcion no debe superar los 150 caracteres.");
            
            //Año de publicacion
            RuleFor(x => x.AñoPublicacion)
                .NotNull().WithMessage("El año de publicación es obligatorio.")
                .NotEmpty().WithMessage("El año de publicación no puede estar vacío.")
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("El año de publicación no puede posterior al año actual");



        }
    }
}


