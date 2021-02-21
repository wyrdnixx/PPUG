using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPUG
{
    public class Utils
    {
        private Parameters p;
        private UtilsIngres utilsIngres;
        public Utils(Parameters p, UtilsIngres utilsIngres)
        {
            this.p = p;
            this.utilsIngres = utilsIngres;


        }

        public string getSQLSelect()
        {
            try
            {
                return System.IO.File.ReadAllText(p.InFile);

            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

    }
}
