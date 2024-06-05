using AutoMapper;
using BooksDBApp.Config;
using BooksDBApp.DAO;
using BooksDBApp.DTO;
using BooksDBApp.Services;
using BooksDBApp.Validators;
using FluentValidation;
using Serilog;

namespace BooksDBApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // serilog service config
            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });

            builder.Services.AddRazorPages();

            builder.Services.AddScoped<IBookDAO, BookDAOImpl>();
            builder.Services.AddScoped<IBookService, BookServiceImpl>();
            builder.Services.AddAutoMapper(typeof(MapperConfig));
            builder.Services.AddScoped<IValidator<BookInsertDTO>, BookInsertValidator>();
            builder.Services.AddScoped<IValidator<BookUpdateDTO>, BookUpdateValidator>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
