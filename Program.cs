using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApiGitHub.Data;
using WebApiGitHub.Features.Players.CreatePlayer;
using WebApiGitHub.Features.Players.GetPlayerById;
using WebApiGitHub.Interfaces;
using WebApiGitHub.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// builder.Services.AddDbContext<AppDbContext>(opt =>
// 	opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
// );
builder.Services.AddDbContext<VideGameAppDbContext>(opt => opt.UseInMemoryDatabase("CQRS"));
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("UnitOfWork"));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
	config.DocumentName = "TodoAPI";
	config.Title = "TodoAPI v1";
	config.Version = "v1";
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
	app.UseOpenApi();
	app.UseSwaggerUi(config =>
	{
		config.DocumentTitle = "TodoAPI";
		config.Path = "/swagger";
		config.DocumentPath = "/swagger/{documentName}/swagger.json";
		config.DocExpansion = "list";
	});
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
  "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
	var forecast = Enumerable.Range(1, 5).Select(index =>
			new WeatherForecast
			(
				DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				Random.Shared.Next(-20, 55),
				summaries[Random.Shared.Next(summaries.Length)]
			))
			.ToArray();
	return forecast;
})
.WithName("GetWeatherForecast");


app.MapPost("/CreatePlayer", async (ISender _sender, CreatePlayerCommand _command) =>
{
	var playerId = await _sender.Send(_command);
	return Results.Ok(playerId);
});


app.MapGet("/GetPlayers/{id}", async (int id, ISender _sender) =>
{
	var player = await _sender.Send(new GetPlayerByIdQuery(id));
	if (player == null) return Results.NoContent();
	return Results.Ok(player);
});

app.MapGet("/GetAllPlayers", async (ISender _sender) =>
{
	var players = await _sender.Send(new GetAllPlayersQuery());
	if (players == null) return Results.NoContent();
	return Results.Ok(players);
});


// Unit of Work Processing
app.MapGet("/ui/GetProducts", async (IUnitOfWork _ui) =>
{
	var products = await _ui.Products.GetAllAsync();
	return Results.Ok(products);
});

app.MapGet("/ui/GetProductById/{id}", async (int id, IUnitOfWork _ui) =>
{
	var product = await _ui.Products.GetByIdAsync(id);
	return Results.Ok(product);
});

app.MapPost("/ui/CreateProduct", async (Product product, IUnitOfWork _ui) =>
{
	await _ui.Products.AddAsync(product);
	await _ui.SaveChangesAsync();
	return Results.Ok();
});

app.MapPut("/ui/update/{id}", async (int id, Product product, IUnitOfWork _ui) =>
{
	var prod = await _ui.Products.GetByIdAsync(id);
	prod!.Name = product.Name;
	await _ui.SaveChangesAsync();
	return Results.Ok("The product ${id} has been delete");
});

app.MapDelete("/ui/remove/{id}", async (int id, IUnitOfWork _ui) =>
{
	var product = await _ui.Products.GetByIdAsync(id);
	_ui.Products.Remove(product!);
	await _ui.SaveChangesAsync();	
	return Results.Ok();
});


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

