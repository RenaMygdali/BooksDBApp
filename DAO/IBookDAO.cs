using BooksDBApp.Models;

namespace BooksDBApp.DAO
{
    public interface IBookDAO
    {
        Book? Insert(Book? book);
        Book? Update(Book? book);
        void Delete(int id);
        Book? GetById(int id);
        IList<Book> GetAll();
    }
}
