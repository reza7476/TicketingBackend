using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

public static class Program
{
    private const string AppSettingPath = "appsettings.json";
    static void Main(string[] args)
    {
        var baseDirectory = Directory.GetCurrentDirectory();
        var connectionString = GetConnectionString(baseDirectory);
        EnsureDatabaseExist(connectionString);

        var runner = CreateRunner(connectionString);
        runner.MigrateUp();
       //runner.MigrateDown(0);

    }

    private static void EnsureDatabaseExist(string connectionString)
    {
        var builder = new NpgsqlConnectionStringBuilder(connectionString);//parse connection string
        var dbName = builder.Database;//get db name

        builder.Database = "postgres";// admin db name in postgres
        var adminConnectionString = builder.ConnectionString;//change connection string to admin db in postgres

        //check exist db by type script
        var checkDbQuery = @"
        SELECT 1
        FROM pg_database
        WHERE datname = @dbName;
        ";

        using var connection = new NpgsqlConnection(adminConnectionString);//create new connection string
        connection.Open();

        using var checkCommand = new NpgsqlCommand(checkDbQuery, connection);//protected from sql injection
        checkCommand.Parameters.AddWithValue("dbName", dbName);

        var exists = checkCommand.ExecuteScalar() != null; //execute query


        //create database if dose not exist
        if (!exists)
        {
            var createDbCommand = $"CREATE DATABASE \"{dbName}\";";
            using var createCommand = new NpgsqlCommand(createDbCommand, connection);
            createCommand.ExecuteNonQuery();
        }
    }
    private static IMigrationRunner CreateRunner(string connectionString)
    {
        var services = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider();

        return services.GetRequiredService<IMigrationRunner>();
    }

    public static string GetConnectionString(string baseDir)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(baseDir)
            .AddJsonFile(AppSettingPath, optional: false, reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();
               
        var connectionString = config["migrationConfig:connectionString"]
           ?? throw new InvalidOperationException("Connection string not found.");

        return connectionString;
    }
}