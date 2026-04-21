using GoodHamburger.Core.Interfaces.Repositories;
using GoodHamburger.Core.Interfaces.Services;
using GoodHamburger.Core.Interfaces.Validators;
using GoodHamburger.Core.Services;
using GoodHamburger.Core.Validators;
using GoodHamburger.Infra.Context;
using GoodHamburger.Infra.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContextDb>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Controllers
builder.Services.AddControllers();

// Swagger + XML Comments
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Good Hamburger API",
        Version = "v1",
        Description = "Lista de Endpoints da Api"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// 🔥 Injeção de dependência


builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IOrderValidator, OrderValidator>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ContextDb>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Good Hamburger API v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext context) =>
{
    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

    return Results.Problem(
        detail: exception?.Message,
        statusCode: exception switch
        {
            ArgumentException => 400,
            KeyNotFoundException => 404,
            _ => 500
        }
    );
});

app.Run();