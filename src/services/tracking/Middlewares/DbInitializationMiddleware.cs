using ACC.Common.Repository;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ACC.Services.Tracking.Middlewares
{
    public class DbInitializationMiddleware
    {
        private readonly RequestDelegate _next;

        public DbInitializationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDbInitializer dbInitializer)
        {
            dbInitializer.InitializeAsync();

            await _next(context);
        }
    }
}