using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Business.Contracts;
using Business.Services;
using DataAccess;
using DataAccess.Data;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add your custom services here:
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ITodoDal, EfTodoDal>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookDal, EfBookDal>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<IStatService, StatService>();
builder.Services.AddScoped<IStatDal, EfStatDal>();
builder.Services.AddDbContext<TodoContext>();

var logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();
builder.Services.AddSingleton(logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// error loggers
app.UseExceptionHandler(handler =>
{
    handler.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var error = exceptionHandlerPathFeature.Error;
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        if (error is ValidationException)
        {
            logger.LogDebug("Validation error occured");
        }
        else if (error is NullReferenceException)
        {
            logger.LogError(error, "Null reference error");
        }
        else if (error is FormatException)
        {
            logger.LogWarning("Format error occured");
        }
        else if (error is IOException)
        {
            logger.LogCritical("I/O error occured");
        }
        else if (error is NotImplementedException)
        {
            logger.LogWarning("Not implemented error");
        }
        else
        {
            logger.LogError(error, "Unhandled exception");
        }
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    });
});

// request response
// Middleware tanımı
app.Use(ErrorHandlingMiddleware);
app.Use(RequestLoggingMiddleware);
static async Task ErrorHandlingMiddleware(HttpContext context, Func<Task> next) 
{
    try 
    {
        await next();
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "Unhandled exception");

        context.Response.StatusCode = 500;
    }
}
static async Task RequestLoggingMiddleware(HttpContext context, Func<Task> next)
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    var startTime = DateTime.UtcNow;
    var endTime = DateTime.UtcNow;
    var elapsedMs = endTime - startTime;
    var stopwatch = Stopwatch.StartNew();
    stopwatch.Stop();
    logger.LogInformation("Request took {Elapsed} ms", stopwatch.Elapsed);
    logger.LogInformation("Request took {Elapsed} ms", elapsedMs);
    logger.LogInformation("Request {method} {url}", context.Request.Method, context.Request.Path);
    
    await next();
    
    // time information response
    var elapsedMs2 = DateTime.UtcNow - startTime;
    logger.LogInformation("Response {statusCode}", context.Response.StatusCode);
    logger.LogInformation("Request took {Elapsed} ms", elapsedMs2);
    
    // performance information response
    logger.LogInformation("API execution took {Elapsed} ms", stopwatch.Elapsed);

    logger.LogInformation("Response {statusCode}",
        context.Response.StatusCode);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();