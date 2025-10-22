using Eshop.Context;
using Eshop.Repositries;
using Eshop.Repositries.Interface;
using Eshop.Service;
using Eshop.Service.Inteterface;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//    });

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();    
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepositries>(); 
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IProductsRepository, ProductsRepositries>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, userRepositries>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IRoleRepository, RoleRepositries>();



var app = builder.Build();

// Enable Swagger & Swagger UI

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
