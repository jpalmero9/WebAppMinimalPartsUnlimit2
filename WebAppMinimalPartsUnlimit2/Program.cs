using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppMinimalPartsUnlimit2.Api;
using WebAppMinimalPartsUnlimit2.AutoMapper;
using WebAppMinimalPartsUnlimit2.Entities;
using WebAppMinimalPartsUnlimit2.Front;
using WebAppMinimalPartsUnlimit2.Front.Services;

namespace WebAppMinimalPartsUnlimit2
{
    public static class Program
    { 
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);            

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<PartsBdContext>(options => options.UseSqlServer(connectionString));

            builder.Services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Front/Pages");
            builder.Services.AddAuthorization();
            builder.Services.AddAutoMapper(typeof(ProductProfile));           

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRazorComponents();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }          

            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseRouting();
           
            app.MapRazorPages();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.ConfigureProductApi();
            app.ConfigureCategoryApi();

            app.Run();
        }
    }
}
