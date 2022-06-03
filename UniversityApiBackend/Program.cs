using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityApiBackend;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;
using Serilog;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using UniversityApiBackend.Utilities;
using System.Text.Json.Serialization;
//10. use Serilog to log events
var builder = WebApplication.CreateBuilder(args);



//11 config Serilog
builder.Host.UseSerilog((hostBuilderCtx, loggerCof) =>
{
    loggerCof.
    WriteTo.Console()
    .WriteTo.Debug()
    .ReadFrom.Configuration(hostBuilderCtx.Configuration);

});

// Add services to the container.
//1. Localization 
builder.Services.AddLocalization(options => options.ResourcesPath = "Resoources");
//2. Connection with SQL Server Express
const string CONNECTIONNAME= "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);
builder.Services.AddTransient<DataSeed>();

//3. Add Context
builder.Services.AddDbContext<UniversityDbContext>(options=>options.UseSqlServer(connectionString));

//7. Add Service of Jwt Autorization
builder.Services.AddJwtTokenServices(builder.Configuration);

builder.Services.AddScoped<IStudentService,StudentService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ICoursesService, CoursesService>();
builder.Services.AddScoped<IChaptersService, ChaptersService>();
builder.Services.AddScoped<IUserHelper, UserHelper>();


builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//Add services

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//8. Add Authorization
builder.Services.AddAuthorization(options =>
{
   // options.AddPolicy("Administrator", policy => policy.RequireClaim("role", "admin"));
    //options.AddPolicy("IsAdmin", policy => policy.RequireClaim("role", "admin"));
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("role", "user"));
});

//9 TODO: Config to take care of  Autorization of  JWT
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {

        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme",


    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
             new OpenApiSecurityScheme
             {
                 Reference= new OpenApiReference
                 {
                     Type=ReferenceType.SecurityScheme,
                     Id="Bearer"
                 }
             },
              new string[]{}

        }


    });

});

//5. CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();

    });

});

builder.Services.AddIdentity<User, IdentityRole>()
                             .AddEntityFrameworkStores<UniversityDbContext>()
                             .AddRoles<IdentityRole>()
                             .AddDefaultTokenProviders();



var app = builder.Build();

//2. supported culture
var supportedCultures = new[] { "en-US", "es-ES", "fr-FR","de-DE" };
var locaozationOptiona = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);



    SeedData(app);

//Seed Data
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeed>();
        service.SeedAsync().Wait();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//12.Tell app to use  Serilog 
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// 6 Tell app to user cors
app.UseCors("CorsPolicy");

app.Run();
