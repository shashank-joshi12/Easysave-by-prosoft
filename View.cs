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

            }
            else if (languagePreference == "E")
            {

            }





        }
        public void setController(IController cont)
        {
            controller = cont;
        }
    }

}
