namespace BooksDBApp.DTO
{
    public class BookUpdateDTO : BaseDTO
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
    }
}
