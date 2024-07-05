using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAppMinimalPartsUnlimit2.Entities;
using WebAppMinimalPartsUnlimit2.Entities.Dtos;
using WebAppMinimalPartsUnlimit2.Entities.Models;

namespace WebAppMinimalPartsUnlimit2.Api
{
    public static class CategoryApi
    {
        public static void ConfigureCategoryApi(this WebApplication app)
        {
            var group = app.MapGroup("/categories");

            group.MapGet("/", async (PartsBdContext db, IMapper mapper) =>
            {
                var categories = await db.Categories.ToListAsync();
                return Results.Ok(mapper.Map<List<CategoryDto>>(categories));
            });

            group.MapGet("/{id:int}", async (int id, PartsBdContext db, IMapper mapper) =>
            {
                var category = await db.Categories.FindAsync(id);
                return category is null ? Results.NotFound() : Results.Ok(mapper.Map<CategoryDto>(category));
            });

            group.MapPost("/", async (CategoryCreateDto categoryDto, PartsBdContext db, IMapper mapper) =>
            {
                var category = mapper.Map<Category>(categoryDto);
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return Results.Created($"/categories/{category.CategoryId}", mapper.Map<CategoryDto>(category));
            });

            group.MapPut("/{id:int}", async (int id, CategoryCreateDto categoryDto, PartsBdContext db, IMapper mapper) =>
            {
                var category = await db.Categories.FindAsync(id);
                if (category is null) return Results.NotFound();

                mapper.Map(categoryDto, category);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            group.MapDelete("/{id:int}", async (int id, PartsBdContext db) =>
            {
                var category = await db.Categories.FindAsync(id);
                if (category is null) return Results.NotFound();

                db.Categories.Remove(category);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}
