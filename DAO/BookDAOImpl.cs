using BooksDBApp.Models;
using BooksDBApp.Services.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace BooksDBApp.DAO
{
    public class BookDAOImpl : IBookDAO
    {
        public void Delete(int id)
        {
            string query = "DELETE FROM BOOKS WHERE ID = @id";

            using SqlConnection? conn = DBHelper.GetConnection();

            if (conn  != null)
            {
                conn.Open();
            }

            using SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        public IList<Book> GetAll()
        {
            string query = "SELECT * FROM BOOKS";
            Book? book;
            var booksList = new List<Book>();

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn != null) conn.Open();

            using SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                book = new Book() 
                { 
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Title = reader.GetString(reader.GetOrdinal("TITLE")),
                    Author = reader.GetString(reader.GetOrdinal("AUTHOR")),
                    Publisher = reader.GetString(reader.GetOrdinal("PUBLISHER"))
                };
                booksList.Add(book);
            }
            return booksList;
        }

        public Book? GetById(int id)
        {
            string query = "SELECT * FROM BOOKS WHERE ID = @id";
            Book? book = null;

            using SqlConnection? conn = DBHelper.GetConnection();

            if (conn != null)
            {
                conn.Open();
            }

            using SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@id", id);
            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                book = new Book()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Title = reader.GetString(reader.GetOrdinal("TITLE")),
                    Author = reader.GetString(reader.GetOrdinal("AUTHOR")),
                    Publisher = reader.GetString(reader.GetOrdinal("PUBLISHER"))
                };
            }

            return book;
        }

        public Book? Insert(Book? book)
        {
            string insertQuery = "INSERT INTO BOOKS (TITLE, AUTHOR, PUBLISHER) VALUES (@title, @author, @publisher); " +
                "SELECT SCOPE_IDENTITY();";
            int insertedId = 0;

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn != null) conn.Open();

            using SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
            insertCommand.Parameters.AddWithValue("@title", book!.Title);
            insertCommand.Parameters.AddWithValue("@author", book.Author);
            insertCommand.Parameters.AddWithValue("@publisher", book.Publisher);
            
            object insertedObj = insertCommand.ExecuteScalar();

            if (insertedObj is not null)
            {
                if (!int.TryParse(insertedObj.ToString(), out insertedId))
                {
                    throw new Exception("Error in insert id");
                }
            }

            string? selectQuery = "SELECT * FROM BOOKS WHERE ID = @id";
            Book? bookToReturn = null;

            using SqlCommand selectCommand = new SqlCommand(selectQuery, conn);
            selectCommand.Parameters.AddWithValue("@id", insertedId);
            using SqlDataReader reader = selectCommand.ExecuteReader();

            if (reader.Read())
            {
                bookToReturn = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Title = reader.GetString(reader.GetOrdinal("TITLE")),
                    Author = reader.GetString(reader.GetOrdinal("AUTHOR")),
                    Publisher = reader.GetString(reader.GetOrdinal("PUBLISHER"))
                };
            }
            return bookToReturn;
        }

        public Book? Update(Book? book)
        {
            if (book is null) return null;
                
            string query = "UPDATE BOOKS SET TITLE = @title, AUTHOR = @author, PUBLISHER = @publisher WHERE ID = @id";

            using SqlConnection? conn = DBHelper.GetConnection();
            if (conn != null) conn.Open();

            using SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@title", book!.Title);
            command.Parameters.AddWithValue("@author", book.Author);
            command.Parameters.AddWithValue("@publisher", book.Publisher);
            command.Parameters.AddWithValue("@id", book.Id);

            command.ExecuteNonQuery();

            return book;
        }
    }
}
