using PruebaAPIPostgreSQLEF.Data;
using Microsoft.EntityFrameworkCore;
using PruebaAPIPostgreSQLEF.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connetionString = builder.Configuration.GetConnectionString("PostgreSQLConnetion");
builder.Services.AddDbContext<DirectoryInformation>(options =>
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

app.MapPost("/phonebook/", async (Phonebook e, DirectoryInformation db) =>
{
    db.Phonebook.Add(e);
    await db.SaveChangesAsync();

    return Results.Created($"/phonebook/{e.Id}", e);

});

app.MapGet("/phonebook/{id:int}", async (int id, DirectoryInformation db) =>
{
    return await db.Phonebook.FindAsync(id)
        is Phonebook e
        ? Results.Ok(e)
        : Results.NotFound();

});


app.MapGet("/phonebook", async (DirectoryInformation db) =>
{
    var phonebookEntries = await db.Phonebook.OrderBy(entry => entry.Id).ToListAsync();
    return phonebookEntries;
});

app.MapPut("/phonebook/{id:int}", async (int id, Phonebook e, DirectoryInformation db) =>
{
    if (e.Id != id)
        return Results.BadRequest();

    var contact = await db.Phonebook.FindAsync(id);

    if (contact is null) return Results.NotFound();

    contact.FirstName = e.FirstName;
    contact.LastName = e.LastName;
    contact.PhoneNumber = e.PhoneNumber;
    contact.TextComments = e.TextComments;

    await db.SaveChangesAsync();

    return Results.Ok(contact);

});

app.MapDelete("/phonebook/{id:int}", async (int id, DirectoryInformation db) =>
{
    var contact = await db.Phonebook.FindAsync(id);
    if (contact is null) return Results.NotFound();

    db.Phonebook.Remove(contact);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

