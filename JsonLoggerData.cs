using System;
using System.Collections.Generic;
using System.Text;

namespace Easysave_v1._0_by_prosoft.model
{
    class JsonLoggerData
    {
        public string SourceDir { get; set; }
        public string TargetDir { get; set; }
        public string SaveName { get; set; }
        public string BackupDate { get; set; }
        public string TransactionTime { get; set; }
        public long TotalSize { get; set; }
    }
}
