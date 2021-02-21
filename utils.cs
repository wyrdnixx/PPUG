using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                MessageBox.Show("Error : "+  ex.Message);
                //throw ex;
                return null;
                
            }
        }

        /// <summary>
        /// saves string contend to csv file
        /// </summary>
        /// <param name="_toSaveString"></param>
        /// <returns></returns>
        public bool saveToCSV(string _toSaveString)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
                
            }
        }

        public string stringToCsv(DataTable dataTable)
        {
            StringBuilder sbData = new StringBuilder();

            // Only return Null if there is no structure.
            if (dataTable.Columns.Count == 0)
                return null;


            //Header
            foreach (var col in dataTable.Columns)
            {
                if (col == null)
                    sbData.Append(";");
                //  else  // Values in "
                //      sbData.Append("\"" + col.ToString().Replace("\"", "\"\"") + "\",");
            }

            sbData.Replace(",", System.Environment.NewLine, sbData.Length - 1, 1);

            foreach (DataRow dr in dataTable.Rows)
            {
                foreach (var column in dr.ItemArray)
                {
                    if (column == null)
                        sbData.Append(";");
                    //  else  // Values in "
                    // sbData.Append("\"" + column.ToString().Replace("\"", "\"\"") + "\",");
                }
                sbData.Replace(";", System.Environment.NewLine, sbData.Length - 1, 1);
            }


            // sbData enthält die spaltentitel - hier entferne die erste Zeile
            var lines = sbData.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return string.Join(Environment.NewLine, lines.Skip(1));

            //string res = sbData.ToString().Substring(sbData.ToString().IndexOf(Environment.NewLine) + 1);
            //eturn res;
        }

    }
}
