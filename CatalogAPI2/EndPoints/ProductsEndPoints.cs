using CatalogAPI2.Context;
using CatalogAPI2.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI2.EndPoints
{
    public static class ProductsEndPoints
    {
        public static void MapProductsEndpoints(this WebApplication app)
        {
            app.MapGet("/products", async (AppDbContext db) => await db.Products.ToListAsync()).WithTags("Products").RequireAuthorization();

            app.MapGet("/products/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Products.FindAsync(id)
                    is Product product ? Results.Ok(product) : Results.NotFound();
            }).WithTags("Products");

            app.MapPost("/products", async (Product product, AppDbContext db) =>
            {
                db.Products?.Add(product);
                await db.SaveChangesAsync();

                return Results.Created($"/categories/{product.Id}", product);
            }).WithTags("Products");

            app.MapPut("/products/{id:int}", async (int id, Product product, AppDbContext db) =>
            {
                if (product.Id != id) return Results.BadRequest();

                var productDb = await db.Products.FindAsync(id);

                if (productDb is null) return Results.NotFound();

                productDb.Name = product.Name;
                productDb.Description = product.Description;

                await db.SaveChangesAsync();
                return Results.Ok(productDb);
            }).WithTags("Products");

            app.MapDelete("/products/{id:int}", async (int id, AppDbContext db) =>
            {
                var product = await db.Products.FindAsync(id);

                if (product is null) return Results.NotFound();

                db.Products?.Remove(product);
                await db.SaveChangesAsync();

                return Results.NoContent();
            }).WithTags("Products");
        }
    }
}
