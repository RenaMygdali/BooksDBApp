using AutoMapper;
using BooksDBApp.DTO;
using BooksDBApp.Models;

namespace BooksDBApp.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<BookInsertDTO, Book>().ReverseMap();
            CreateMap<BookUpdateDTO, Book>().ReverseMap();
            CreateMap<BookReadOnlyDTO, Book>().ReverseMap();
        }
    }
}
