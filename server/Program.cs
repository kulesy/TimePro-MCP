using TimePro.Server.Services;
using TimePro.Server.MCP;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "TimePro API", 
        Version = "v1",
        Description = "Timesheet management API with MCP support"
    });
});

// Register custom services
builder.Services.AddSingleton<ITimesheetService, TimesheetService>();
builder.Services.AddSingleton<McpServer>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TimePro API v1");
        c.RoutePrefix = "swagger";
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowReactApp");

// Skip authorization for Swagger and API endpoints in development
if (!app.Environment.IsDevelopment())
{
    app.UseAuthorization();
}

app.MapControllers();

app.Run();


