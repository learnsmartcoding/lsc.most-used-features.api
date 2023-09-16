using Microsoft.Extensions.Primitives;
using MinimalApiChanges;
using MinimalApiChanges.Model;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Logging.AddDebug();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var logGroup = app.MapGroup("/api").AddEndpointFilter(async (context, next) =>
{
    app.Logger.LogInformation($"Request for {context.HttpContext.Request.Path} received at {DateTime.Now.ToLongTimeString()}.");

    var result = await next(context);

    app.Logger.LogInformation($"Request for {context.HttpContext.Request.Path} handled at {DateTime.Now.ToLongTimeString()}.");

    return result;
});


logGroup.MapGet("/todos", (IToDoItemRepository toDoItemRepository) =>
{
    return toDoItemRepository.GetAllToDoItems();
});

//logGroup.MapPost("/todos", (ToDoItem todoItem, IToDoItemRepository toDoItemRepository) =>
//{
//    if (todoItem == null)
//        return Results.BadRequest();

//    if (String.IsNullOrEmpty(todoItem.Title))
//        return Results.BadRequest();

//    toDoItemRepository.AddToDoItem(todoItem);

//    return Results.NoContent();

//});


//app.MapGet("/todos", (IToDoItemRepository toDoItemRepository) =>
//{
//    return toDoItemRepository.GetAllToDoItems();
//}).AddEndpointFilter(async (context, next) =>
//{

//    app.Logger.LogInformation($"Request for {context.HttpContext.Request.Path} received at {DateTime.Now.ToLongTimeString()}.");

//    var result = await next(context);

//    app.Logger.LogInformation($"Request for {context.HttpContext.Request.Path} handled at {DateTime.Now.ToLongTimeString()}.");

//    return result;
//});

//app.MapPost("/todos", (ToDoItem todoItem, IToDoItemRepository toDoItemRepository) =>
//{
//    if (todoItem == null)
//        return Results.BadRequest();

//    if (String.IsNullOrEmpty(todoItem.Title))
//        return Results.BadRequest();

//    toDoItemRepository.AddToDoItem(todoItem);

//    return Results.NoContent();

//}).AddEndpointFilter(async (context, next) =>
//{

//    app.Logger.LogInformation($"Request for {context.HttpContext.Request.Path} received at {DateTime.Now.ToLongTimeString()}.");

//    var result = await next(context);

//    app.Logger.LogInformation($"Request for {context.HttpContext.Request.Path} handled at {DateTime.Now.ToLongTimeString()}.");

//    return result;
//});


app.MapGet("/todos", (IToDoItemRepository toDoItemRepository) =>
{
    return toDoItemRepository.GetAllToDoItems();
}).AddEndpointFilter<MyEndpointFilter>();








app.Run();
