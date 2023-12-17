using System;
using System.Collections.Generic;
using System.Text;

namespace Easysave_v1._0_by_prosoft.model
{
    class StatusData
    {
        //Properties declaration for all variable of json status file
        public string SaveName { get; set; }
        public string BackupDate { get; set; }
        public bool SaveState { get; set; }
        public string SourceFile { get; set; }
        public string TargetFile { get; set; }
        public float TotalFile { get; set; }
        public long TotalSize { get; set; }
        public StatusData(string saveName)
        {
            SaveName = saveName;
        }
    }
}
