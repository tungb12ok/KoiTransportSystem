using ApplicationCore.Mapping;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();

// Add Swagger configuration
builder.Services.AddSwaggerConfiguration();

// Add JWT Authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

// Add Database context
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// Register application services
builder.Services.RegisterApplicationServices();

// Add CORS configuration
builder.Services.AddCorsConfiguration();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "KoiFishTransportation API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowAllOrigins");

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
