using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Swashbuckle.AspNetCore.Versioning
{
    /// <inheritdoc cref="SwaggerUIOptions"/>>
    public sealed class ConfigureSwaggerUiOptions : IConfigureOptions<SwaggerUIOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;
        private readonly SwaggerConfiguration configuration;

        /// <inheritdoc />
        public ConfigureSwaggerUiOptions(IApiVersionDescriptionProvider versionDescriptionProvider, IOptions<SwaggerConfiguration> settings)
        {
            Debug.Assert(versionDescriptionProvider != null, $"{nameof(versionDescriptionProvider)} != null");
            Debug.Assert(settings != null, $"{nameof(versionDescriptionProvider)} != null");

            this.provider = versionDescriptionProvider;
            this.configuration = settings?.Value ?? new SwaggerConfiguration();
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerUIOptions options)
        {
            provider
                .ApiVersionDescriptions
                .ToList()
                .ForEach(description =>
                {
                    options.SwaggerEndpoint(
                        $"/{configuration.RoutePrefixWithSlash}{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());

                    options.RoutePrefix = configuration.RoutePrefix;
                });
        }
    }
}
