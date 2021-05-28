using Microsoft.OpenApi.Models;

namespace Swashbuckle.AspNetCore.Versioning
{
    /// <summary>
    /// Swagger Configuration
    /// </summary>
    public class SwaggerConfiguration
    {
        public string Name { get; set; }
        public OpenApiInfo Info { get; set; }
        public string RoutePrefix { get; set; }
        public string RoutePrefixWithSlash =>
            string.IsNullOrWhiteSpace(RoutePrefix)
                ? string.Empty
                : RoutePrefix + "/";
    }
}
