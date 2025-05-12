using BookShop.Api.EF;
using BookShop.Api.EF.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

// Add services to the container.S

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(currentAssemblies);

builder.Services.AddDbContext<BookShopContext>(options =>
{
	options.UseInMemoryDatabase("BookShopDb");
});

builder.Services.RegisterRepositories();

builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	//app.MapOpenApi();

	// Serve the generated Swagger document as JSON.
	app.UseSwagger();

	// Serve the Swagger UI, which provides a web-based API explorer.
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
