using AutoMapper;
using BooksDBApp.DAO;
using BooksDBApp.DTO;
using BooksDBApp.Models;
using System.Transactions;

namespace BooksDBApp.Services
{
    public class BookServiceImpl : IBookService
    {
        private readonly IBookDAO? _bookDAO;
        private readonly IMapper? _bookMapper;
        private readonly ILogger<BookServiceImpl>? _logger;

        public BookServiceImpl(IBookDAO? bookDAO, IMapper? bookMapper, ILogger<BookServiceImpl>? logger)
        {
            _bookDAO = bookDAO;
            _bookMapper = bookMapper;
            _logger = logger;
        }

        public Book? DeleteBook(int id)
        {
            Book? bookToReturn = null;

            try
            {
                using TransactionScope scope = new();
                bookToReturn = _bookDAO!.GetById(id);

                if (bookToReturn is null) return null;
                _bookDAO.Delete(id);

                scope.Complete();

                _logger!.LogInformation($"Book with id {id} deleted successfully");
                return bookToReturn;
            } catch (Exception ex)
            {
                _logger!.LogError("An error occured while deleting book: " + ex.Message);
                throw;
            }
        }

        public IList<Book> GetAllBooks()
        {
            try
            {
                IList<Book> booksList = _bookDAO!.GetAll();
                return booksList;
            } catch (Exception ex)
            {
                _logger!.LogError("An error occured fetching books: " + ex.Message);
                throw;
            }
        }

        public Book? GetBook(int id)
        {
            try
            {
                Book? bookToReturn = null;
                bookToReturn = _bookDAO!.GetById(id);
                return bookToReturn;
            } catch (Exception ex)
            {
                _logger!.LogError($"An error occured while fetching book with id {id}: " + ex.Message);
                throw;
            }
        }

        public Book? InsertBook(BookInsertDTO dto)
        {
            if (dto is null) return null;

            var book = _bookMapper!.Map<Book>(dto);

            try
            {
                using TransactionScope scope = new();

                Book? insertedBook = _bookDAO!.Insert(book);

                scope.Complete();
                _logger!.LogInformation("New book inserted successfully!");
                return insertedBook;
            } catch (Exception ex)
            {
                _logger!.LogError($"An error occured while inserting new book: " + ex.Message);
                throw;
            }
        }

        public Book? UpdateBook(BookUpdateDTO dto)
        {
            if (dto is null) return null;

            var book = _bookMapper!.Map<Book>(dto);

            try
            {
                using TransactionScope scope = new();

                Book? updatedBook = _bookDAO!.Update(book);

                scope.Complete();
                _logger!.LogInformation("Book updated successfully!");
                return updatedBook;
            } catch (Exception ex)
            {
                _logger!.LogError("An error occured while updating book: " + ex.Message);
                throw;
            }
        }
    }
}
