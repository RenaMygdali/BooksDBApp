using AutoMapper;
using BooksDBApp.DTO;
using BooksDBApp.Models;
using BooksDBApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksDBApp.Pages.Books
{
    public class IndexModel : PageModel
    {
        public List<BookReadOnlyDTO>? Books { get; set; } = new();
        public Error ErrorObj { get; set; } = new();

        private readonly IMapper? _mapper;
        private readonly IBookService? _bookService;

        public IndexModel(IMapper? mapper, IBookService? bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        public IActionResult OnGet()
        {
            try
            {
                IList<Book> booksList = _bookService!.GetAllBooks();
                foreach (Book book in booksList)
                {
                    BookReadOnlyDTO bookDTO = _mapper!.Map<BookReadOnlyDTO>(book);
                    Books!.Add(bookDTO);
                }
            } catch (Exception ex) 
            {
                ErrorObj = new Error(ex.Message);
            }
            return Page();
        }
    }
}
