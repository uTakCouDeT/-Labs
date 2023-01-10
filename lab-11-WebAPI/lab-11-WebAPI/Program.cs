using Microsoft.EntityFrameworkCore;
using WebEmployees;
using WebApiModel;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ISuppliers, Supp>();
builder.Services.AddDbContext<Northwind>(opt =>
    opt.UseSqlServer("Data Source=localhost;Initial Catalog=Northwind;User ID=sa;Password=HelloWorld10;Encrypt=False",
        providerOptions => { providerOptions.EnableRetryOnFailure(); }));
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new() { Title = "TodoApi", Version = "v1" }); });

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();