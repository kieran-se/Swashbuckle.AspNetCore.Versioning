using Microsoft.AspNetCore.Builder;

namespace Swashbuckle.AspNetCore.Versioning
{
    /// <summary>
    /// Extending Swagger services
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Enabling Swagger UI.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        public static void UseSwaggerDocuments(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
