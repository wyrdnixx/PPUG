﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingres.Client;

namespace PPUGv2
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
                Console.WriteLine(ingCmd.CommandText + "\n\n" + ex.Message);
                //throw;
                return null;
            }
            finally
            {
                connection.Close();
            }
            
        }



        
    }
}
