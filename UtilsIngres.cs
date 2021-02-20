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
            ConnectionString = "Host = " + p.DbHost + "; Database = " + p.DbDatabase + "; Uid = " + p.DbUser + "; Pwd =" + p.DbPasswd + ";";
            connection = new IngresConnection(ConnectionString);
        }



        public DataTable getSql(String sqlSelect)
        {
            try
            {
                connection.Open();
                IngresCommand ingCmd = new IngresCommand();

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
                MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
            
        }
    }
}
