using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPUGv2
{
    class Program
    {

        private static Parameters p;
        private static Utils utils;
        private static UtilsIngres utilsIngres;


        static void Main(string[] args)
        {
            p = new Parameters();
            p = init(p);

            Console.WriteLine("Programm gestartet...");

            if (args.Length == 0)
            {
                // ToDo: Info ausgeben
            }
            // if not 5 args are given or second not -i or fourth not -o
            else if (args.Length != 5 || args[1] != "-i" || args[3] != "-o")
            {
               Console.WriteLine("wrong parameters... \n use: \n ppug.exe medico.cfg -i ppug.sql -o outfile.csv");
            }
            // correct parameters given
            else
            {
                processArgs(args);

                init(p);

                string SQLCommand = utils.getSQLSelect();
                //MessageBox.Show(SQLCommand);

                DataTable dt = utilsIngres.getSql(SQLCommand);
                if (dt != null)
                    utils.saveToCSV(utils.DatatableToCsv(dt), p.OutFile);

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
                Console.WriteLine("Fehler beim starten: \n" + ex.Message);
                throw;
            }

        }


        private static Parameters init(Parameters p)
        {
            p.DbHost = "medico-mcb";
            p.DbUser = "dps";
            p.DbPasswd = "dps1991";
            p.DbDatabase = "nt_fri";

            utilsIngres = new UtilsIngres(p);
            utils = new Utils(p, utilsIngres);

            return p;

        }


    }
}
