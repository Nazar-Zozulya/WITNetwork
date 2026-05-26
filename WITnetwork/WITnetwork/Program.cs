using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using WITnetwork.Data;
using WITnetwork.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization", 
        Type = SecuritySchemeType.Http, 
        Scheme = "bearer",
        BearerFormat = "JWT", 
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        { 
            new OpenApiSecuritySchemeReference("Bearer"), 
            new List<string>() 
        }
    });
});






builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, ValidIssuer = "AuthServer",
        ValidateAudience = true, ValidAudience = "BackendApi",
        ValidateLifetime = true, ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("12345789012345789012345789012345"))
    });

builder.Services.AddDbContext<NetworkDBContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<UserProfile, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
})
    .AddEntityFrameworkStores<NetworkDBContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseAuthorization();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

// app.MapPost("/create-user", async (UserManager<UserProfile> UserManager) =>
// {
//    var user = new UserProfile
//    {
//         UserName = "admin_test",
//         Email = "admin_test@example.com",
//         FirstName = "Admin",
//         LastName = "Test",
//    };

//    var result = await UserManager.CreateAsync(user, "123456");

//    if (result.Succeeded)
//     {
//         return Results.Ok("User created successfully");
//     }
//     return Results.BadRequest(result.Errors);
// });

app.Run();
