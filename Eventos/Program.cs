using Eventos.Service.Interface;
using Eventos.Service.Service;
using Eventos.Infra.Data.Repository;
using Eventos.Filters;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(GeneralExceptionFilter));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var chaveCrypto = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(chaveCrypto),
        ValidateIssuer = true,
        ValidIssuer = "APIClientes.com",
        ValidateAudience = true,
        ValidAudience = "APIEvents.com"
    };
});

builder.Services.AddScoped<ICityEventService, CityEventService>();
builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();
builder.Services.AddScoped<IEventReservationService, EventReservationService>();
builder.Services.AddScoped<IEventReservationRepository, EventReservationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
