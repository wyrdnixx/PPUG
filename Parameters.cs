using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPUG
{
    public class Parameters
    {

         public   Parameters()
        {

        }


        private string dbHost;
        private string dbUser;
        private string dbPasswd;
        private string dbDatabase;

        public string DbHost { get => dbHost; set => dbHost = value; }
        public string DbUser { get => dbUser; set => dbUser = value; }
        public string DbPasswd { get => dbPasswd; set => dbPasswd = value; }
        public string DbDatabase { get => dbDatabase; set => dbDatabase = value; }
    }
}
