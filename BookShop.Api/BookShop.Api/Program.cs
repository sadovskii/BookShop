using BookShop.Api.EF;
using BookShop.Api.EF.DependencyInjection;
using BookShop.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

builder.Services.AddControllers()
	.AddNewtonsoftJson();

builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(currentAssemblies);

builder.Services.AddDbContext<BookShopContext>(options =>
{
	options.UseInMemoryDatabase("BookShopDb");
});

builder.Services.RegisterRepositories();

builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseGlobalExceptionHandler();

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<BookShopContext>();
	context.Database.EnsureCreated();
}

app.UseRouting();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthorization();

//app.UseSpa(spa =>
//{
//	//spa.Options.StartupTimeout = TimeSpan.FromSeconds(120);
//	// Point to the external Angular project.
//	spa.Options.SourcePath = "../../BookShop.Web";
//	// Alternatively, if your folder structure allows a relative path:
//	// spa.Options.SourcePath = "../../YourAngularApp"; 

//	if (app.Environment.IsDevelopment())
//	{
//		// This will launch the Angular CLI server by running `npm start` in the given folder.
//		ProcessStartInfo psi = new ProcessStartInfo
//		{
//			FileName = "npm",
//			Arguments = "start",
//			WorkingDirectory = spa.Options.SourcePath,
//			UseShellExecute = true,
//		};

//		Process? process = Process.Start(psi);

//		//spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
//	}
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	//app.MapOpenApi();

	// Serve the generated Swagger document as JSON.
	app.UseSwagger();

	// Serve the Swagger UI, which provides a web-based API explorer.
	app.UseSwaggerUI();

	ProcessStartInfo psi = new ProcessStartInfo
	{
		FileName = "npm",
		Arguments = "start",
		WorkingDirectory = "../../BookShop.Web",
		UseShellExecute = true,
		//WindowStyle = ProcessWindowStyle.Hidden
	};

	Process? process = Process.Start(psi);
}

app.MapControllers();

app.Run();
