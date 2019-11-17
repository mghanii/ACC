using Microsoft.AspNetCore.Builder;

namespace ACC.Services.Tracking.Middlewares
{
    public static class Extensions
    {
        public static IApplicationBuilder UseDbInitialization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbInitializationMiddleware>();
        }
    }
}