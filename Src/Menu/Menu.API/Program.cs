using Menu.API.Dto.Mapper;
using Menu.API.Services;
using Menu.API.Services.Impl;
using Menu.DataAccess.Models;
using Menu.DataAccess.Repository;
using Menu.DataAccess.Repository.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MyMapper).Assembly);
builder.Services.AddDbContext<MenuDbContext>(
              options => options.UseSqlServer(builder.Configuration.GetConnectionString("LoadDb")));

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));

    options.AddPolicy("View", policy => policy.RequireClaim("Permission", "View"));
    options.AddPolicy("Edit", policy => policy.RequireClaim("Permission", "Edit"));
    options.AddPolicy("Create", policy => policy.RequireClaim("Permission", "Create"));
    options.AddPolicy("Delete", policy => policy.RequireClaim("Permission", "Delete"));
});
builder.Services.AddHttpClient<IRecipeService,RecipeService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5298"); // URL của Recipe.API
});

//builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
/*
builder.Services.AddHttpClient<MenuService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5298"); // Địa chỉ của RecipeService
});
*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
