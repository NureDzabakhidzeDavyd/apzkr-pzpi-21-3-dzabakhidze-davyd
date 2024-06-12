using System.Globalization;
using System.Reflection;
using System.Text;
using FluentValidation;
using Kolosok.Application.Features.BrigadeRescuer.Commands;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Application.Interfaces.Persistence;
using Kolosok.Application.Mapper;
using Kolosok.Application.Validators;
using Kolosok.Infrastructure;
using Kolosok.Infrastructure.Repositories;
using Kolosok.Persistence.Services;
using Kolosok.Presentation.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ValidationException = Kolosok.Application.Validators.ValidationException;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Kolosok API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddHttpClient();

builder.Services.AddLocalization(x => x.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
        {
            new("en-US")
            {
                DateTimeFormat =
                {
                    LongTimePattern = "MM/DD/YYYY",
                    ShortTimePattern = "MM/DD/YYYY"
                }
            },
            new("uk-UA")
            {
                //DateTimeFormat =
                //{
                //    LongTimePattern = "DD/MM/YYYY",
                //    ShortTimePattern = "DD/MM/YYYY"
                //}
            }
        };

        options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

builder.Services.AddCors((option) =>
{
    option.AddPolicy(name: "kolosok-frontend", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithExposedHeaders("content-disposition");
    });
});
builder.Services.AddAutoMapper(typeof(Mappings).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateBrigadeRescuerCommand).Assembly));

builder.Services.AddAuthentication(opt => {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            // ValidIssuer = "http://localhost:5000",
            // ValidAudience = "http://localhost:5000",
            IssuerSigningKey = new SymmetricSecurityKey("superSecretKey@345superSecretKey@345superSecretKey@345"u8.ToArray())
        };
    });


var connectionString = builder.Configuration.GetConnectionString("KolosokConnectionString");
builder.Services.AddDbContext<KolosokDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IBrigadeRepository, BrigadeRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IBrigadeRescuerRepository, BrigadeRescuerRepository>();
builder.Services.AddScoped<IVictimRepository, VictimRepository>();
builder.Services.AddScoped<IActionRepository, ActionRepository>();
builder.Services.AddScoped<IDiagnosisRepository, DiagnosisRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddScoped<IBackupRepository, BackupRepository>();
builder.Services.AddTransient<QRCodeService>();
builder.Services.AddMemoryCache();

builder.Services.AddValidatorsFromAssembly(typeof(ValidationException).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseRequestLocalization();
app.MapControllers();
app.UseCors("kolosok-frontend");

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RateLimitMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
