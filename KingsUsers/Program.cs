using System.Net;
using System.Text.Json;
using KingsUsers.Controllers;
using KingsUsers.Data;
using KingsUsers.Interfaces;
using KingsUsers.Middleware;
using KingsUsers.Models;
using KingsUsers.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


[assembly: ApiConventionType(typeof(ApiConventions))]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add configuration for the database context
builder.Services.AddDbContext<KingsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Kings-Users",
        Version = "v1",
        Description = "API documentation for Kings-Users",
        Contact = new OpenApiContact
        {
            Name = "Gerald Greyvenstein",
            Url = new Uri("https://bold.pro/my/gerald-greyvenstein")
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:7223")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddScoped<IUserService, UserService>();
var app = builder.Build();

// Seed the database

// Register the database context as a service
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<KingsDbContext>();

    // Apply any pending migrations
    dbContext.Database.Migrate();

    // Seed the database
    if (!dbContext.Users.Any())
      SeedData(dbContext);
}

app.UseExceptionHandler(applicationBuilder =>
{
    applicationBuilder.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerFeature>();
        if (exception != null)
        {
            var errorResponse = new ErrorResponse
            {
                Message = "An error occurred.",
                Error = exception.Error.Message
            };

            var json = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(json);
        }
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"));
    app.UseReDoc(configure => configure.RoutePrefix = "/redoc");
}

app.UseExceptionHandler("/error");
app.UseCors();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();
//app.MapEndpoints();

//app.MapGet("/", () => "Hello, my kings!");

app.Run();


static void SeedData(KingsDbContext dbContext)
{
    // Create groups
    var kingsGroup = new Group { GroupName = "Kings" };
    var normalUsersGroup = new Group { GroupName = "Normal Users" };
    dbContext.Groups.AddRange(kingsGroup, normalUsersGroup);

    // Create permissions
    var adminPermission = new Permission { PermissionName = "Admin" };
    var basicPermission = new Permission { PermissionName = "Basic" };
    dbContext.Permissions.AddRange(adminPermission, basicPermission);

    // Create group permissions
    var kingsGroupPermission = new GroupPermission { Group = kingsGroup, Permission = adminPermission };
    var normalUsersGroupPermission = new GroupPermission { Group = normalUsersGroup, Permission = basicPermission };
    dbContext.GroupPermissions.AddRange(kingsGroupPermission, normalUsersGroupPermission);

    // Create kings
    var kings = new List<User>
    {
        new()
        {
            Username = "king1", FirstName = "Arthur", LastName = "Pendragon", Address = "Camelot",
            About = "Legendary King of the Britons"
        },
        new()
        {
            Username = "king2", FirstName = "Richard", LastName = "the Lionheart", Address = "England",
            About = "King of England and Crusader"
        },
        new()
        {
            Username = "king3", FirstName = "Charlemagne", LastName = "", Address = "Frankish Kingdom",
            About = "Founder of the Holy Roman Empire"
        }
    };
    foreach (var king in kings)
    {
        var userGroup = new UserGroup { User = king, Group = kingsGroup };
        king.UserGroups = new List<UserGroup> { userGroup };
    }

    dbContext.Users.AddRange(kings);

    // Create normal users
    var normalUsers = new List<User>
    {
        new() { Username = "user1", FirstName = "John", LastName = "Doe", Address = "USA", About = "Regular User" },
        new()
        {
            Username = "user2", FirstName = "Jane", LastName = "Smith", Address = "Canada", About = "Regular User"
        }
    };
    foreach (var user in normalUsers)
    {
        var userGroup = new UserGroup { User = user, Group = normalUsersGroup };
        user.UserGroups = new List<UserGroup> { userGroup };
    }

    dbContext.Users.AddRange(normalUsers);

    dbContext.SaveChanges();
}