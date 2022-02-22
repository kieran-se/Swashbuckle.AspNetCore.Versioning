using Swashbuckle.AspNetCore.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<SwaggerConfiguration>(builder.Configuration.GetSection(nameof(SwaggerConfiguration)));

builder.Services.AddApiVersionWithExplorer()
    .AddSwaggerOptions()
    .AddSwaggerGen();

var app = builder.Build();

app.UseSwaggerDocuments();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
