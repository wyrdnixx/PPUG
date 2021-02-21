using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ingres.Client;

namespace PPUG
{
    public class UtilsIngres
    {

        private String connectionString;
        private IngresConnection connection;

        public string ConnectionString { get => connectionString; set => connectionString = value; }
        public IngresConnection Connection { get => connection; set => connection = value; }


        public UtilsIngres(Parameters p)
        {
            //ConnectionString = "Host = " + p.DbHost + "; Database = " + p.DbDatabase + "; Uid = " + p.DbUser + "; Pwd =" + p.DbPasswd + "; Date_format = GERMAN";
            // connection = new IngresConnection(ConnectionString);

            connection = new IngresConnection(p.DbConnectionString);
        }

        public DataTable getSql(String sqlSelect)
        {

            IngresCommand ingCmd = new IngresCommand();

            try
            {
              

                connection.Open();

                ingCmd.Connection = Connection;
                ingCmd.CommandText = sqlSelect;
                ingCmd.CommandType = CommandType.Text;

                IngresDataReader dr = ingCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);

                connection.Close();

                return dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ingCmd.CommandText + "\n\n" + ex.Message);
                //throw;
                return null;
            }
            finally
            {
                connection.Close();
            }
            
        }



        public string ToCsv( DataTable dataTable)
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
