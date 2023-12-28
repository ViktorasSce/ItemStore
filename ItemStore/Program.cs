using DbUp;
using ItemStore.Contexts;
using ItemStore.Interfaces;
using ItemStore.Middlewares;
using ItemStore.Repositories;
using ItemStore.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped<IItemRepository, ItemRepository>();
//builder.Services.AddScoped<IItemService, ItemService>();

builder.Services.AddTransient<ItemServiceEF>();
builder.Services.AddTransient<IItemRepositoryEF, ItemRepositoryEF>();

string dbConnectionString = builder.Configuration.GetConnectionString("PostgreConnection");
//builder.Services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(dbConnectionString)); //dapper connection to db
//builder.Services.AddDbContext<DataContext>(o => o.UseInMemoryDatabase("DB")); // EF core save in memory
builder.Services.AddDbContext<DataContext>(o => o.UseNpgsql(dbConnectionString)); // EF core with DB connection


//var upgrader =
//    DeployChanges.To
//        .PostgresqlDatabase(dbConnectionString)
//        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
//        .LogToConsole()
//        .Build();

//var result = upgrader.PerformUpgrade();

//if (!result.Successful)
//{
//    Console.ForegroundColor = ConsoleColor.Red;
//    Console.WriteLine(result.Error);
//    Console.ResetColor();
//#if DEBUG
//    Console.ReadLine();
//#endif

//}

//Console.ForegroundColor = ConsoleColor.Green;
//Console.WriteLine("Success!");
//Console.ResetColor();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
