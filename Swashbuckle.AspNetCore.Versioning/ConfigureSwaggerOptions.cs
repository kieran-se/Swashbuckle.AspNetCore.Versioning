using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Swashbuckle.AspNetCore.Versioning
{
    /// <inheritdoc />
    public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerOptions>
    {
        private readonly SwaggerConfiguration configuration;

        /// <inheritdoc />
        public ConfigureSwaggerOptions(IOptions<SwaggerConfiguration> configuration)
        {
            this.configuration = configuration?.Value ?? new SwaggerConfiguration();
        }

        /// <inheritdoc />
        public void Configure(SwaggerOptions options)
        {
            options.RouteTemplate = configuration.RoutePrefixWithSlash + "{documentName}/swagger.json";
        }
    }
}
