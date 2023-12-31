using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
      .AddJsonOptions(options=> 
         options.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//string mySQLLiteConnection = builder.Configuration.GetConnectionString("DefaultConnection");
string projectDirectory = Environment.CurrentDirectory;
string mySQLLiteConnection = String.Concat( "DataSource=", projectDirectory, "\\Database\\Catalogo.db");
    
builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite(mySQLLiteConnection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
