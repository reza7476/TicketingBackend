using LawyerExam.Api.Configurations.AutofacSettings;
using LawyerExam.Api.Configurations.ConnectionStrings;
using LawyerExam.Api.Configurations.JWT;
using Microsoft.EntityFrameworkCore;
using Ticketing.Api.BackgroundJobs;
using Ticketing.Api.Configurations.HangfireConfigurations;
using Ticketing.Api.Configurations.SwaggerSettings;
using Ticketing.Infrastructure.Persistence.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);

var connectionString = ConnectionStringConfig.GetProjectConnectionString(
    builder.Environment.EnvironmentName,
    builder.Environment.ContentRootPath);

// Add services to the container.



builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Host.AddAutoFac();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfigurationGen();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.HangFireConfigurationService(builder.Environment);

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI();

app.UseAppHangfire();
app.RegisterHangfireRequiringJobs(builder.Environment);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
