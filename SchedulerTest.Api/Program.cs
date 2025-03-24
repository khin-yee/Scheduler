using Microsoft.EntityFrameworkCore;
using SchedulerTest.Repository;
using Hangfire;
using Hangfire.PostgreSql;
using SchedulerTest.Service;
using SchedulerTest.Service.Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(x =>
{
    x.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("connectionstring"));
});

// Add Hangfire server
builder.Services.AddHangfireServer();

// Configure Entity Framework Core with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("connectionstring"))
);

// Register application services
builder.Services.AddService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.StartRecurringJob(builder.Configuration);

app.UseHangfireDashboard();

app.MapControllers();
app.Run();
