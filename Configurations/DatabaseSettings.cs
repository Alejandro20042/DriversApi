namespace Drivers.Api.Configurations;

public class DatabaseSettings: IDatabaseSetting
{
    public string ConnectionString {get; set; } = String.Empty;
    public string DatabaseName {get; set; } = String.Empty;
    public Dictionary<string, string> Collections {get; set;}

}

public interface IDatabaseSetting{
    string ConnectionString {get; set;}
    string DatabaseName{get; set;}
    Dictionary<string, string > Collections {get; set;}

}

