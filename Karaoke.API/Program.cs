using Karaoke.API.Services;
using Karaoke.Application;
using Karaoke.Application.Persistence;
using Karaoke.Application.Users.Interfaces;
using Karaoke.Infrastructure;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(x =>
{
    x.AddDefaultPolicy(c =>
    {
        c.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();

        c.WithOrigins("https://karaoke.utapoi.com")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => { c.CustomSchemaIds(type => type?.FullName?.Replace("+", ".")); });

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
    var initializers = scope.ServiceProvider.GetServices<IInitializer>();

    foreach (var initializer in initializers)
    {
        await initializer.InitialiseAsync();
        await initializer.SeedAsync(app.Configuration);
    }
}

app.UseHttpsRedirection();
app.UseCors();
app.UseInfrastructure();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "files")),
    RequestPath = "/files"
});
app.MapControllers();
app.Run();