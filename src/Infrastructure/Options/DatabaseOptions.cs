namespace Infrastructure.Options;

public class DatabaseOptions
{
    public string ConnectionString { get; set; } = string.Empty;

    public bool EnabledDetailedErrors { get; set; }

    public bool EnabledSensitiveDataLogging { get; set; }

    public int MaxRetryCount { get; set; }

    public int CommandTimeOut { get; set; }
}