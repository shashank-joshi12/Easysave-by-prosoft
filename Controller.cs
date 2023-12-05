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
        public Controller()
        {
            model = new Model();
            view = new View();
            view.RunStart(); 
            model.userMenuInput = Menu();
        }
        private string GetSourcePath() //Function to retrieve the input from the source
        {
            string sourcePath = "";
            bool isValid = false;

            while (!isValid) //Loop to allow verification of the path
            {
                sourcePath = Console.ReadLine(); //Retrieving user input
                if (Directory.Exists(sourcePath.Replace("\"", ""))) //Remplace \ for ""
                {
                    isValid = true;
                }
                else
                {
                    view.ShowError("Incorect Path"); //Show error message
                }

            }
            return sourcePath;
        }
        private string GetTargetPath() //Function to retrieve the input from the source
        {
            string targetPath = "";
            bool isValid = false;

            while (!isValid) //Verify is path is valid while talking input
            {
                targetPath = Console.ReadLine(); //
                if (Directory.Exists(targetPath.Replace("\"", ""))) 
                {
                    isValid = true;
                }
                else
                {
                    view.ShowError("Incorect Path"); //Show error message
                }

            }
            return targetPath;
        }
        private string Menu()
        {
            return 

        }

    }
}
