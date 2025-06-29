using Application;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json.Serialization;
using Application.Exceptions;
using FluentValidation;
using Persistence;
using WebApp;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.RegisterApplication(builder.Configuration);

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

builder.Services.AddSwaggerGen(options => { options.CustomSchemaIds(type => type.FullName.Replace("+", ".")); });


builder.Services.AddDbContext<ApplicationDbContext>(options => options
    .UseSqlite($"Data Source=data.db")
);

WebApplication app = builder.Build();

app.UseExceptionHandler(a => a.Run(async context =>
{
    IExceptionHandlerPathFeature? exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    Exception? exception = exceptionHandlerPathFeature?.Error;
    if (exception is AggregateException aggEx)
        exception = aggEx.InnerExceptions.First();

    switch (exception)
    {
        case ValidationException validationEx:
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new { validationEx.Message, validationEx.Errors });

            break;
        }
        case MyApplicationException appEx:
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new { appEx.Message, appEx.Payload });

            break;
        }
        default:
            await context.Response.WriteAsJsonAsync(new { error = exceptionHandlerPathFeature?.Error?.Message });
            break;
    }
}));

// Configure the HTTP request pipeline.
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

await DbSeeder.SeedAsync(app.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>()!);

app.Run();