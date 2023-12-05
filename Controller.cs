using System;
using System.Collections.Generic;
using System.Text;

namespace Easysave_v1._0_by_prosoft
{
    class Controller : IController
    {
        private Model model;
        private View view;
        public Controller()
        {
            model = new Model();
            view = new View();
            view.setController(this);
            view.RunBackup();
        }
        public void Backup()
        {


        }

    }
}
