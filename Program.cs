using Microsoft.OpenApi.Models;
using OnlineFoodWebAPI.Interfaces;
using OnlineFoodWebAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IMenuService, MenuService>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Swagger Documentation Section
var info = new OpenApiInfo()
{
    Title = "Your API Documentation",
    Version = "v1",
    Description = "Description of your API",
    Contact = new OpenApiContact()
    {
        Name = "Your name",
        Email = "your@email.com",
    }

};
builder.Services.AddSwaggerGen(c =>{
    c.SwaggerDoc("v1", info);

// Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

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
