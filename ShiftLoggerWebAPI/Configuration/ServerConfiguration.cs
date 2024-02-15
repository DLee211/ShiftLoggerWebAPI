namespace ShiftLoggerWebAPI.Configuration;

public class ServerConfiguration
{
    public static string DatabaseConnectionString { get; } = "Server=(localdb)\\MSSQLLocalDB;Database=workershifts.db;Integrated Security=True";

}