using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using Easysave_v1._0_by_prosoft.model;
using Easysave_v1._0_by_prosoft.view;

namespace Easysave_v1._0_by_prosoft.controller
{
    class Controller : IController
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
        private string Menu()
        {
            return 

        }

    }
}
