namespace OperationTable;

public static class Logger
{
    private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt");
    public static void Log(string Message)
    {
        using (var streamWriter = new StreamWriter(LogFilePath, true))
        {
            streamWriter.WriteLine($"[{DateTime.Now}] {Message}");
        }
    }
}
