using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.API.Application.Commands;
using VideoGameCatalog.API.Application.Queries;
using VideoGameCatalog.API.Data;
using VideoGameCatalog.API.DTOs;
using VideoGameCatalog.API.Validation;

var builder = WebApplication.CreateBuilder(args);

// --- Services ---

// Database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=videogames.db"));

// Commands & Queries
builder.Services.AddScoped<AddVideoGameCommand>();
builder.Services.AddScoped<UpdateVideoGameCommand>();
builder.Services.AddScoped<GetAllVideoGamesQuery>();
builder.Services.AddScoped<GetVideoGameByIdQuery>();
builder.Services.AddScoped<DeleteVideoGameCommand>();


// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateVideoGameDtoValidator>();

// CORS to allow Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- Seed Data ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbSeeder.Seed(db);
}

// --- Middleware ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply CORS policy (must be before Authorization)
app.UseCors("AllowAngularClient");

app.UseAuthorization();

app.MapControllers();

app.Run();