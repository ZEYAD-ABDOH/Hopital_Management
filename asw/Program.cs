using asw.Data;
using asw.Filters;
using asw.Middlewares;
using asw.Service;
using Jose;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//Connect  mamage :1- «·«œ«¡ 2-«·„Ê«—œ 3-Êﬁ  «·«” Ã«»…
// Add services to the container.

builder.Configuration.AddJsonFile("confin.json");

var cont = builder.Configuration.GetConnectionString("Defaultconnection");


builder.Services.AddDbContext<My_db_s>(options=>options.UseSqlServer(cont));

//œ„Ã »Ì‰ «·Ê«ÃÂÂ Ê «·ﬂ·«”

builder.Services.AddScoped<GetToken, CgetToken>();

//builder.Services.AddScoped<IGetTokenZeyad,GetTokenZeyad>();
//«·Ê’Ê· «·Ï «⁄œ«œ«  

//builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

//builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.Configure<AttOpation>(builder.Configuration.GetSection("Attch"));

var jwtOpations = builder.Configuration.GetSection("Jwt").Get<JwtOpations>();
builder.Services.AddAuthentication(options =>
     {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
     }
).AddJwtBearer(options =>

{
    //options.SaveToken=true;
    options.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience =  builder.Configuration["Jwt:Aidoence"],// jwtOpations.Issuer
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"])),

    };

});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(

    options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(1);
        options.Cookie.HttpOnly = true; // xss + csrf
        options.Cookie.IsEssential = true;
    }
    );






//«·Ê’Ê· «·„’«œﬁ… 
//builder.Services.AddAuthentication(
//    options =>
//    {
//        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    }
//).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateIssuerSigningKey = true,
//        ValidateLifetime = true,
//        ValidAudience = builder.Configuration["Jwt:Aidoence"],
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]))


//    };
//}); 
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireRoleAdmin", builder=> builder.RequireRole("Admin"));
    options.AddPolicy("RequireRoleUser", builder=> builder.RequireRole("User"));
    options.AddPolicy("RequireRoleSuperisor", builder=> builder.RequireRole("Superisor","Sales"));
    options.AddPolicy("RequireRoleSales", builder=> builder.RequireRole("Sales", "Superisor"));
}
);

builder.Services.AddSignalR();
builder.Services.AddCors(options =>

{
     options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:7035")
       .AllowAnyMethod()
       .AllowAnyHeader()
       .AllowCredentials();
    });
}


);

builder.Services.AddControllers(
    
    //optins=>
    //{
    //    optins.Filters.Add<LogActitvityFilters>();
    //}
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<RateLimitingMiddlewares>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

//app.UseMiddleware<PriofilingMiddleware>(); // «·»Ê«»Â 
app.MapControllers();
app.MapHub<Natifaction>("/datehub");
app.Run();
