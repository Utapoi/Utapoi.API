using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;
using Utapoi.API.Services;
using Utapoi.Application;
using Utapoi.Application.Persistence;
using Utapoi.Application.Users.Interfaces;
using Utapoi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(x =>
{
    x.AddDefaultPolicy(c =>
    {
        //c.WithOrigins("http://localhost:3000")
        //    .AllowAnyHeader()
        //    .AllowAnyMethod()
        //    .AllowAnyOrigin();

        c.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();

        //c.WithOrigins("https://karaoke.utapoi.com")
        //    .AllowAnyHeader()
        //    .AllowAnyMethod()
        //    .AllowCredentials();
    });
});


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services
    .AddControllers()
    .AddJsonOptions(x => { 
        x.JsonSerializerOptions.PropertyNamingPolicy = null;
        x.JsonSerializerOptions.WriteIndented = false;
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => { c.CustomSchemaIds(type => type?.FullName?.Replace("+", ".")); });

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();


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
    app.UseHttpsRedirection();
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


app.UseCors();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseInfrastructure();

if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "files")))
{
    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "files"));
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "files")),
    RequestPath = "/files"
});
app.MapControllers();
app.Run();