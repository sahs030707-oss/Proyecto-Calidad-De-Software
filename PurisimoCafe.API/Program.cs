using Capa_Datos;
using Capa_Entidades;
using Capa_Negocio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Base de datos 
builder.Services.AddDbContext<PC_DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Controladores
builder.Services.AddControllers();
//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Inyección de dependencias
builder.Services.AddScoped<UsuarioDatos>();
builder.Services.AddScoped<UsuarioServicio>();

builder.Services.AddScoped<ProductoDatos>();
builder.Services.AddScoped<ProductoServicio>();

builder.Services.AddScoped<CategoriaDatos>();
builder.Services.AddScoped<CategoriaServicio>();

builder.Services.AddScoped<InventarioDatos>();
builder.Services.AddScoped<InventarioServicio>();

builder.Services.AddScoped<MovimientosInventarioDatos>();
builder.Services.AddScoped<MovimientosInventarioServicio>();

builder.Services.AddScoped<VentaDatos>();
builder.Services.AddScoped<VentaServicio>();
// Inyección de dependencias (registrar interfaces para permitir testing con Moq)
builder.Services.AddScoped<Capa_Datos.IUsuarioDatos, UsuarioDatos>();
builder.Services.AddScoped<Capa_Datos.IProductoDatos, ProductoDatos>();
builder.Services.AddScoped<Capa_Datos.ICategoriaDatos, CategoriaDatos>();
builder.Services.AddScoped<Capa_Datos.IInventarioDatos, InventarioDatos>();
builder.Services.AddScoped<Capa_Datos.IMovimientosInventarioDatos, MovimientosInventarioDatos>();
builder.Services.AddScoped<Capa_Datos.IVentaDatos, VentaDatos>();

// Configuración de JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

// Configuración de JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Swagger con título
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Purisimo Cafe Masatepe API",
        Version = "v1",
         Contact = new OpenApiContact
         {
             Name = "Soporte Tecnico - Purisimo Cafe Masatepe",
             Email = "PurisimoCafe3130@gmail.com"
         }     
    });


    // Configuración para JWT
    c.AddSecurityDefinition("Bearer", new()
{
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Ingresa el token JWT con el formato: Bearer {token}"
});

    c.AddSecurityRequirement(new()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new() { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });

   // Documentacion XML para Swagger
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

    //Habilitar anotaciones
    c.EnableAnnotations();


});



var app = builder.Build();


// Middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Purisimo Cafe Masatepe API v1");
    c.RoutePrefix = "swagger"; // Swagger carga en /swagger
});

app.UseHttpsRedirection();

// Habilitar autenticación con JWT
app.UseAuthentication();
app.UseCors("PermitirFrontend");
app.UseAuthorization();

app.MapControllers();

app.Run();