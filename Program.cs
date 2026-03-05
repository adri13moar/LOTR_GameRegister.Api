using LOTR_GameRegister.Api.Repositories;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURACIÓN DE SERVICIOS (Dependency Injection) ---

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Esto convierte Id -> id, QuestName -> questName automáticamente
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
builder.Services.AddOpenApi();

// Registro de tus 7 Repositorios
builder.Services.AddScoped<CycleRepository>();
builder.Services.AddScoped<QuestRepository>();
builder.Services.AddScoped<HeroRepository>();
builder.Services.AddScoped<DifficultyRepository>();
builder.Services.AddScoped<ResultRepository>();
builder.Services.AddScoped<ReasonForDefeatRepository>();
builder.Services.AddScoped<SphereRepository>();
builder.Services.AddScoped<GameRepository>();

// (Opcional) Configurar CORS para permitir que aplicaciones externas consulten tu API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// -----------------------------------------------------------

var app = builder.Build();

// --- 2. CONFIGURACIÓN DEL PIPELINE (Middleware) ---

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Aplicar la política de CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();