using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PPUGv2
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

                string SQlDate = DateTime.Today.AddDays(-1).ToString("d");
                

                string strFromFile = System.IO.File.ReadAllText(p.InFile);               

                return strFromFile.Replace("$BDGPC_STARTDATE", SQlDate);

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error : "+  ex.Message);
                //throw ex;
                return null;
                
            }
        }

        /// <summary>
        /// saves string contend to csv file
        /// </summary>
        /// <param name="_toSaveString"></param>
        /// <returns></returns>
        public void saveToCSV(string _toSaveString, string destFile)
        {
            try
            {
                File.WriteAllText(destFile, _toSaveString);


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }


        public static string RemoveFirstLine(string input)
        {
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return string.Join(Environment.NewLine, lines.Skip(1));
        }

        public string DatatableToCsv(DataTable dataTable)
        {

            //checked for the datatable dtCSV not empty
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                // create object for the StringBuilder class
                StringBuilder sb = new StringBuilder();

                // Get name of columns from datatable and assigned to the string array
                string[] columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();

                // Create comma sprated column name based on the items contains string array columnNames
                sb.AppendLine(string.Join(";", columnNames));

                // Fatch rows from datatable and append values as comma saprated to the object of StringBuilder class 
                foreach (DataRow row in dataTable.Rows)
                {
                    // encapsulate fields in ""
                    //IEnumerable<string> fields = row.ItemArray.Select(field => string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                    // dont encapsulate
                    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());

                    sb.AppendLine(string.Join(";", fields));
                }

               //MessageBox.Show(RemoveFirstLine(sb.ToString()));
                return RemoveFirstLine(sb.ToString());
            }
            else
                return null;
                
        }

    }
}
