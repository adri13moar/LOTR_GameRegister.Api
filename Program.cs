using Dapper;
using LOTR_GameRegister.Api.Helpers;
using LOTR_GameRegister.Api.Repositories.Implementations;
using LOTR_GameRegister.Api.Repositories.Interfaces;
using LOTR_GameRegister.Api.Services;
using LOTR_GameRegister.Api.Services.Interfaces;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// --- SECTION 1: CONTROLLERS ---
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

// --- SECTION 2: API DOCUMENTATION ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "LOTR Game Register API",
        Version = "v1",
        Description = "Specialized API for tracking and analyzing match results from 'The Lord of the Rings: The Card Game'. " +
                        "It automates game registration, tracks hero performance, and calculates deck statistics. " +
                        "\n\n" +
                        "**Source Code:** [https://github.com/adri13moar](https://github.com/adri13moar)",
        Contact = new()
        {
            Name = "Adrián Molina Arroyo",
            Url = new Uri("https://www.linkedin.com/in/molinaarroyoadrian")
        },
        License = new()
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// --- SECTIONS 3, 4, 5, 6 (REPOSITORIES AND SERVICES) ---
builder.Services.AddScoped<ICycleRepository, CycleRepository>();
builder.Services.AddScoped<IDifficultyRepository, DifficultyRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IHeroRepository, HeroRepository>();
builder.Services.AddScoped<IQuestRepository, QuestRepository>();
builder.Services.AddScoped<IReasonForDefeatRepository, ReasonForDefeatRepository>();
builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddScoped<ISphereRepository, SphereRepository>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

// --- SECTION 7: MIDDLEWARE ---
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LOTR API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();