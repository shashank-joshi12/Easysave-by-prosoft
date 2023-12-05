using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Easysave_v1._0_by_prosoft
{
    public class JsonLogger
    {
        private const string logFilePath = @"\\server\share\path\to\logs\daily_log.json";

        public static void LogBackupAction(BackupLogEntry logEntry)
        {
            List<BackupLogEntry> logEntries = LoadLogEntries();
            logEntries.Add(logEntry);

            // Serialize to JSON with formatting for readability
            string jsonLog = JsonConvert.SerializeObject(logEntries, Formatting.Indented);

            // Write to the log file
            File.WriteAllText(logFilePath, jsonLog);
        }

        private static List<BackupLogEntry> LoadLogEntries()
        {
            if (File.Exists(logFilePath))
            {
                string jsonLog = File.ReadAllText(logFilePath);
                return JsonConvert.DeserializeObject<List<BackupLogEntry>>(jsonLog) ?? new List<BackupLogEntry>();
            }
            else
            {
                return new List<BackupLogEntry>();
            }
        }

    }
}
