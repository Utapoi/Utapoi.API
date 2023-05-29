using Karaoke.Infrastructure;
using Karaoke.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

app.UseAuthorization();
app.MapControllers();
app.Run();