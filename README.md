# Swashbuckle.AspNetCore.Versioning

Swagger versioning for documenting APIs built on ASP.NET Core

## Installation

Use the package manager [pip](https://pip.pypa.io/en/stable/) to install foobar.

```bash
Install-Package Swashbuckle.AspNetCore.Versioning
```

## Usage

Add to Startup.cs

```cs
using Swashbuckle.AspNetCore.Versioning;

public void ConfigureServices(IServiceCollection services)
{
    ...

    services.Configure<SwaggerConfiguration>(Configuration.GetSection(nameof(SwaggerConfiguration)));

    services.AddApiVersionWithExplorer()
        .AddSwaggerOptions()
        .AddSwaggerGen();

    ...
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...

    app.UseSwaggerDocuments();

    ...
}
```

Add to appsettings.json

```json
{
  ...
  "SwaggerConfiguration": {
    "RoutePrefix": "",
    "Info": {
      "Title": "<<API TITLE>>",
      "Description": "<<API DESCRIPTION>>",
      "TermsOfService": "",
      "Contact": {
        "Name": "<<CONTACT NAME>>",
        "Email": "<<CONTACT EMAIL>>",
        "Url": "<<CONTACT URL>>"
      }
    }
  }
  ...
}

```

On the controller add the versioning attribute or for cross version use neutral.

Will show on V1 and v2:

```cs
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthorizationController : BaseController
{
    ...
}
```
Will show on all versions:

```cs
[ApiVersionNeutral]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthorizationController : BaseController
{
    ...
}
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)