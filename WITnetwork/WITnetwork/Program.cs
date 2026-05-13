using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WITnetwork.Data;
using WITnetwork.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NetworkDBContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapPost("/create-user", async (UserManager<User> UserManager) =>
{
   var user = new User
   {
        UserName = "admin_test",
        Email = "admin_test@example.com",
        FirstName = "Admin",
        LastName = "Test",
   };

   var result = await UserManager.CreateAsync(user, "123456");

   if (result.Succeeded)
    {
        return Results.Ok("User created successfully");
    }
    return Results.BadRequest(result.Errors);
});

app.Run();
