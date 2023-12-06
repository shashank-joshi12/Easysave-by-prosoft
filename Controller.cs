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
                    view.ShowError("Incorect Path"); //show error is path invalid
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
