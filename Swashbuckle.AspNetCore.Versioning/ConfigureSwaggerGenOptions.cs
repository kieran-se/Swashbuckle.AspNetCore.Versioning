using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swashbuckle.AspNetCore.Versioning
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of IConfigureOptions&lt;SwaggerGenOptions&gt;
    /// </summary>
    public sealed class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;
        private readonly SwaggerConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerGenOptions"/> class.
        /// </summary>
        /// <param name="versionDescriptionProvider">IApiVersionDescriptionProvider</param>
        /// <param name="swaggerSettings">App Settings for Swagger</param>
        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider versionDescriptionProvider,
                                          IOptions<SwaggerConfiguration> swaggerConfiguration)
        {
            Debug.Assert(versionDescriptionProvider != null, $"{nameof(versionDescriptionProvider)} != null");
            Debug.Assert(swaggerConfiguration != null, $"{nameof(swaggerConfiguration)} != null");

            this.provider = versionDescriptionProvider;
            this.configuration = swaggerConfiguration.Value ?? new SwaggerConfiguration();
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            options.OperationFilter<SwaggerDefaultValues>();

            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();

            AddSwaggerDocumentForEachDiscoveredApiVersion(options);
            SetCommentsPathForSwaggerJsonAndUi(options);
        }

        private void AddSwaggerDocumentForEachDiscoveredApiVersion(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                configuration.Info.Version = description.ApiVersion.ToString();

                if (description.IsDeprecated)
                {
                    configuration.Info.Description += " - DEPRECATED";
                }

                options.SwaggerDoc(description.GroupName, configuration.Info);
            }
        }

        private static void SetCommentsPathForSwaggerJsonAndUi(SwaggerGenOptions options)
        {
            var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";

            if (File.Exists(xmlFile)) {
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            }
        }
    }
}
