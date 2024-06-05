using BooksDBApp.Models;
using BooksDBApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksDBApp.Pages.Books
{
    public class DeleteModel : PageModel
    {
        public List<Error> ErrorList { get; set; } = new();

        private readonly IBookService? _bookService;

        public DeleteModel(IBookService? bookService)
        {
            _bookService = bookService;
        }

        public void OnGet(int id)
        {
            try
            {
                Book? book = _bookService!.DeleteBook(id);
                Response.Redirect("/Books/getall");

            } catch (Exception ex)
            {
                ErrorList.Add(new Error(ex.Message));
            }
        }
    }
}
