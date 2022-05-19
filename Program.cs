using Microsoft.EntityFrameworkCore;
using SchoolApi.Models;


var builder = WebApplication.CreateBuilder(args);

// use swagger UI
// swagger route will be: /swagger/index.html
bool useSwagger = true;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SchoolContext>(opt => {
    opt.UseInMemoryDatabase("StudentList");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
if (useSwagger) {
    builder.Services.AddSwaggerGen(c =>
    {
    c.SwaggerDoc("v1", new() { Title = "SchoolApi", Version = "v1" });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    if (useSwagger) {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolApi v1"));
    }
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
