using Microsoft.EntityFrameworkCore;
using MotorcycleRent.API.Middleware;
using Motorcycles.Application;
using Motorcycles.Infraestructure;
using Motorcycle.Consumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddInfraestructure(builder.Configuration)
    .AddApplication()
    .AddConsumer();



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Executar migrações automaticamente
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); // Isso aplica as migrações automaticamente
        Console.WriteLine("Migrações aplicadas com sucesso!");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrações");
        // Não throw - permite que a aplicação continue mesmo com erro de migração
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();
