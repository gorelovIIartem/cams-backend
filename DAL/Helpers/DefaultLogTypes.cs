using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Helpers
{
    public static class DefaultLogTypes
    {
        public static void SeedLogTypes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogType>()
                .HasData(
                new LogType
                {
                    LogTypeId = (int)Enums.LogType.Critical,
                    LogName = Enums.LogType.Critical.ToString(),
                    Description = "Logs that describe an unrecoverable application or system crash, or a catastrophic failure that requires immediate attention."
                },
                new LogType
                {
                    LogTypeId = (int)Enums.LogType.Debug,
                    LogName = Enums.LogType.Debug.ToString(), 
                    Description = "Logs that are used for interactive investigation during development. These logs should primarily contain information useful for debugging and have no long-term value."
                },
                new LogType
                {
                    LogTypeId = (int)Enums.LogType.Error,
                    LogName = Enums.LogType.Error.ToString(),
                    Description = "Logs that highlight when the current flow of execution is stopped due to a failure.These should indicate a failure in the current activity, not an application - wide failure."
                },
                new LogType
                {
                    LogTypeId = (int)Enums.LogType.Information,
                    LogName = Enums.LogType.Information.ToString(),
                    Description = "Logs that track the general flow of the application.These logs should have long - term value."
                },
                new LogType
                {
                    LogTypeId = (int)Enums.LogType.None,
                    LogName = Enums.LogType.None.ToString(),
                    Description = "Not used for writing log messages.Specifies that a logging category should not write any messages."
                },
                new LogType
                {
                    LogTypeId = (int)Enums.LogType.Trace,
                    LogName = Enums.LogType.Trace.ToString(),
                    Description = "Logs that contain the most detailed messages. These messages may contain sensitive application data. These messages are disabled by default and should never be enabled in a production environment."
                },
                new LogType
                {
                    LogTypeId = (int)Enums.LogType.Warning,
                    LogName = Enums.LogType.Warning.ToString(),
                    Description = "Logs that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the application execution to stop."
                }
                );
        }
    }
}
