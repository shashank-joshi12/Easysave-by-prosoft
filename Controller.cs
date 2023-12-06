using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using Easysave_v1._0_by_prosoft.model;
using Easysave_v1._0_by_prosoft.view;

namespace Easysave_v1._0_by_prosoft.controller
{
    class Controller 
    {
        private Model model;
        private View view;
        private int inputMenu;
        public Controller()//constructor for controller
        {
            model = new Model();//invoking model constructor
            view = new View();//invoking default constructor for view
            view.RunStart(); 
            model.UserMenuInput = Menu();
        }
        private string GetSourcePath() //to get source path
        {
            string sourcePath = "";
            bool isValid = false;

            while (!isValid) //Verify is path is valid while talking input
            {
                sourcePath = Console.ReadLine(); //reading user input
                if (Directory.Exists(sourcePath.Replace("\"", ""))) 
                {
                    isValid = true;
                }
                else
                {
                    view.ShowError("Incorect Path"); //show error if path invalid
                }

            }
            return sourcePath;
        }
        private string GetTargetPath() //to get target path
        {
            string targetPath = "";
            bool isValid = false;

            while (!isValid) //Verify is path is valid while talking input
            {
                targetPath = Console.ReadLine(); //reading user input
                if (Directory.Exists(targetPath.Replace("\"", ""))) 
                {
                    isValid = true;
                }
                else
                {
                    view.ShowError("Incorect Path"); //show error if path invalid
                }

            }
            return targetPath;
        }
        private string Menu() //function for menu management
        {
            Stopwatch stopwatch = new Stopwatch();
            bool menu = true;
            while (menu) //Loop for menu
            {
                model.CheckNoOfBackups(); // Calling the function to check the number of backups
                try
                {
                    view.RunMenu(); //Calling the function to display the menu
                    inputMenu = int.Parse(Console.ReadLine()); //Retrieving user input for menu
                    switch (inputMenu) // Switch of menu
                    {
                        case 0:
                            Environment.Exit(0); //Stop the programs
                            break;
                        case 1:
                            view.GetName(); //Display message introduction on the backup names

                            string jsonString = File.ReadAllText(model.backupJobsFile); //Function to read json file
                            BackupJob[] list = JsonConvert.DeserializeObject<BackupJob[]>(jsonString); // Function to deserialize the json file into a list 

                            foreach (var obj in list) //Loop to display the names of the backups
                            {
                                Console.WriteLine(" - " + obj.SaveName); //Display of backup names
                            }
                            view.ShowFile();//Calling the function to display the names of the backups
                            string inputnamebackup = Console.ReadLine(); // Recovering backup names
                            model.LoadSave(inputnamebackup); // Calling the function to start the backup
                            break;

                        case 2:
                            if (model.noOfBackups < 5) // Check not to exceed the save limit
                            {
                                Console.Clear(); //Console cleaning
                                view.RunSubMenu(); // Calling the function to display the second menu
                                MenuSub(); // Calling the function for the second menu
                            }
                            else
                            {
                                Console.Clear(); //Console cleaning
                                if (view.languagePreference == "E")
                                {

                                }
                                view.ShowError("Maximum number of backup jobs reached"); // Show Error Message
                            }

                            break;
                    }

                }
                catch
                {
                    Console.Clear();//Console cleaning
                }

            }

            return "";
        }
        private void MenuSub() //Function for the menu when creating backup jobs.
        {
            bool menusub = true;
            while (menusub) //Loop for menu
            {
                try
                {
                    int inputMenuSub = int.Parse(Console.ReadLine()); //Retrieving user input for second menu
                    switch (inputMenuSub) // Switch of menu
                    {
                        case 0:
                            Console.Clear();//Console cleaning
                            Menu(); //Calling up the menu function
                            break;
                        case 1: //Case 1, creating a full backup job
                            model.Type = 1; //Type declaration for backup
                            view.GetName(); //Display for backup name
                            model.SaveName = Console.ReadLine(); // Retrieving the name of the backup
                            view.GetSourcePath(); // Display for folder source
                            model.SourcePath = GetSourcePath(); // Function for checking the folder path
                            view.GetTargetPath(); // Display for the folder destination
                            model.TargetPath= GetTargetPath();  // Function for checking the folder path
                            BackupJob backup = new BackupJob(model.SaveName, model.SourcePath, model.TargetPath, model.Type);
                            model.AddSave(backup); // Calling the function to add a backup job
                            break;

                        case 2: //Case 2, creating a differential backup job
                            model.Type = 2; //Type declaration for backup
                            view.GetName();
                            model.SaveName = Console.ReadLine();
                            view.GetSourcePath();
                            model.SourcePath= GetSourcePath();
                            view.GetTargetPath();
                            model.TargetPath= GetTargetPath();
                            BackupJob backup2 = new BackupJob(model.SaveName, model.SourcePath, model.TargetPath, model.Type);
                            model.AddSave(backup2); // Calling the function to add a backup job
                            break;
                    }

                }
                catch
                {
                    Console.Clear();
                    Menu(); //Calling up the menu function
                }

            }

        }

    }
}
