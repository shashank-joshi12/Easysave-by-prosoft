using System;
using System.Collections.Generic;
using System.Text;

namespace Easysave_v1._0_by_prosoft.view
{
    class View
    {
        private string name;
        private string sourcepath;
        private string targetpath;
        private string type;
        private string languagePreference;
        private IController controller;
        
        public void RunStart()
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
                RunStart();
            }
            /*else if (languagePreference == "F")
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
            }*/
           
        }
        public void RunMenu()
        {
            if(languagePreference == "F")
            {
                Console.WriteLine("Choisissez l'option dans le menu : ");
                Console.WriteLine("0 -   Sortie");
                Console.WriteLine("1 - Ouvrir une tâche de sauvegarde existante");
                Console.WriteLine("2 - Créer un nouveau travail de sauvegarde");
            }
            else
            {
                Console.WriteLine("Choose the option from menu: ");
                Console.WriteLine("0 -  Exit");
                Console.WriteLine("1 - Open an existing Backup Job");
                Console.WriteLine("2 - Create a new Backup Job");                
            }
        }
        public void RunSubMenu()
        {
            if(languagePreference == "F")
            {
                Console.WriteLine("Choisissez le type de travail de sauvegarde dans le menu : ");
                Console.WriteLine("0 -   Sortie");
                Console.WriteLine("1 - Sauvegarde complète");
                Console.WriteLine("2 - sauvegarde différentielle");
            }
            else
            {
                Console.WriteLine("Choose the type of backup job from menu: ");
                Console.WriteLine("0 -  Exit");
                Console.WriteLine("1 - Complete backup");
                Console.WriteLine("2 - differential backup");
            }
        }
        public void GetName()
        {
            if(languagePreference == "F")
            {
                Console.WriteLine("Veuillez saisir le nom de la sauvegarde : ");
            }
            else
            {
                Console.WriteLine("Please enter the name of the backup : ");
            }
        }
        public void GetSourcePath()
        {
            if(languagePreference == "F")
            {
                Console.WriteLine("Veuillez saisir le chemin du répertoire source : [GLISSEZ-DÉPOSEZ VOTRE DOSSIER]");
            }
            else
            {
                Console.WriteLine("Please enter the path of the source directory: [DRAG AND DROP YOUR FOLDER]");
            }
        }
        public void GetTargetPath()
        {
            if(languagePreference == "F")
            {
                Console.WriteLine("Veuillez saisir le chemin du répertoire cible : [GLISSEZ-DÉPOSEZ VOTRE DOSSIER]");
            }
            else
            {
                Console.WriteLine("Please enter the path of the target directory: [DRAG AND DROP YOUR FOLDER]");
            }
        }
        public void ShowError(string res)
        {
            Console.WriteLine(res);
        }
        public void ShowFile()
        {
            if(languagePreference == "F")
            {
                Console.WriteLine("Veuillez saisir le nom de la sauvegarde : ");
            }
            else
            {
                Console.WriteLine("Please enter the name of the backup : ");
            }
        }
        public void ShowFileNames()
        {
            if (languagePreference == "F")
            {
                Console.Clear();
                Console.WriteLine("Voici une liste de toutes les sauvegardes :");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Here is a list of all the backups: ");

            }
        }
        public void setController(IController cont)
        {
            controller = cont;
        }
    }

}
