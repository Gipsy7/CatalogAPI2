using CatalogAPI2.AppServicesExtensions;
using CatalogAPI2.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistance();
builder.Services.AddCors();
builder.AddAuthenticationJwt();

var app = builder.Build();

app.MapCategoriesEndpoints();
app.MapProductsEndpoints();
app.MapAuthenticationEndpoints();

var environment = app.Environment;

app.UseExceptionHandling(environment)
    .UseSwaggerMiddleware()
    .UseAppCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

