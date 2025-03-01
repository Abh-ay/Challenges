using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Repository;
using WebApplication1.Repository.Interface;
using WebApplication1.Service;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<SaleDbContext>();
//sqlconection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connectionString);

builder.Services.AddDbContext<SaleDbContext>(options =>
    options.UseNpgsql(connectionString));



//Depdendecy Inject
builder.Services.AddScoped<ISaleRepository,SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
