﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ACC.Services.Tracking.Middlewares
{
    public static class Extensions
    {
        public static IApplicationBuilder UseDbInitialization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbInitializationMiddleware>();
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            var logger = app.ApplicationServices.GetService<ILogger>();
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new
                        {
                            code = context.Response.StatusCode,
                            message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}