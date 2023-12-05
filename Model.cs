using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Easysave_v1._0_by_prosoft.model
{
    class Model
    {
        public int checkdatabackup;
        private string serializeObj;
        public string backupListFile = System.Environment.CurrentDirectory + @"\Works\";
        public string stateFile = System.Environment.CurrentDirectory + @"\State\";
        public DataState DataState { get; set; }
        public string NameStateFile { get; set; }
        public string BackupNameState { get; set; }
        public string SourceDir { get; set; }
        public int nbfilesmax { get; set; }
        public int nbfiles { get; set; }
        public long size { get; set; }
        public float progs { get; set; }
        public string TargetDir { get; set; }
        public string SaveName { get; set; }
        public int Type { get; set; }
        public string SourceFile { get; set; }
        public string TypeString { get; set; }
        public long TotalSize { get; set; }
        public TimeSpan TimeTransfert { get; set; }
        public string userMenuInput { get; set; }
        public string MirrorDir { get; set; }
                
        public void RunBackupJob(string source, string target, bool type)
        {
            var files = Directory.GetFiles(source);
            var directories = Directory.GetDirectories(source);

            
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file); 
                string destFile = Path.Combine(target, fileName); 
                File.Copy(file, destFile, type); 
            }
                   

            foreach (var directory in directories)
            {
                string dirName = Path.GetFileName(directory); 
                string destDir = Path.Combine(target, dirName); 
                RunBackupJob(directory, destDir, type); 
            }


        }
        public void DelExistingBackup()
        {

        }
        
        
        
        
    }
}
