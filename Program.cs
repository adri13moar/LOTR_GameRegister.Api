using Dapper;
using LOTR_GameRegister.Api.Helpers;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using LOTR_GameRegister.Api.Repositories.Implementations;
using LOTR_GameRegister.Api.Services;
using LOTR_GameRegister.Api.Services.Interfaces;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// --- SECTION 1: CONTROLLERS & JSON CONFIGURATION ---
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

// --- SECTION 2: API DOCUMENTATION (OPENAPI / SWAGGER) ---
builder.Services.AddOpenApi();

// --- SECTION 3: DEPENDENCY INJECTION (REPOSITORIES) ---
builder.Services.AddScoped<ICycleRepository, CycleRepository>();
builder.Services.AddScoped<IDifficultyRepository, DifficultyRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IHeroRepository, HeroRepository>();
builder.Services.AddScoped<IQuestRepository, QuestRepository>();
builder.Services.AddScoped<IReasonForDefeatRepository, ReasonForDefeatRepository>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<ISphereRepository, SphereRepository>();

// --- SECTION 4: DEPENDENCY INJECTION (SERVICES) ---
builder.Services.AddScoped<IGameService, GameService>();

// --- SECTION 5: SECURITY & CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// --- SECTION 6: DAPPER CONFIGURATION ---
SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

// --- SECTION 7: HTTP REQUEST PIPELINE (MIDDLEWARE) ---
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();