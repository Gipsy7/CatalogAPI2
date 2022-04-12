using CatalogAPI2.Models;
using CatalogAPI2.Services;
using Microsoft.AspNetCore.Authorization;

namespace CatalogAPI2.EndPoints
{
    public static class AuthenticationEndPoints
    {
        public static void MapAuthenticationEndpoints(this WebApplication app)
        {
            app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
            {
                if (userModel == null)
                {
                    return Results.BadRequest("Login Inválido");
                }
                if (userModel.UserName == "gipsy" && userModel.Password == "1234")
                {
                    var tokenString = tokenService.GenerateToken(app.Configuration["Jwt:Key"],
                        app.Configuration["Jwt:Issuer"],
                        app.Configuration["Jwt:Audience"],
                        userModel);
                    return Results.Ok(new { token = tokenString });
                }
                else
                {
                    return Results.BadRequest("Login Inválido");
                }
            }).Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status200OK)
              .WithName("Login")
              .WithTags("Authentication");
        }
    }
}
