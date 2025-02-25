var builder = WebApplication.CreateBuilder(args);

// Allow CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

builder.Services.AddSignalR();
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseRouting();

app.MapHub<NotificationHub>("/notificationHub");
app.MapControllers();

app.Run();