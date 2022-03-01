using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;   
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TeknoA_Database
{
    public partial class DatabaseScreen : Form
    {

        
        public DatabaseScreen()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            LoginScreen ls = Application.OpenForms.OfType<LoginScreen>().FirstOrDefault();
            this.usernameLabel.Text = ls.loggedInUsername;
            if(ls.isUserAdmin == false)
            {
                adminPanelButton.Hide();
            }
            RefreshProductDatatable();
            RefreshCustomerDatatable();
            RefreshLogisticsDatatable();
            comboBox1.Text = "PRODUCT_ID";
            comboBox2.Text = "CUSTOMER_ID";
            comboBox3.Text = "LOGISTICS_ID";



        }

        // DATABASE METHODS

        public MySqlConnection ConnectDatabase()
        {
            string server = "localhost";
            string database = "teknoa_db";
            string username = "root";
            string password = "root";
            string constring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";
            MySqlConnection conn = new MySqlConnection(constring);
            return conn;
        }

        //////////////////

        public void RefreshProductDatatable()
        {
            MySqlConnection conn = ConnectDatabase();
            string query = "SELECT * FROM product";
            MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
            conn.Open();
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        public void RefreshCustomerDatatable()
        {
            MySqlConnection conn = ConnectDatabase();
            string query = "SELECT * FROM customer";
            MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
            conn.Open();
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView2.DataSource = dt;
            conn.Close();
        }
        public void RefreshLogisticsDatatable()
        {
            MySqlConnection conn = ConnectDatabase();
            string query = "SELECT * FROM logistics";
            MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
            conn.Open();
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView3.DataSource = dt;
            conn.Close();
        }


        private void DatabaseScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void adminPanelButton_Click(object sender, EventArgs e)
        {
            AdminScreen adminScreen = new AdminScreen();
            adminScreen.ShowDialog();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox1.Text;
                string choosedFilter = comboBox1.SelectedItem.ToString();
                string query = "SELECT * FROM product WHERE `" + choosedFilter + "` LIKE '%" + searchedKeyword + "%'";
                MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
                conn.Open();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (MySqlException myException)
            {
                Console.WriteLine(myException);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox2.Text;
                string query = "SELECT * FROM product WHERE CONCAT(PRODUCT_ID,CUSTOMER_ID,LOGISTICS_ID,PRODUCT_CODE,PRODUCT_NAME,PRODUCT_QUANTITY,PRODUCT_SERIAL_NO,PRODUCT_ANT_SERIAL_NO,PRODUCT_CONFIGURATION,PRODUCT_WARRANTY_PERIOD,PRODUCT_MANUFACTURER_SUPPORT_PERIOD,PRODUCT_ENDOFLIFE_DATE,PRODUCT_ENDOFSUPPORT_DATE,PRODUCT_REPAIR_DATE,PRODUCT_REPAIR_DETAILS,PRODUCT_RETURN_DATE,PRODUCT_RENEWAL_DETAILS,TEKNOA_SUPPORT_PERIOD,TEKNOA_SUPPORT_RENEWAL_DATE,CUSTOMER_REG_DATE,TEKNOA_OFFER_DATE,CUSTOMER_PO_DATE,TEKNOA_INVOICE_DATE,RESELLER_NAME,RESELLER_ADDRESS,RESELLER_BILLING_DETAILS,RESELLER_DELIVERY_DETAILS,CUSTOMER_BILLING_DETAILS,CUSTOMER_DELIVERY_DETAILS) LIKE '%" + searchedKeyword + "%'";
                MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
                conn.Open();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (MySqlException myException)
            {
                Console.WriteLine(myException);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshProductDatatable();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox3.Text;
                string choosedFilter = comboBox2.SelectedItem.ToString();
                string query = "SELECT * FROM customer WHERE `" + choosedFilter + "` LIKE '%" + searchedKeyword + "%'";
                MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
                conn.Open();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView2.DataSource = dt;
                conn.Close();
            }
            catch (MySqlException myException)
            {
                Console.WriteLine(myException);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox4.Text;
                string query = "SELECT * FROM customer WHERE CONCAT(CUSTOMER_ID,CUSTOMER_NAME,CUSTOMER_ADDRESS,CUSTOMER_CONTACTS_INFO) LIKE '%" + searchedKeyword + "%'";
                MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
                conn.Open();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView2.DataSource = dt;
                conn.Close();
            }
            catch (MySqlException myException)
            {
                Console.WriteLine(myException);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshCustomerDatatable();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox5.Text;
                string choosedFilter = comboBox3.SelectedItem.ToString();
                string query = "SELECT * FROM logistics WHERE `" + choosedFilter + "` LIKE '%" + searchedKeyword + "%'";
                MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
                conn.Open();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView3.DataSource = dt;
                conn.Close();
            }
            catch (MySqlException myException)
            {
                Console.WriteLine(myException);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox6.Text;
                string query = "SELECT * FROM logistics WHERE CONCAT(LOGISTICS_ID,TRANSPORTER_NAME,TRANSPORTER_DETAILS,CUSTOMS_DUTY_DETAILS,CUSTOMS_CLEARANCE_AGENT_NAME,AGENT_DEMANDS,AGENT_BILLING_DETAILS,CUSTOMS_CLEARANCE_NUMBER,CUSTOMS_TAX_PAYMENT_INFO,PRODUCT_STOCK_DATE,PRODUCT_VOLUME) LIKE '%" + searchedKeyword + "%'";
                MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
                conn.Open();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView3.DataSource = dt;
                conn.Close();
            }
            catch (MySqlException myException)
            {
                Console.WriteLine(myException);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshLogisticsDatatable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Product";
            // storing header part in Excel  
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            app.Columns.AutoFit();
            // save the application  
            //  workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Customer";
            // storing header part in Excel  
            for (int i = 1; i < dataGridView2.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView2.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                }
            }
            app.Columns.AutoFit();
            // save the application  
            //  workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Logistics";
            // storing header part in Excel  
            for (int i = 1; i < dataGridView3.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView3.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView3.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView3.Rows[i].Cells[j].Value.ToString();
                }
            }
            app.Columns.AutoFit(); 
            // save the application  
            //  workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();
        }
    }
}
