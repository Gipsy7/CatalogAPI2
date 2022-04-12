using CatalogAPI2.Context;
using CatalogAPI2.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI2.EndPoints
{
    public static class CategoriesEndPoints
    {
        public static void MapCategoriesEndpoints(this WebApplication app)
        {
            app.MapGet("/", () => "Product Catalog - 2022").ExcludeFromDescription();

            app.MapGet("/categories", async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapGet("/categories/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Categories.FindAsync(id)
                    is Category category ? Results.Ok(category) : Results.NotFound();
            });

            app.MapPost("/categories", async (Category category, AppDbContext db) =>
            {
                db.Categories?.Add(category);
                await db.SaveChangesAsync();

                return Results.Created($"/categories/{category.Id}", category);
            });

            app.MapPut("/categories/{id:int}", async (int id, Category category, AppDbContext db) =>
            {
                if (category.Id != id) return Results.BadRequest();

                var categoryDb = await db.Categories.FindAsync(id);

                if (categoryDb is null) return Results.NotFound();

                categoryDb.Name = category.Name;
                categoryDb.Description = category.Description;

                await db.SaveChangesAsync();
                return Results.Ok(categoryDb);
            });

            app.MapDelete("/categories/{id:int}", async (int id, AppDbContext db) =>
            {
                var category = await db.Categories.FindAsync(id);

                if (category is null) return Results.NotFound();

                db.Categories?.Remove(category);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}
