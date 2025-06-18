using Microsoft.EntityFrameworkCore;
using RestfulAPIBook.Data;
using RestfulAPIBook.Repository.Implements;
using RestfulAPIBook.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
//______________________Goi Repository____________________________
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
//__________________________________________________________________
//----------------------ket noi sql-----------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookRestfulAPIConnectionString"));
});
//----------------------------------------------------------
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
//---------------Tao SwaggerUI-------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//--------------------------------------------------
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
