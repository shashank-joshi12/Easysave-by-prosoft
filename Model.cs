using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Easysave_v1._0_by_prosoft
{
    class Model
    {
        private string jobName;
        private string jobSourcePath;
        private string jobTargetPath;
        private bool jobType;

        public string JobName
        {
            get { return jobName; }
            set { jobName = value; }
        }
        public string JobSourcePath
        {
            get { return jobSourcePath; }
            set { jobSourcePath = value; }
        }
        public string JobTargetPath
        {
            get { return jobTargetPath; }
            set { jobTargetPath = value; }
        }
        public bool JobType
        {
            get { return jobType; }
            set { jobType = value; }
        }

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
