using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Application.Interfaces.Services;
using TaskManagment.Application.Mappings;
using TaskManagment.Infrastructure.Persistence;
using TaskManagment.Infrastructure.Repositories;
using TaskManagment.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

//  Dependency Injection
builder.Services.AddScoped(typeof(TaskManagment.Application.Interfaces.Persistence.IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(MappingProfile)); 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
var services = builder.Services;
services.AddMediatR(typeof(Program));
var assembly = AppDomain.CurrentDomain.Load("TaskManagment.Application");
services.AddMediatR(assembly);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Task API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,         
        Scheme = "Bearer",                 
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    options.OperationFilter<SwaggerSecurityRequirementsOperationFilter>(); 
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "EHEHEEEHEHEHEHEHEHEH",
            ValidAudience = "EHEHEHEHEHEHEHEHEHEHEHEH",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aeheheheheheheehehehehehehehehehee"))
        };
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseAuthentication();  
 
app.UseCors("AllowAll");

//app.UseAuthorization();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();