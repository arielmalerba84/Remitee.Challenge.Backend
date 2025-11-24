using FluentAssertions;
using Remitee.Challenge.Domain;

namespace Remitee.Challenge.Tests.Domain
{
    public class BookTests
    {
        [Fact]
        public void Book_Should_Create_With_Values()
        {
            // Arrange & Act
            var book = new Book
            {
                Titulo = "Test Book",
                Descripcion = "Descripción de prueba",
                AñoPublicacion = 2020
            };

            // Assert
            book.Titulo.Should().Be("Test Book");
            book.Descripcion.Should().Be("Descripción de prueba");
            book.AñoPublicacion.Should().Be(2020);
            book.Id.Should().Be(0); // por defecto no persistido aún
        }

        [Fact]
        public void Book_Default_Values_Should_Not_Be_Null()
        {
            var book = new Book();

            book.Titulo.Should().NotBeNull();
            book.Descripcion.Should().NotBeNull();
            // AñoPublicacion es int => por defecto 0
            book.AñoPublicacion.Should().Be(0);
        }

        [Fact]
        public void Can_Update_Book_Properties()
        {
            var book = new Book
            {
                Titulo = "Original",
                Descripcion = "Orig desc",
                AñoPublicacion = 1990
            };

            // Act - update
            book.Titulo = "Updated";
            book.Descripcion = "Updated desc";
            book.AñoPublicacion = 2000;

            // Assert
            book.Titulo.Should().Be("Updated");
            book.Descripcion.Should().Be("Updated desc");
            book.AñoPublicacion.Should().Be(2000);
        }

        [Fact]
        public void Books_With_Same_Id_Should_Be_Considered_Equal_By_Id()
        {
         
            var b1 = new Book { Id = 1, Titulo = "A" };
            var b2 = new Book { Id = 1, Titulo = "B" };

            b1.Id.Should().Be(b2.Id);
         
            (b1.Id == b2.Id).Should().BeTrue();
        }

        [Fact]
        public void Long_Titulo_Should_Be_Accepted_By_Entity()
        {
         
            var longTitulo = new string('X', 1000);
            var book = new Book { Titulo = longTitulo };

            book.Titulo.Length.Should().Be(1000);
            book.Titulo.Should().Be(longTitulo);
        }

        [Fact]
        public void AñoPublicacion_Can_Be_Future_Number_But_Business_Validation_Should_Handle_It()
        {
         
            var nextYear = DateTime.Now.Year + 1;
            var book = new Book { AñoPublicacion = nextYear };

            book.AñoPublicacion.Should().Be(nextYear);
        }
    }
}
