using ScraperWeb.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/search", (string assunto) =>
{
    Scraper scraper = new();
    var response = scraper.Buscar(assunto);

    if (response == null) return Results.BadRequest("Não foram retornados resultados devido a um erro");        

    return Results.Ok(response);
});

app.Run();