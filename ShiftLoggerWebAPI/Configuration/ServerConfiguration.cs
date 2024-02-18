namespace ShiftLoggerWebAPI.Configuration;

public class ServerConfiguration
{
    private static string password = "";
    
    public static string
        DatabaseConnectionString
    {
        get;
    } = //"Server=(localdb)\\MSSQLLocalDB;Database=workershifts.db;Integrated Security=True";//Windows
        $"Server=localhost,1433;Database=workershifts;User Id=SA;Password={password};TrustServerCertificate=True";//Mac

}