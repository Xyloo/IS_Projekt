using IS_Projekt.Controllers;
using IS_Projekt.Database;
using IS_Projekt.Extensions;
using IS_Projekt.Repos;
using IS_Projekt.Services;
using Microsoft.EntityFrameworkCore;
using SoapCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddXmlSerializerFormatters();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=10.5.0.5,1433;Database=db;User Id=sa;Password=!root123456;TrustServerCertificate=true;"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IFileDataRepository, FileDataRepository>();
builder.Services.AddScoped<IDataRepository, DataRepository>();

builder.Services.AddScoped<IXmlService, XmlService>();
builder.Services.AddScoped<IJsonService, JsonService>();

builder.Services.AddScoped<ISoapDataService, SoapController>();
builder.Services.AddSoapCore();


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

builder.Services.AddAuthentication(auth => {
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt => {
    jwt.RequireHttpsMetadata = false;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey("hardone123hardone123hardone123"u8.ToArray()),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSoapEndpoint<ISoapDataService>("/api/DataService.asmx", new SoapEncoderOptions());

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();