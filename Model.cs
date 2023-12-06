﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Diagnostics;

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
        public string SourcePath { get; set; }
        public int nbfilesmax { get; set; }
        public int nbfiles { get; set; }
        public long size { get; set; }
        public float progs { get; set; }
        public string TargetPath { get; set; }
        public string SaveName { get; set; }
        public int Type { get; set; }
        public string SourceFile { get; set; }
        public string TypeString { get; set; }
        public long TotalSize { get; set; }
        public TimeSpan TimeTransfert { get; set; }
        public string UserMenuInput { get; set; }
        public string MirrorDir { get; set; }


        public Model()
        {
            UserMenuInput = " ";

            if (!Directory.Exists(backupListFile)) //checking if directory already exists
            {
                DirectoryInfo Dir = Directory.CreateDirectory(backupListFile); //if directory doesn't exist then create new directory
            }
            backupListFile += @"backupList.json"; //create or append to JSON log

            if (!Directory.Exists(stateFile))//checking is directory already exists
            {
                DirectoryInfo Dir = Directory.CreateDirectory(stateFile); //if not create a new directory
            }
            stateFile += @"state.json"; //create or append to JSON log


        }
        public void FullBackup(string srcPath, string tgtPath, bool copyDir, bool verif) //Function for full backup (CompleteSave)
        {
            DataState = new DataState(NameStateFile);
            this.DataState.SaveState = true;//telling that the state of this backup is saved
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); //Starting the timer for the log file

            DirectoryInfo dir = new DirectoryInfo(srcPath);  // Get the subdirectories for the specified directory.

            if (!dir.Exists) //Check if the file is present
            {
                throw new DirectoryNotFoundException("Directory Not Found at source path!" + srcPath);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            Directory.CreateDirectory(tgtPath); // if there's no destination folder, create it 

            FileInfo[] files = dir.GetFiles(); // Get the files in the directory 

            if (!verif) //  to reset variable if new backup
            {
                TotalSize = 0;
                nbfilesmax = 0;
                size = 0;
                nbfiles = 0;
                progs = 0;

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

                if (size > 0)
                {
                    progs = ((float)size / TotalSize) * 100;
                }

                //Systems which allows to insert the values ​​of each file in the report file.
                DataState.SourceFile = Path.Combine(srcPath, file.Name);
                DataState.TargetFile = tempPath;
                DataState.TotalSize = nbfilesmax;
                DataState.TotalFile = TotalSize;
                DataState.TotalSizeRest = TotalSize - size;
                DataState.FileRest = nbfilesmax - nbfiles;
                DataState.Progress = progs;

                UpdateStatefile(); //call to update or start the file status system

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
            DataState.TotalSize = TotalSize;
            DataState.SourceFile = null;
            DataState.TargetFile = null;
            DataState.TotalFile = 0;
            DataState.TotalSize = 0;
            DataState.TotalSizeRest = 0;
            DataState.FileRest = 0;
            DataState.Progress = 0;
            DataState.SaveState = false;

            UpdateStatefile(); //call to update or start the file status system

            stopwatch.Stop(); //Stop the stopwatch
            this.TimeTransfert = stopwatch.Elapsed;
        }
        public void DifferentialBackup(string srcPath, string tgtPath, string tgtPathM) // Function that allows you to make a differential backup
        {
            DataState = new DataState(NameStateFile); // instantiating an object for class describing the state of backup
            Stopwatch stopwatch = new Stopwatch(); // Instattation of the method
            stopwatch.Start(); //Starting the stopwatch

            DataState.SaveState = true;
            TotalSize = 0;
            nbfilesmax = 0;

            System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(srcPath);
            System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(tgtPath);

            // Get info of both directories  
            IEnumerable<System.IO.FileInfo> list1 = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
            IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            //A custom file comparer defined below  
            FileCompare myFileCompare = new FileCompare();

            var queryList1Only = (from file in list1 select file).Except(list2, myFileCompare);
            size = 0;
            nbfiles = 0;
            progs = 0;

            foreach (var v in queryList1Only)
            {
                TotalSize += v.Length;
                nbfilesmax++;

            }

            //Loop that allows the backup of different files
            foreach (var v in queryList1Only)
            {
                string tempPath = Path.Combine(tgtPathM, v.Name);
                //Systems which allows to insert the values ​​of each file in the report file.
                DataState.SourceFile = Path.Combine(srcPath, v.Name);
                DataState.TargetFile = tempPath;
                DataState.TotalSize = nbfilesmax;
                DataState.TotalFile = TotalSize;
                DataState.TotalSizeRest = TotalSize - size;
                DataState.FileRest = nbfilesmax - nbfiles;
                DataState.Progress = progs;

                UpdateStatefile();//call to update or start the file status system
                v.CopyTo(tempPath, true); //Function that allows you to copy the file to its new folder.
                size += v.Length;
                nbfiles++;
            }

            //to reset backup status after the end of backup
            DataState.SourceFile = null;
            DataState.TargetFile = null;
            DataState.TotalFile = 0;
            DataState.TotalSize = 0;
            DataState.TotalSizeRest = 0;
            DataState.FileRest = 0;
            DataState.Progress = 0;
            DataState.SaveState = false;
            UpdateStatefile();//call to update or start the file status system

            stopwatch.Stop(); //Stop the stopwatch
            this.TimeTransfert = stopwatch.Elapsed; // Transfer of the chrono time to the variable
        }


    }
}