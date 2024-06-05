using BooksDBApp.DTO;
using FluentValidation;

namespace BooksDBApp.Validators
{
    public class BookUpdateValidator : AbstractValidator<BookUpdateDTO>
    {
        public BookUpdateValidator() 
        {
            RuleFor(b => b.Title)
                .NotEmpty()
                .WithMessage("The field 'Title' cannot be empty")
                .Length(5, 50)
                .WithMessage("The field 'Title' must contains from 5 to 50 characters");

            RuleFor(b => b.Author)
                .NotEmpty()
                .WithMessage("The field 'Author' cannot be empty")
                .Length(5, 50)
                .WithMessage("The field 'Author' must contains from 5 to 50 characters");

            RuleFor(b => b.Publisher)
                .Length(5, 50)
                .WithMessage("The field 'Publisher' must contains from 5 to 50 characters");
        }
    }
}
