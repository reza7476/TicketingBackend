namespace LawyerExam.Api.Configurations.ConnectionStrings;

public class ConnectionStringConfig
{
    public static string GetProjectConnectionString(string environmentName, string contentRootPath)
    {

        var builder = new ConfigurationBuilder()
            .SetBasePath(contentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .AddEnvironmentVariables();

        var config = builder.Build();


        var connectionString = config.GetConnectionString("DefaultConnection");

        return connectionString ?? string.Empty;
    }
    public static (IConfiguration configuration, string connectionString)
      LoadConfigAndConnectionStringForHangFire(string environmentName, string contentRootPath)
    {
        var builder = new ConfigurationBuilder()
              .SetBasePath(contentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
              .AddEnvironmentVariables();
        var configuration = builder.Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        return (configuration, connectionString);
    }
}
