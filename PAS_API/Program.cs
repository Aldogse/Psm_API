using Microsoft.EntityFrameworkCore;
using PAS_API.Data;
using PAS_API.Interface;
using PAS_API.Middleware;
using PAS_API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<PAS_API_DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("azure_connection"));
});
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<ApiKeyMiddleWare>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

