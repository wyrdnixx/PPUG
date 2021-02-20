using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPUG
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Parameters p = new Parameters();
            p = init(p);

            UtilsIngres utilsIngres = new UtilsIngres(p);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(p, utilsIngres));

         
        }


      
        private static Parameters init(Parameters p)
        {
            p.DbHost = "medico-mcb";
            p.DbUser = "dps";
            p.DbPasswd = "dps1991";
            p.DbDatabase = "nt_fri";

            return p;

        }




    }






}

