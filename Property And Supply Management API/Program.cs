using Microsoft.EntityFrameworkCore;
using Property_And_Supply_Management_API.Data;
using Property_And_Supply_Management_API.Interfaces;
using Property_And_Supply_Management_API.Repository;
using Property_And_Supply_Management_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<PSMdbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default_Connection"));
});
builder.Services.AddScoped<IItemRepository,ItemRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
//app.UseMiddleware<ApiKeyMiddleware>();

app.Run();

