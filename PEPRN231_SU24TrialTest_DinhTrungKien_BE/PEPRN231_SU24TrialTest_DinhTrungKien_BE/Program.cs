using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.Mapper;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.Services.Implements;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Business.Services.Interfaces;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Models;
using PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Repository.UnitOfWork;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddControllers();
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Style>("Style").HasManyBinding(s => s.WatercolorsPaintings, "WatercolorsPainting");
modelBuilder.EntitySet<WatercolorsPainting>("WatercolorsPainting");
builder.Services.AddControllers().AddOData(
    options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
        "odata",
        modelBuilder.GetEdmModel()));

// JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireClaim("Role", "1"));
    options.AddPolicy("RequireStaffRole", policy => policy.RequireClaim("Role", "2"));
    options.AddPolicy("RequireManagerRole", policy => policy.RequireClaim("Role", "3"));
    options.AddPolicy("RequireCustomerRole", policy => policy.RequireClaim("Role", "3"));
    options.AddPolicy("RequireStaffOrManagerRole", policy => policy.RequireClaim("Role", "2", "3"));
});

builder.Services.AddDbContext<WatercolorsPainting2024DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IWatercolorsPaintingService, WatercolorsPaintingService>();

builder.Services.AddAutoMapper(typeof(Program), typeof(Mapping));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
