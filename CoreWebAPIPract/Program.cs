using CoreWebAPIPract.DI;
using CoreWebAPIPract.Fileters;
using CoreWebAPIPract.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();   
builder.Services.AddScoped<LogActionFilter>();
builder.Services.AddScoped<INotificationService, EmailService>();
builder.Services.AddScoped<OrderService>();
var app = builder.Build();

//Minimal API
app.MapGet("/ping", () => "pong");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();

//Inline middleware
app.Use(async (context, next) =>
{
    if (context.User.Claims.Any())
        Console.WriteLine("before next");
    await next();
    Console.WriteLine("after next");
});
app.UseMiddleware<LoggingMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.Run();
