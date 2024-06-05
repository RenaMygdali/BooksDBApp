using AutoMapper;
using BooksDBApp.DTO;
using BooksDBApp.Models;
using BooksDBApp.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksDBApp.Pages.Books
{
    public class UpdateModel : PageModel
    {
        public List<Error>? ErrorList { get; set; } = new();
        public BookUpdateDTO BookUpdateDTO { get; set; } = new();

        private readonly IBookService? _bookService;
        private readonly IValidator<BookUpdateDTO>? _updateDTOValidator;
        public readonly IMapper? _mapper;

        public UpdateModel(IBookService? bookService, IValidator<BookUpdateDTO>? updateDTOValidator, IMapper? mapper)
        {
            _bookService = bookService;
            _updateDTOValidator = updateDTOValidator;
            _mapper = mapper;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                Book? book = _bookService!.GetBook(id);
                BookUpdateDTO = _mapper!.Map<BookUpdateDTO>(book);
            }
            catch (Exception e)
            {
                ErrorList!.Add(new Error(e.Message));
            }
            return Page();
        }

        public void OnPost(BookUpdateDTO dto)
        {
            var validationResult = _updateDTOValidator!.Validate(dto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors) 
                {
                    ErrorList!.Add(new Error(error.ErrorMessage));
                }
                return;
            }

            try
            {
                Book? book = _bookService!.UpdateBook(dto);
                Response.Redirect("/Books/getall");
            }
            catch (Exception e)
            {
                ErrorList!.Add(new Error(e.Message));
            }
        }
    }
}
