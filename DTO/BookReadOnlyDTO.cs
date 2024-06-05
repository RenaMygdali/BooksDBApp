namespace BooksDBApp.DTO
{
    public class BookReadOnlyDTO : BaseDTO
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
    }
}
