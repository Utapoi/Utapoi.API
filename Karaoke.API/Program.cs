using Karaoke.API.Services;
using Karaoke.Application;
using Karaoke.Application.Interfaces;
using Karaoke.Infrastructure;
using Karaoke.Infrastructure.Persistence;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type?.FullName?.Replace("+", "."));

    c.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "Authorization",
        BearerFormat = "Bearer {token}"
    });
});

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId("SwaggerAPI");
        c.OAuthAppName("Karaoke.API");
        c.OAuthUsePkce();
    });
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHttpsRedirection();
}

// Initialise and seed database
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<AuthDbContextInitializer>();
    await initializer.InitialiseAsync();
    await initializer.SeedAsync(app.Configuration);
}

app.UseInfrastructure();
app.MapControllers();
app.Run();