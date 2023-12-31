﻿using System;
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
        public string languagePreference;
       
        
        public void RunStart()
        {
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("               Welcome to Easysave_v1.0 by Prosoft               ");
            Console.WriteLine("             Bienvenue sur Easysave_v1.0 par Prosoft             ");
            Console.ResetColor();
            Console.WriteLine(" ");
            Console.WriteLine("-----------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Available Languages / Langues disponibles: ");
            Console.WriteLine("E : English");
            Console.WriteLine("F : Français");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Enter your Choice: ");
            languagePreference = Console.ReadLine();
            if(languagePreference != "F" && languagePreference != "E")
            {
                Console.WriteLine("Language not available, Please enter again / Langue non disponible, veuillez saisir à nouveau");
                RunStart();
            }
                       
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
                Console.WriteLine("1 - Full backup");
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Veuillez saisir le chemin du répertoire source : [GLISSEZ-DÉPOSEZ VOTRE DOSSIER]");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Please enter the path of the source directory: [DRAG AND DROP YOUR FOLDER]");
                Console.ResetColor();
            }
        }
        public void GetTargetPath()
        {
            if(languagePreference == "F")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Veuillez saisir le chemin du répertoire cible : [GLISSEZ-DÉPOSEZ VOTRE DOSSIER]");
                Console.ResetColor();
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please enter the path of the target directory: [DRAG AND DROP YOUR FOLDER]");
                Console.ResetColor();
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
       
    }

}
