using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace BookShop.Api.Extensions
{
	public static class ApplicationBuilderExtension
	{
		public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
		{
			return app.UseExceptionHandler(errorApp =>
			{
				errorApp.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = "application/json";

					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					if (contextFeature != null)
					{
						var errorResponse = new
						{
							status = context.Response.StatusCode,
							message = "An unexpected error occurred. Please try again later."
						};

						var errorJson = JsonSerializer.Serialize(errorResponse);
						await context.Response.WriteAsync(errorJson);
					}
				});
			});
		}
	}
}
