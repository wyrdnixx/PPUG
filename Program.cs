using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPUG
{
    static class Program
    {

        private static Parameters p;
        private static Utils utils;
        private static UtilsIngres utilsIngres;
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            p = new Parameters();
            p = init(p);
            utilsIngres = new UtilsIngres(p);
            utils = new Utils(p, utilsIngres);

            // ppug.exe medico.cfg -i ppug.sql -o outfile.csv

            // caled without parameters -> start GUI
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(p, utils, utilsIngres));
            } 
            // if not 5 args are given or second not -i or fourth not -o
            else if (args.Length != 5 || args[1] != "-i"  || args[3] != "-o" )
            {
                MessageBox.Show("wrong parameters... \n use: \n ppug.exe medico.cfg -i ppug.sql -o outfile.csv");
            } 
            // correct parameters given
            else
            {
                processArgs(args);
                
                string SQLCommand = utils.getSQLSelect();
                MessageBox.Show(SQLCommand);
            }

        }

        private static void processArgs(string[] args)
        {
            try
            {
                string ConnectionString = System.IO.File.ReadAllText(args[0]);

                p.DbConnectionString = ConnectionString.Replace("ConnectionString=", "");
                //MessageBox.Show(p.DbConnectionString);

                p.InFile = args[2];
                p.OutFile = args[4];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim starten: \n" + ex.Message);
                throw;
            }
            
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

