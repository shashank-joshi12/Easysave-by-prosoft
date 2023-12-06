using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Diagnostics;

namespace Easysave_v1._0_by_prosoft.model
{
    class Model
    {
        public int noOfBackups;
        private string serializeObj;
        public string backupJobsFile = System.Environment.CurrentDirectory + @"\BackupJobs\";//go to current directory add append the next path
        public string backupStatusFile = System.Environment.CurrentDirectory + @"\BackupStatus\";
        public StatusData statusData { get; set; }
        public string StatusFile { get; set; }
        public string BackupNameState { get; set; }
        public string SourcePath { get; set; }
        public int nbfilesmax { get; set; }
        public int nbfiles { get; set; }
        public long size { get; set; }       
        public string TargetPath { get; set; }
        public string SaveName { get; set; }
        public int Type { get; set; }
        public string SourceFile { get; set; }
        public string TypeString { get; set; }
        public long TotalSize { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public string UserMenuInput { get; set; }
        public string MirrorDir { get; set; }


        public Model()
        {
            UserMenuInput = " ";

            if (Directory.Exists(backupJobsFile) != true) //checking if directory for backup jobs already exists
            {
                DirectoryInfo Dir = Directory.CreateDirectory(backupJobsFile); //if directory doesn't exist then create new directory
            }
            backupJobsFile += @"backupJobs.json"; //append to logfile

            if (Directory.Exists(backupStatusFile) != true)//checking is directory for backup status already exists
            {
                DirectoryInfo Dir = Directory.CreateDirectory(backupStatusFile); //if directory doesn't exist then create new directory
            }
            backupStatusFile += @"backupStatus.json"; //append to logfile 


        }
        public void FullBackup(string srcPath, string tgtPath, bool copyDir, bool verif) //Function for full backup (CompleteSave)
        {
            statusData = new StatusData(StatusFile);
            this.statusData.SaveState = true;//telling that the state of this backup is saved
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); //Starting the timer for the log file

            DirectoryInfo dir = new DirectoryInfo(srcPath);  // Get the subdirectories for the specified directory.

            if (!dir.Exists) //Check if the file is present
            {
                throw new DirectoryNotFoundException("Directory Not Found at source path!" + srcPath);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();//getting subdirectories
            Directory.CreateDirectory(tgtPath); // if there's no destination folder, create it 

            FileInfo[] files = dir.GetFiles(); // Get the files in the directory 

            if (!verif) //  to reset variable if new backup
            {
                TotalSize = 0;
                nbfilesmax = 0;
                size = 0;
                nbfiles = 0;
                

                foreach (FileInfo file in files) // to calculate size of files and folder
                {
                    TotalSize += file.Length; //adding size of each file to a variable 
                    nbfilesmax++;
                }
                foreach (DirectoryInfo subdir in dirs) // to calculate size of subfiles and subfolder 
                {
                    FileInfo[] Maxfiles = subdir.GetFiles();// getting all files in subdirectory
                    foreach (FileInfo file in Maxfiles)
                    {
                        TotalSize += file.Length;
                        nbfilesmax++;
                    }
                }

            }
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(tgtPath, file.Name);

                

                //Systems which allows to insert the values ​​of each file in the report file.
                statusData.SourceFile = Path.Combine(srcPath, file.Name);
                statusData.TargetFile = tempPath;
                statusData.TotalSize = nbfilesmax;
                statusData.TotalFile = TotalSize;
                
                UpdateBackupStatus(); //call to update or start the file status system

                file.CopyTo(tempPath, true); //Function that allows you to copy the file to its new folder.
                nbfiles++;
                size += file.Length;

            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copyDir)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(tgtPath, subdir.Name);
                    FullBackup(subdir.FullName, tempPath, copyDir, true);
                }
            }
            //reset backup status at the end of backup
            statusData.TotalSize = TotalSize;
            statusData.SourceFile = null;
            statusData.TargetFile = null;
            statusData.TotalFile = 0;
            statusData.TotalSize = 0;
            
            statusData.SaveState = false;

            UpdateBackupStatus(); //call to update or start the file status system

            stopwatch.Stop(); //Stop the stopwatch
            this.TimeTaken = stopwatch.Elapsed;
        }
        public void DifferentialBackup(string srcPath, string tgtPath, string tgtPathM) // Function that allows you to make a differential backup
        {
            statusData = new StatusData(StatusFile); // instantiating an object for class describing the state of backup
            Stopwatch stopwatch = new Stopwatch(); 
            stopwatch.Start(); //Starting the stopwatch

            statusData.SaveState = true;
            TotalSize = 0;
            nbfilesmax = 0;

            DirectoryInfo dir1 = new System.IO.DirectoryInfo(srcPath);
            DirectoryInfo dir2 = new System.IO.DirectoryInfo(tgtPath);

            // Get info of both directories  
            IEnumerable<System.IO.FileInfo> list1 = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
            IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            //invoking constructor of defined comparator
            FileComparison _2fileCompare = new FileComparison();

            var List1Only = (from file in list1 select file).Except(list2, _2fileCompare);
            size = 0;
            nbfiles = 0;
           

            foreach (var v in List1Only)
            {
                TotalSize += v.Length;
                nbfilesmax++;

            }

            //Loop that allows the backup of different files
            foreach (var v in List1Only)
            {
                string tempPath = Path.Combine(tgtPathM, v.Name);
                //Systems which allows to insert the values ​​of each file in the report file.
                statusData.SourceFile = Path.Combine(srcPath, v.Name);
                statusData.TargetFile = tempPath;
                statusData.TotalSize = nbfilesmax;
                statusData.TotalFile = TotalSize;
                

                UpdateBackupStatus();//call to update or start the file status system
                v.CopyTo(tempPath, true); //Function that allows you to copy the file to its new folder.
                size += v.Length;
                nbfiles++;
            }

            //to reset backup status after the end of backup
            statusData.SourceFile = null;
            statusData.TargetFile = null;
            statusData.TotalFile = 0;
            statusData.TotalSize = 0;
            
            statusData.SaveState = false;
            UpdateBackupStatus();//call to update or start the file status system

            stopwatch.Stop(); //Stop the stopwatch
            this.TimeTaken = stopwatch.Elapsed; // Transfer of the chrono time to the variable
        }
        private void UpdateBackupStatus()//Function that updates the status json file.
        {
            List<StatusData> stateList = new List<StatusData>();//creating list of type DataState
            this.serializeObj = null;
            if (!File.Exists(backupStatusFile)) //Checking if the file exists
            {
                File.Create(backupStatusFile).Close();
            }

            string jsonString = File.ReadAllText(backupStatusFile);  //Reading the json file

            if (jsonString.Length != 0) //Checking the contents of the json file is empty or not
            {
                StatusData[] list = JsonConvert.DeserializeObject<StatusData[]>(jsonString); //Dserialization of the json file

                foreach (var obj in list) // Loop to allow filling of the JSON file
                {
                    if (obj.SaveName == this.StatusFile) // comparing names to verify current and existing backup
                    {
                        obj.SourceFile = this.statusData.SourceFile;
                        obj.TargetFile = this.statusData.TargetFile;
                        obj.TotalFile = this.statusData.TotalFile;
                        obj.TotalSize = this.statusData.TotalSize;
                        obj.BackupDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        obj.SaveState = this.statusData.SaveState;
                    }

                    stateList.Add(obj); //adds the object to the list of type DataState

                }

                this.serializeObj = JsonConvert.SerializeObject(stateList.ToArray(), Formatting.Indented) + Environment.NewLine;

                File.WriteAllText(backupStatusFile, this.serializeObj);//writing into the json file in json format
            }

        }
        public void UpdateLogFile(string savename, string sourcedir, string targetdir)//Function to allow modification of the log file
        {
            Stopwatch stopwatch = new Stopwatch();
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", TimeTaken.Hours, TimeTaken.Minutes, TimeTaken.Seconds, TimeTaken.Milliseconds / 10); //for display

            JsonLoggerData logdata = new JsonLoggerData //Apply the retrieved values ​​to their classes
            {
                SaveName = savename,
                SourceDir = sourcedir,
                TargetDir = targetdir,
                BackupDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                TotalSize = TotalSize,
                TransactionTime = elapsedTime
            };

            string path = System.Environment.CurrentDirectory; 
            var directory = System.IO.Path.GetDirectoryName(path); 

            string serializeObj = JsonConvert.SerializeObject(logdata, Formatting.Indented) + Environment.NewLine; //Serialization for writing to json file
            File.AppendAllText(directory + @"DailyLogs_" + DateTime.Now.ToString("dd-MM-yyyy") + ".json", serializeObj); //Function to write to log file

            stopwatch.Reset(); //resetting the stopwatch
        }



        public void AddSave(BackupJob backup) //Function that creates a backup job
        {
            List<BackupJob> backupList = new List<BackupJob>();
            this.serializeObj = null;

            if (!File.Exists(backupJobsFile)) //Checking if the file exists
            {
                File.WriteAllText(backupJobsFile, this.serializeObj);
            }

            string jsonString = File.ReadAllText(backupJobsFile); //Reading the json file

            if (jsonString.Length != 0) //Checking the contents of the json file is empty or not
            {
                BackupJob[] list = JsonConvert.DeserializeObject<BackupJob[]>(jsonString); //Derialization of the json file
                foreach (var obj in list) //Loop to add the information in the json
                {
                    backupList.Add(obj);
                }
            }
            backupList.Add(backup); //Allows you to prepare the objects for the json filling

            this.serializeObj = JsonConvert.SerializeObject(backupList.ToArray(), Formatting.Indented) + Environment.NewLine; //Serialization for writing to json file
            File.WriteAllText(backupJobsFile, this.serializeObj); // Writing to the json file

            statusData = new StatusData(this.SaveName); //Class initiation

            statusData.BackupDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); //Adding the time in the variable
            AddState(); //Call of the function to add the backup in the report file.
        }
        public void AddState() //Function that allows you to add a backup job to the report file.
        {
            List<StatusData> stateList = new List<StatusData>();
            this.serializeObj = null;

            if (!File.Exists(backupStatusFile)) //Checking if the file exists
            {
                File.Create(backupStatusFile).Close();
            }

            string jsonString = File.ReadAllText(backupStatusFile); //Reading the json file

            if (jsonString.Length != 0)
            {
                StatusData[] list = JsonConvert.DeserializeObject<StatusData[]>(jsonString); //Derialization of the json file
                foreach (var obj in list) //Loop to add the information in the json
                {
                    stateList.Add(obj);
                }
            }
            this.statusData.SaveState = false;
            stateList.Add(this.statusData); //Allows you to prepare the objects for the json filling

            this.serializeObj = JsonConvert.SerializeObject(stateList.ToArray(), Formatting.Indented) + Environment.NewLine; //Serialization for writing to json file
            File.WriteAllText(backupStatusFile, this.serializeObj);// Writing to the json file
        }
        public void LoadSave(string backupname) //Function that allows you to load backup jobs
        {
            BackupJob backupjob = null;
            this.TotalSize = 0;
            BackupNameState = backupname;

            string jsonString = File.ReadAllText(backupJobsFile); //Reading the json file


            if (jsonString.Length != 0) //Checking the contents of the json file is empty or not
            {
                BackupJob[] list = JsonConvert.DeserializeObject<BackupJob[]>(jsonString);  //Deserialization of the json file into a list
                foreach (var obj in list)
                {
                    if (obj.SaveName == backupname) //Check to have the correct name of the backup
                    {
                        backupjob = new BackupJob(obj.SaveName, obj.SourceDir, obj.TargetDir, obj.Type); //Function that allows you to retrieve information about the backup
                    }
                }
            }

            if (backupjob.Type == 1) //If the type is 1, it means it's a full backup
            {
                StatusFile = backupjob.SaveName;
                FullBackup(backupjob.SourceDir, backupjob.TargetDir, true, false); //function call for full backup
                UpdateLogFile(backupjob.SaveName, backupjob.SourceDir, backupjob.TargetDir); //function call to update json log file
                Console.WriteLine("Saved Successfull !"); //to display if backup successful
            }
            else //if it's not a full backup it's a differential backup
            {
                StatusFile = backupjob.SaveName;
                DifferentialBackup(backupjob.SourceDir, backupjob.TargetDir, backupjob.TargetDir); //function call for differential backup
                UpdateLogFile(backupjob.SaveName, backupjob.SourceDir, backupjob.TargetDir); //function call to update json log file
                Console.WriteLine("Saved Successfull !"); // to display if backup successful
            }

        }
        public void CheckNoOfBackups()  // Function that allows to count the number of backups in the json file of backup jobs
        {
            noOfBackups = 0;

            if (File.Exists(backupJobsFile)) //Check if file exists
            {
                string jsonString = File.ReadAllText(backupJobsFile);//Reading all content of the file in path
                if (jsonString.Length != 0)//Checking if the retrieved data of the json file is empty or not
                {
                    BackupJob[] list = JsonConvert.DeserializeObject<BackupJob[]>(jsonString); //Deserialization of data of json into list
                    noOfBackups = list.Length; //because jobs are stored in the log no of objects in the list = no of backup jobs
                }
            }
        }



    }
}