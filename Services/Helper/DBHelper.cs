using System.Data.SqlClient;

namespace BooksDBApp.Services.Helper
{
    public class DBHelper
    {
        private static SqlConnection? _conn;

        public static SqlConnection? GetConnection()
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = configBuilder.Build();
            var url = configuration.GetConnectionString("BooksDBConnection");

            try
            {
                _conn = new SqlConnection(url);
                return _conn;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public static void CloseConnection()
        {
            if (_conn is not null)
            {
                _conn.Close();
            }
        }
    }
}
