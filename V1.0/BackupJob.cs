using System;
using System.Collections.Generic;
using System.Text;

namespace Easysave_v1._0_by_prosoft.model
{
    class BackupJob
    {
        // Declaration of properties that are used for saving backup information for the backup job file
        public string SourceDir { get; set; }
        public string TargetDir { get; set; }
        public string SaveName { get; set; }
        public int Type { get; set; }
        

        public BackupJob(string saveName, string sourceDir, string targetDir, int type)
        {
            SaveName = saveName;
            SourceDir = sourceDir;
            TargetDir = targetDir;
            Type = type;
            
        }
    }
}
