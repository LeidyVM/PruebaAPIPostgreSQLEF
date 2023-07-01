using PruebaAPIPostgreSQLEF.Data;
using Microsoft.EntityFrameworkCore;
using PruebaAPIPostgreSQLEF.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connetionString = builder.Configuration.GetConnectionString("PostgreSQLConnetion");
builder.Services.AddDbContext<InfoDirectorio>(options =>
    options.UseNpgsql(connetionString));

// Configuraci�n de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapPost("/phonebook/", async (Directorio e, InfoDirectorio db) =>
{
    db.Directorio.Add(e);
    await db.SaveChangesAsync();

    return Results.Created($"/phonebook/{e.Id}", e);

});

app.MapGet("/phonebook/{id:int}", async (int id, InfoDirectorio db) =>
{
    return await db.Directorio.FindAsync(id)
        is Directorio e
        ? Results.Ok(e)
        : Results.NotFound();

});

app.MapGet("/phonebook", async (InfoDirectorio db) => await db.Directorio.ToListAsync());

app.MapPut("/phonebook/{id:int}", async (int id, Directorio e, InfoDirectorio db) =>
{
    if (e.Id != id)
        return Results.BadRequest();

    var contacto = await db.Directorio.FindAsync(id);

    if (contacto is null) return Results.NotFound();

    contacto.FirstName = e.FirstName;
    contacto.LastName = e.LastName;
    contacto.PhoneNumber = e.PhoneNumber;
    contacto.TextComments = e.TextComments;

    await db.SaveChangesAsync();

    return Results.Ok(contacto);

});

app.MapDelete("/phonebook/{id:int}", async (int id, InfoDirectorio db) =>
{
    var contacto = await db.Directorio.FindAsync(id);
    if (contacto is null) return Results.NotFound();

    db.Directorio.Remove(contacto);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
