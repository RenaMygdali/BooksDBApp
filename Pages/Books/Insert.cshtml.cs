using BooksDBApp.DTO;
using BooksDBApp.Models;
using BooksDBApp.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksDBApp.Pages.Books
{
    public class InsertModel : PageModel
    {
        public List<Error> ErrorList { get; set; } = new();
        public BookInsertDTO BookInsertDTO { get; set; } = new();

        private readonly IBookService? _bookService;
        private readonly IValidator<BookInsertDTO>? _bookInsertValidator;

        public InsertModel(IBookService? bookService, IValidator<BookInsertDTO>? bookInsertValidator)
        {
            _bookService = bookService;
            _bookInsertValidator = bookInsertValidator;
        }

        public void OnGet()
        {

        }

        public void OnPost(BookInsertDTO dto)
        {
            var validationResult = _bookInsertValidator!.Validate(dto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ErrorList.Add(new Error(error.ErrorMessage));
                }
                return;
            }
             try
            {
                Book? book = _bookService!.InsertBook(dto);
                Response.Redirect("/Books/getall");
            } catch (Exception ex)
            {
                ErrorList.Add(new Error(ex.Message));
            }
        }
    }
}
