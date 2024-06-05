using BooksDBApp.DTO;
using BooksDBApp.Models;

namespace BooksDBApp.Services
{
    public interface IBookService
    {
        Book? InsertBook(BookInsertDTO dto);
        Book? UpdateBook(BookUpdateDTO dto);
        Book? DeleteBook(int id);
        Book? GetBook(int id);
        IList<Book> GetAllBooks();
    }
}
