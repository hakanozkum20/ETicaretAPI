using System.Text;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Persistence;
using ETicaretAPI.Infrastructure;
using FluentValidation.AspNetCore;
using ETicaretAPI.Infrastructure.Services.Storage.Google;
using ETicaretAPI.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddPersistenceServices();

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();


// builder.Services.AddStorage(StorageType.Azure);
// builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<GoogleStorage>();


// builder.Services.AddStorage(StorageType.Local);

// builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
//     policy.WithOrigins("http://localhost:4200","https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
// )); 
builder.Services.AddCors(options => {options.AddPolicy("Coriolis", policy => policy.WithOrigins("https://localhost:4200", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod());}); 

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
.AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter= true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin",options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        
        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            
    };
} );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(); 
app.UseHttpsRedirection();
app.UseCors("Coriolis");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();