using LawyerExam.Api.Configurations.AutofacSettings;
using LawyerExam.Api.Configurations.ConnectionStrings;
using LawyerExam.Api.Configurations.JWT;
using Ticketing.Api.Configurations.SwaggerSettings;

var builder = WebApplication.CreateBuilder(args);

var connectionString = ConnectionStringConfig.GetProjectConnectionString(
    builder.Environment.EnvironmentName,
    builder.Environment.ContentRootPath);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Host.AddAutoFac();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfigurationGen();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
