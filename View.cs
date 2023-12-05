using System;
using System.Collections.Generic;
using System.Text;

namespace Easysave_v1._0_by_prosoft
{
    class View
    {
        private string name;
        private string sourcepath;
        private string targetpath;
        private string type;
        private string languagePreference;
        private IController controller;
        
        public void RunBackup()
        {
            Console.WriteLine("---------------Welcome to Easysave_v1.0 by Prosoft---------------");
            Console.WriteLine("-------------Bienvenue sur Easysave_v1.0 par Prosoft-------------");
            Console.WriteLine("Available Languages / Langues disponibles: ");
            Console.WriteLine("E : English");
            Console.WriteLine("F : français");
            Console.Write("Enter your Choice: ");
            languagePreference = Console.ReadLine();
            if(languagePreference != "F" || languagePreference != "E")
            {
                Console.WriteLine("Language not available, Please enter again / Langue non disponible, veuillez saisir à nouveau");
                RunBackup();
            }
            else if (languagePreference == "F")
            {
                Console.WriteLine("Cette application vous permet de créer des sauvegardes de répertoires");
                Console.WriteLine("Veuillez saisir les paramètres suivants pour chaque tâche de sauvegarde :");
                Console.WriteLine("Nom de la sauvegarde : le nom de la tâche de sauvegarde");
                Console.WriteLine("Source de sauvegarde : le répertoire source dans lequel sauvegarder le travail");
                Console.WriteLine("Cible de sauvegarde : le répertoire de destination pour stocker la sauvegarde");
                Console.WriteLine("Type de sauvegarde : D pour différentiel ou B pour sauvegarde");

            }
            else if (languagePreference == "E")
            {
                Console.WriteLine("This app allows you to create backups of directories");
                Console.WriteLine("Please enter the following parameters for each backup task:");
                Console.WriteLine("Backup Name: The name of the backup job");
                Console.WriteLine("Backup Source: The source directory to backup job");
                Console.WriteLine("Backup Target: The destination directory to store the backup");
                Console.WriteLine("Backup Type: D for Differential or B for Backup");
            }






        }
        public void setController(IController cont)
        {
            controller = cont;
        }
    }

}
