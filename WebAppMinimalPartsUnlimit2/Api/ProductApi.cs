using AutoMapper;
using WebAppMinimalPartsUnlimit2.Entities.Dtos;
using WebAppMinimalPartsUnlimit2.Entities.Models;
using WebAppMinimalPartsUnlimit2.Entities;
using Microsoft.EntityFrameworkCore;


namespace WebAppMinimalPartsUnlimit2.Api
{
    public static class ProductApi
    {
        public static void ConfigureProductApi(this WebApplication app)
        {
            var group = app.MapGroup("/products");

            group.MapGet("/", async (PartsBdContext db, IMapper mapper) =>
            {
                var products = await db.Products.ToListAsync();
                return Results.Ok(mapper.Map<List<ProductDto>>(products));
            });

            group.MapGet("/{id:int}", async (int id, PartsBdContext db, IMapper mapper) =>
            {
                var product = await db.Products.FindAsync(id);
                return product is null ? Results.NotFound() : Results.Ok(mapper.Map<ProductDto>(product));
            });

            group.MapPost("/", async (ProductCreateDto productDto, PartsBdContext db, IMapper mapper) =>
            {
                var product = mapper.Map<Product>(productDto);
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return Results.Created($"/products/{product.ProductId}", mapper.Map<ProductDto>(product));
            });

            group.MapPut("/{id:int}", async (int id, ProductCreateDto productDto, PartsBdContext db, IMapper mapper) =>
            {
                var product = await db.Products.FindAsync(id);
                if (product is null) return Results.NotFound();

                mapper.Map(productDto, product);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            group.MapDelete("/{id:int}", async (int id, PartsBdContext db) =>
            {
                var product = await db.Products.FindAsync(id);
                if (product is null) return Results.NotFound();

                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return Results.Ok();
            });

        }
    }
}
