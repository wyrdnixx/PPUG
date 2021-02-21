using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace PPUG
{
    public partial class Form1 : Form
    {

        private Parameters p;
        private UtilsIngres utilsIngres;
        private Utils utils;
        public Form1(Parameters p, Utils utils, UtilsIngres utilsIngres)
        {
            InitializeComponent();

            this.p = p;
            this.utilsIngres = utilsIngres;
            this.utils = utils;
            tbCsvPath.Text = Application.StartupPath.ToString() + @"\export.csv";

        }

        private void btnTest_Click(object sender, EventArgs e)
        {

            String testSelect = tbSQL.Text;
            string SQlDate = DateTime.Today.ToString("d");

            //MessageBox.Show();

            testSelect = testSelect.Replace("$BDGPC_STARTDATE", SQlDate);


            DataTable dt = utilsIngres.getSql(testSelect);
            dgvTest.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            //// -> ToDo - umbau zu utils.saveToCsv
            var csvData =  utils.stringToCsv((DataTable)dgvTest.DataSource);
            MessageBox.Show(csvData);
            File.WriteAllText(tbCsvPath.Text, csvData);
        }


    }
}
