using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;




namespace TeknoA_Database
{
    public partial class AdminScreen : Form
    {
        int a;
        int logisticsIdFromClick;
        public AdminScreen()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            RefreshUsersDatatableAdmin();
            RefreshCustomerDatatableAdmin();
            RefreshLogisticsDatatableAdmin();
            RefreshProductDatatableAdmin();
            RefreshLogsDatatableAdmin();
            comboBox1.Text = "CUSTOMER_ID";
            comboBox2.Text = "LOGISTICS_ID";
            comboBox3.Text = "uid";
            comboBox4.Text = "PRODUCT_ID";
            dataGridView4.Columns[5].HeaderText = "Arguments";



        }


        // DATABASE METHODS

        public MySqlConnection ConnectDatabase()
        {
            string server = "localhost";
            string database = "teknoa_db";
            string username = "root";
            string password = "root";
            string constring = "SERVER=" + server + ";"
                + "DATABASE=" + database + ";"
                + "UID=" + username + ";"
                + "PASSWORD=" + password + ";";
            MySqlConnection conn = new MySqlConnection(constring);
            return conn;
        }

        //////////////////


        public void RefreshUsersDatatableAdmin()
        {
            MySqlConnection conn = ConnectDatabase();
            string query = "SELECT * FROM users";
            MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
            conn.Open();
            DataTable dt = new DataTable();
            adp.Fill(dt);
            usersDatagridAdmin.DataSource = dt;

            conn.Close();
        }

        public void RefreshCustomerDatatableAdmin()
        {
            MySqlConnection conn = ConnectDatabase();
            string query = "SELECT * FROM customer";
            MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
            conn.Open();
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        public void RefreshLogisticsDatatableAdmin()
        {
            MySqlConnection conn = ConnectDatabase();
            string query = "SELECT * FROM logistics";
            MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
            conn.Open();
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView2.DataSource = dt;
            conn.Close();
        }

        public void RefreshProductDatatableAdmin()
        {
            MySqlConnection conn = ConnectDatabase();
            string query = "SELECT * FROM product";
            MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
            conn.Open();
            DataTable dt = new DataTable();
           
            adp.Fill(dt);
            dataGridView3.DataSource = dt;
            conn.Close();
        }

        public void RefreshLogsDatatableAdmin()
        {
            MySqlConnection conn = ConnectDatabase();
            string query = "SELECT ((event_time)),((user_host)),((thread_id)),((server_id)),((command_type)),CONVERT((argument) USING utf8) FROM mysql.general_log WHERE CONVERT((argument) USING utf8) != 'SELECT * FROM users' && CONVERT((argument) USING utf8) NOT LIKE '%((event_time))%' && command_type LIKE '%Query%' && (CONVERT((argument) USING utf8) LIKE '%users%' || CONVERT((argument) USING utf8) LIKE '%INSERT%' || CONVERT((argument) USING utf8) LIKE '%DELETE%' || CONVERT((argument) USING utf8) LIKE '%UPDATE%')";
            MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
            conn.Open();

            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView4.DataSource = dt;

            conn.Close();
        }

        private void refreshButtonUsersAdmin_Click(object sender, EventArgs e)
        {
            RefreshUsersDatatableAdmin();
        }

        private void deleteButtonUsersAdmin_Click(object sender, EventArgs e)
        {
            int rowIndex = usersDatagridAdmin.CurrentCell.RowIndex;
            string selectedRowStr = usersDatagridAdmin.Rows[rowIndex].Cells["uid"].FormattedValue.ToString();
            int selectedRowId = int.Parse(selectedRowStr);

            MySqlConnection conn = ConnectDatabase();
            conn.Open();
            string query = "DELETE FROM users WHERE uid = '"+selectedRowId+"'";
            MySqlCommand cmd = new MySqlCommand(query,conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Succesfuly Deleted");
            RefreshUsersDatatableAdmin();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            bool isAdmin = checkBox1.Checked;
            int isAdminInt = 0;
            if (isAdmin)
            {
                isAdminInt = 1;
            }

            int rowIndex = usersDatagridAdmin.CurrentCell.RowIndex;
            string selectedRowStr = usersDatagridAdmin.Rows[rowIndex].Cells["uid"].FormattedValue.ToString();
            int selectedRowId = int.Parse(selectedRowStr);


            MySqlConnection conn = ConnectDatabase();
            conn.Open();

            string query = "UPDATE users SET uname = '"+username+"',upassword = '"+password+"',isAdmin = '"+isAdminInt+"' WHERE uid = '"+selectedRowId+"'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            RefreshUsersDatatableAdmin();
            MessageBox.Show("Succesfuly edited");
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox4.Text;
            string password = textBox3.Text;


            MySqlConnection conn = ConnectDatabase();
            conn.Open();

            string query = "INSERT INTO users (uname,upassword) VALUES ('"+username+"','"+password+"')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            RefreshUsersDatatableAdmin();
            MessageBox.Show("Succesfuly added");
        }

        private void usersDatagridAdmin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int indexRow = e.RowIndex;
                DataGridViewRow row = usersDatagridAdmin.Rows[indexRow];

                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                string tmp = row.Cells[3].Value.ToString();
                if (tmp == "True")
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
            } 
            catch(ArgumentOutOfRangeException myException)
            {
                Console.WriteLine(myException);
            }

            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            RefreshCustomerDatatableAdmin();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            string selectedRowStr = dataGridView1.Rows[rowIndex].Cells["CUSTOMER_ID"].FormattedValue.ToString();
            int selectedRowId = int.Parse(selectedRowStr);

            MySqlConnection conn = ConnectDatabase();
            conn.Open();
            string query = "DELETE FROM customer WHERE CUSTOMER_ID = '" + selectedRowId + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            RefreshCustomerDatatableAdmin();
            MessageBox.Show("Succesfuly Deleted");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int indexRow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[indexRow];

                textBox10.Text = row.Cells[1].Value.ToString();
                textBox9.Text = row.Cells[2].Value.ToString();
                textBox11.Text = row.Cells[3].Value.ToString();
            }
            catch (ArgumentOutOfRangeException myException)
            {
                Console.WriteLine(myException);
            }
 
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string customerName = textBox10.Text;
            string customerAddress = textBox9.Text;
            string customerConctacts = textBox11.Text;

            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            string selectedRowStr = dataGridView1.Rows[rowIndex].Cells["CUSTOMER_ID"].FormattedValue.ToString();
            int selectedRowId = int.Parse(selectedRowStr);


            MySqlConnection conn = ConnectDatabase();
            conn.Open();

            string query = "UPDATE customer SET CUSTOMER_NAME = '" + customerName + "', CUSTOMER_ADDRESS = '"+ customerAddress + "', CUSTOMER_CONTACTS_INFO = '"+customerConctacts+"' WHERE CUSTOMER_ID = '" + selectedRowId + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            RefreshCustomerDatatableAdmin();
            MessageBox.Show("Succesfuly edited");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string customerName = textBox10.Text;
            string customerAddress = textBox9.Text;
            string customerConctacts = textBox11.Text;


            MySqlConnection conn = ConnectDatabase();
            conn.Open();

            string query = "INSERT INTO customer (CUSTOMER_NAME,CUSTOMER_ADDRESS,CUSTOMER_CONTACTS_INFO) VALUES ('" + customerName + "','" + customerAddress + "','" + customerConctacts + "')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            RefreshCustomerDatatableAdmin();
            MessageBox.Show("Succesfuly added");

        }

        private void button9_Click(object sender, EventArgs e)
        {
            RefreshLogisticsDatatableAdmin();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int indexRow = e.RowIndex;
                DataGridViewRow row = dataGridView2.Rows[indexRow];

                textBox19.Text = row.Cells[1].Value.ToString();
                textBox18.Text = row.Cells[2].Value.ToString();
                textBox17.Text = row.Cells[3].Value.ToString();
                textBox16.Text = row.Cells[4].Value.ToString();
                textBox15.Text = row.Cells[5].Value.ToString();
                textBox12.Text = row.Cells[6].Value.ToString();
                textBox24.Text = row.Cells[7].Value.ToString();
                textBox23.Text = row.Cells[8].Value.ToString();
                textBox22.Text = row.Cells[9].Value.ToString();
                textBox33.Text = row.Cells[10].Value.ToString();
                textBox20.Text = row.Cells[11].Value.ToString();
                logisticsIdFromClick = int.Parse(row.Cells[0].Value.ToString());
            }
            catch (ArgumentOutOfRangeException myException)
            {
                Console.WriteLine(myException);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string transporterName = textBox19.Text;
            string transporterDetails = textBox18.Text;
            string customsDutyDetails = textBox17.Text;
            string customsClearanceAgentName = textBox16.Text;
            string agentDemands = textBox15.Text;

            string agentInvoiceStr = textBox12.Text;
            double agentInvoice;
            if(agentInvoiceStr == "")
            {
                agentInvoice = 0.0;
            }
            else
            {
                agentInvoice = double.Parse(agentInvoiceStr);
            }
            

            string agentBillingDetails = textBox24.Text;
            string customsClearanceNumber = textBox23.Text;
            string customsTaxPaymentInfo = textBox22.Text;
            string productStockDate = textBox33.Text;
            string productVolume = textBox20.Text;

           
            int rowIndex = dataGridView2.CurrentCell.RowIndex;
            string selectedRowStr = dataGridView2.Rows[rowIndex].Cells["LOGISTICS_ID"].FormattedValue.ToString();
            int selectedRowId = int.Parse(selectedRowStr);


            MySqlConnection conn = ConnectDatabase();
            MySqlConnection conn2 = ConnectDatabase();
            MySqlConnection conn3 = ConnectDatabase();
            MySqlConnection conn4 = ConnectDatabase();
            conn.Open();
            conn2.Open();
            conn3.Open();
            conn4.Open();



            string query4 = "SELECT AGENT_INVOICE FROM logistics WHERE LOGISTICS_ID = '" + logisticsIdFromClick + "'";
            MySqlCommand cmd4 = new MySqlCommand(query4, conn4);
            MySqlDataReader dr2 = cmd4.ExecuteReader();
            dr2.Read();
            double oldAgentInvoice = dr2.GetDouble(0);

            string query2 = "SELECT PRODUCT_UNIT_COST FROM product WHERE LOGISTICS_ID = '" + logisticsIdFromClick + "'";
            MySqlCommand cmd2 = new MySqlCommand(query2, conn2);
            MySqlDataReader dr = cmd2.ExecuteReader();
            while (dr.Read())
            {

       

                double tmp = dr.GetDouble(0);
                tmp = tmp - oldAgentInvoice;
                double tmp2 = tmp + agentInvoice;
                double listPrice = tmp2 * 1.35;
             

                string query3 = "UPDATE product SET PRODUCT_UNIT_COST = '"+tmp2+"', LIST_PRICE = '"+listPrice+"' WHERE LOGISTICS_ID = '"+logisticsIdFromClick+"'";
                MySqlCommand cmd3 = new MySqlCommand(query3, conn3);
                cmd3.ExecuteNonQuery();
            }

           


     
            

            string query = "UPDATE logistics SET TRANSPORTER_NAME = '" + transporterName + "',TRANSPORTER_DETAILS = '" + transporterDetails + "',CUSTOMS_DUTY_DETAILS = '" + customsDutyDetails + "', CUSTOMS_CLEARANCE_AGENT_NAME = '" + customsClearanceAgentName + "', AGENT_DEMANDS = '" + agentDemands + "',AGENT_INVOICE = '"+agentInvoice+"', AGENT_BILLING_DETAILS = '" + agentBillingDetails + "', CUSTOMS_CLEARANCE_NUMBER = '" + customsClearanceNumber + "', CUSTOMS_TAX_PAYMENT_INFO = '" + customsTaxPaymentInfo + "', PRODUCT_STOCK_DATE = '" + productStockDate + "', PRODUCT_VOLUME = '" + productVolume + "' WHERE LOGISTICS_ID = '" + selectedRowId + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            conn2.Close();
            conn3.Close();
            conn4.Close();
            RefreshLogisticsDatatableAdmin();
            RefreshProductDatatableAdmin();
            MessageBox.Show("Succesfuly edited");
            

            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string transporterName = textBox19.Text;
            string transporterDetails = textBox18.Text;
            string customsDutyDetails = textBox17.Text;
            string customsClearanceAgentName = textBox16.Text;
            string agentDemands = textBox15.Text;

            string agentInvoiceStr = textBox12.Text;
            double agentInvoice;
            if(agentInvoiceStr == "")
            {
                agentInvoice = 0.0;
            }
            else
            {
                agentInvoice = double.Parse(agentInvoiceStr);
            }
            

            string agentBillingDetails = textBox24.Text;
            string customsClearanceNumber = textBox23.Text;
            string customsTaxPaymentInfo = textBox22.Text;
            string productStockDate = textBox33.Text;
            string productVolume = textBox20.Text;


            MySqlConnection conn = ConnectDatabase();
            conn.Open();

            string query = "INSERT INTO logistics (TRANSPORTER_NAME,TRANSPORTER_DETAILS,CUSTOMS_DUTY_DETAILS,CUSTOMS_CLEARANCE_AGENT_NAME,AGENT_DEMANDS,AGENT_INVOICE,AGENT_BILLING_DETAILS,CUSTOMS_CLEARANCE_NUMBER,CUSTOMS_TAX_PAYMENT_INFO,PRODUCT_STOCK_DATE,PRODUCT_VOLUME) VALUES ('" + transporterName + "','" + transporterDetails + "','" + customsDutyDetails + "','" + customsClearanceAgentName + "','" + agentDemands + "','"+agentInvoice+"','" + agentBillingDetails + "','" + customsClearanceNumber + "','" + customsTaxPaymentInfo + "','" + productStockDate + "','" + productVolume + "')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            RefreshLogisticsDatatableAdmin();
            MessageBox.Show("Succesfuly added");
        }



        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox35.Text;
                string query = "SELECT * FROM customer WHERE CONCAT(CUSTOMER_ID,CUSTOMER_NAME,CUSTOMER_ADDRESS,CUSTOMER_CONTACTS_INFO) LIKE '%" + searchedKeyword + "%'";
                MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
                conn.Open();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch(MySqlException myException)
            {
                Console.WriteLine(myException);
            }
            
            

           
        }

        private void textBox36_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox36.Text;
                string choosedFilter = comboBox1.SelectedItem.ToString();
                string query = "SELECT * FROM customer WHERE `"+choosedFilter+"` LIKE '%" + searchedKeyword + "%'";
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

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox8.Text;
                string query = "SELECT * FROM logistics WHERE CONCAT(LOGISTICS_ID,TRANSPORTER_NAME,TRANSPORTER_DETAILS,CUSTOMS_DUTY_DETAILS,CUSTOMS_CLEARANCE_AGENT_NAME,AGENT_DEMANDS,AGENT_INVOICE,AGENT_BILLING_DETAILS,CUSTOMS_CLEARANCE_NUMBER,CUSTOMS_TAX_PAYMENT_INFO,PRODUCT_STOCK_DATE,PRODUCT_VOLUME) LIKE '%" + searchedKeyword + "%'";
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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox7.Text;
                string choosedFilter = comboBox2.SelectedItem.ToString();
                string query = "SELECT * FROM logistics WHERE `" + choosedFilter + "` LIKE '%" + searchedKeyword + "%'";
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

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox14.Text;
                string query = "SELECT * FROM users WHERE CONCAT(uid,uname,upassword) LIKE '%" + searchedKeyword + "%'";
                MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
                conn.Open();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                usersDatagridAdmin.DataSource = dt;
                conn.Close();
            }
            catch (MySqlException myException)
            {
                Console.WriteLine(myException);
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox13.Text;
                string choosedFilter = comboBox3.SelectedItem.ToString();
                string query = "SELECT * FROM users WHERE `" + choosedFilter + "` LIKE '%" + searchedKeyword + "%'";
                MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
                conn.Open();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                usersDatagridAdmin.DataSource = dt;
                conn.Close();
            }
            catch (MySqlException myException)
            {
                Console.WriteLine(myException);
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int indexRow = e.RowIndex;
                DataGridViewRow row = dataGridView3.Rows[indexRow];

                textBox41.Text = row.Cells[1].Value.ToString();
                textBox40.Text = row.Cells[2].Value.ToString();
                textBox39.Text = row.Cells[3].Value.ToString();
                textBox43.Text = row.Cells[4].Value.ToString();
                textBox48.Text = row.Cells[5].Value.ToString();
                textBox5.Text = row.Cells[6].Value.ToString();
                textBox21.Text = row.Cells[8].Value.ToString();
                textBox47.Text = row.Cells[9].Value.ToString();
                textBox46.Text = row.Cells[10].Value.ToString();
                textBox42.Text = row.Cells[11].Value.ToString();
                textBox50.Text = row.Cells[12].Value.ToString();
                textBox60.Text = row.Cells[13].Value.ToString();
                textBox59.Text = row.Cells[14].Value.ToString();
                textBox53.Text = row.Cells[15].Value.ToString();
                textBox52.Text = row.Cells[16].Value.ToString();
                textBox51.Text = row.Cells[17].Value.ToString();
                textBox49.Text = row.Cells[18].Value.ToString();
                textBox45.Text = row.Cells[19].Value.ToString();
                textBox44.Text = row.Cells[20].Value.ToString();
                textBox56.Text = row.Cells[21].Value.ToString();
                textBox55.Text = row.Cells[22].Value.ToString();
                textBox54.Text = row.Cells[23].Value.ToString();
                textBox64.Text = row.Cells[24].Value.ToString();
                textBox63.Text = row.Cells[25].Value.ToString();
                textBox25.Text = row.Cells[27].Value.ToString();
                textBox6.Text = row.Cells[28].Value.ToString();
                textBox26.Text = row.Cells[29].Value.ToString();
                textBox66.Text = row.Cells[30].Value.ToString();
                textBox65.Text = row.Cells[31].Value.ToString();
                textBox62.Text = row.Cells[32].Value.ToString();
                textBox61.Text = row.Cells[33].Value.ToString();
                textBox58.Text = row.Cells[34].Value.ToString();
                textBox57.Text = row.Cells[35].Value.ToString();
                a = int.Parse(row.Cells[5].Value.ToString());

            }
            catch (ArgumentOutOfRangeException myException)
            {
                Console.WriteLine(myException);
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {

            string customerIdStr = textBox41.Text;
            int customerId = int.Parse(customerIdStr);

            string logisticsIdStr = textBox40.Text;
            int logisticsId = int.Parse(logisticsIdStr);

            string productCode = textBox39.Text;
            string productName = textBox43.Text;

            string productQuantityStr = textBox48.Text;
            int productQuantity;
            if (productQuantityStr == "")
            {
                productQuantity = 0;
            }
            else
            {
                productQuantity = int.Parse(productQuantityStr);
            }


            string productUnitPriceStr = textBox5.Text;
            double productUnitPrice;
            if(productUnitPriceStr == "")
            {
                productUnitPrice = 0.0;
            }
            else
            {
                productUnitPrice = double.Parse(productUnitPriceStr);
            }

            

            string currency = textBox21.Text;
            string productSerialNo = textBox47.Text;
            string productAntSerialNo = textBox46.Text;
            string productConfiguration = textBox42.Text;
            string productWarrantyPeriod = textBox50.Text;
            string productManufacturerSupportPeriod = textBox60.Text;
            string productEndOfLifeDate = textBox59.Text;
            string productEndOfSupportDate = textBox53.Text;
            string productRepairDate = textBox52.Text;
            string productRepairDetails = textBox51.Text;
            string productReturnDate = textBox49.Text;
            string productRenewalDetails = textBox45.Text;
            string teknoaSupportPeriod = textBox44.Text;
            string teknoaSupportRenewalDate = textBox56.Text;
            string customerRegDate = textBox55.Text;
            string teknoaOfferDate = textBox54.Text;
            string customerPoDate = textBox64.Text;
            string teknoaInvoiceDate = textBox63.Text;

            string approvedListPriceStr = textBox25.Text;
            double approvedListPrice;
            if(approvedListPriceStr == "")
            {
                approvedListPrice = 0.0;
            }
            else
            {
                approvedListPrice = double.Parse(approvedListPriceStr);
            }

            string teknoaUnitSalesPriceStr = textBox6.Text;
            double teknoaUnitSalesPrice;
            if (teknoaUnitSalesPriceStr == "")
            {
                teknoaUnitSalesPrice = 0.0;
            }
            else
            {
                teknoaUnitSalesPrice = double.Parse(teknoaUnitSalesPriceStr);
            }

            string saleCurrency = textBox26.Text;
            string resellerName = textBox66.Text;
            string resellerAddress = textBox65.Text;
            string resellerBillingDetails = textBox62.Text;
            string resellerDeliveryDetails = textBox61.Text;
            string customerBillingDetails = textBox58.Text;
            string customerDeliveryDetails = textBox57.Text;


            int rowIndex = dataGridView3.CurrentCell.RowIndex;
            string selectedRowStr = dataGridView3.Rows[rowIndex].Cells["PRODUCT_ID"].FormattedValue.ToString();
            int selectedRowId = int.Parse(selectedRowStr);


            MySqlConnection conn = ConnectDatabase();
            MySqlConnection conn2 = ConnectDatabase();
            MySqlConnection conn3 = ConnectDatabase();
            MySqlConnection conn4 = ConnectDatabase();
            conn.Open();
            conn2.Open();
            conn3.Open();
            conn4.Open();

            string query2 = "SELECT PRODUCT_VOLUME FROM logistics WHERE LOGISTICS_ID = '" + logisticsId + "'";
            MySqlCommand cmd2 = new MySqlCommand(query2, conn2);
            MySqlDataReader dr = cmd2.ExecuteReader();
            dr.Read();
            int currentVolume = dr.GetInt32(0);

            int newVolume = currentVolume - a;
            newVolume = newVolume + productQuantity;

            string query3 = "UPDATE logistics SET PRODUCT_VOLUME = '" + newVolume + "' WHERE LOGISTICS_ID = '" + logisticsId + "'";
            MySqlCommand cmd3 = new MySqlCommand(query3, conn3);
            cmd3.ExecuteNonQuery();


            string query4 = "SELECT AGENT_INVOICE FROM logistics WHERE LOGISTICS_ID = '" + logisticsId + "'";
            MySqlCommand cmd4 = new MySqlCommand(query4, conn4);
            MySqlDataReader dr2 = cmd4.ExecuteReader();
            dr2.Read();
            double agentInvoice = dr2.GetDouble(0);

            double productCost = productUnitPrice + agentInvoice;
            double listPrice = productCost * 1.35;


            string query = "UPDATE product SET CUSTOMER_ID = '" + customerId + "', LOGISTICS_ID = '" + logisticsId + "', PRODUCT_CODE = '" + productCode + "', PRODUCT_NAME = '" + productName + "', PRODUCT_QUANTITY = '" + productQuantity + "',PRODUCT_UNIT_PRICE = '"+productUnitPrice+"',PRODUCT_UNIT_COST = '"+productCost+"',CURRENCY = '"+currency+"' ,PRODUCT_SERIAL_NO = '" + productSerialNo + "', PRODUCT_ANT_SERIAL_NO = '" + productAntSerialNo + "', PRODUCT_CONFIGURATION = '" + productConfiguration + "', PRODUCT_WARRANTY_PERIOD = '" + productWarrantyPeriod + "', PRODUCT_MANUFACTURER_SUPPORT_PERIOD = '" + productManufacturerSupportPeriod + "', PRODUCT_ENDOFLIFE_DATE = '" + productEndOfLifeDate + "', PRODUCT_ENDOFSUPPORT_DATE = '" + productEndOfSupportDate + "', PRODUCT_REPAIR_DATE = '" + productRepairDate + "', PRODUCT_REPAIR_DETAILS = '" + productRepairDetails + "', PRODUCT_RETURN_DATE = '" + productReturnDate + "', PRODUCT_RENEWAL_DETAILS = '" + productRenewalDetails + "', TEKNOA_SUPPORT_PERIOD = '" + teknoaSupportPeriod + "', TEKNOA_SUPPORT_RENEWAL_DATE = '" + teknoaSupportRenewalDate + "', CUSTOMER_REG_DATE = '" + customerRegDate + "', TEKNOA_OFFER_DATE = '" + teknoaOfferDate + "', CUSTOMER_PO_DATE = '" + customerPoDate + "', TEKNOA_INVOICE_DATE = '" + teknoaInvoiceDate + "',LIST_PRICE = '"+listPrice+"',APPROVED_LIST_PRICE = '"+approvedListPrice+"',TEKNOA_UNIT_SALES_PRICE = '"+teknoaUnitSalesPrice+"', SALE_CURRENCY = '"+saleCurrency+"' , RESELLER_NAME = '" + resellerName + "', RESELLER_ADDRESS = '" + resellerAddress + "', RESELLER_BILLING_DETAILS = '" + resellerBillingDetails + "', RESELLER_DELIVERY_DETAILS = '" + resellerDeliveryDetails + "', CUSTOMER_BILLING_DETAILS = '" + customerBillingDetails + "', CUSTOMER_DELIVERY_DETAILS = '" + customerDeliveryDetails + "' WHERE PRODUCT_ID = '" + selectedRowId + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            conn2.Close();
            conn3.Close();
            conn4.Close();
            RefreshProductDatatableAdmin();
            RefreshLogisticsDatatableAdmin();
            MessageBox.Show("Succesfuly edited");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            RefreshProductDatatableAdmin();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            
           
            try {

                int rowIndex = dataGridView3.CurrentCell.RowIndex;
                string selectedRowStr = dataGridView3.Rows[rowIndex].Cells["PRODUCT_ID"].FormattedValue.ToString();
                int selectedRowId = int.Parse(selectedRowStr);

                string selectedRowStr2 = dataGridView3.Rows[rowIndex].Cells["LOGISTICS_ID"].FormattedValue.ToString();
                int logisticsId = int.Parse(selectedRowStr2);


                MySqlConnection conn = ConnectDatabase();
                MySqlConnection conn2 = ConnectDatabase();
                MySqlConnection conn3 = ConnectDatabase();
                MySqlConnection conn4 = ConnectDatabase();
                conn.Open();
                conn2.Open();
                conn3.Open();
                conn4.Open();

                string query2 = "SELECT PRODUCT_QUANTITY FROM product WHERE PRODUCT_ID = '" + selectedRowId + "'";
                MySqlCommand cmd2 = new MySqlCommand(query2, conn2);
                MySqlDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                int productQuantity = dr.GetInt32(0);

                string query3 = "SELECT PRODUCT_VOLUME FROM logistics WHERE LOGISTICS_ID = '" + logisticsId + "'";
                MySqlCommand cmd3 = new MySqlCommand(query3, conn3);
                MySqlDataReader dr2 = cmd3.ExecuteReader();
                dr2.Read();
                int productVolume = dr2.GetInt32(0);

                productVolume = productVolume - productQuantity;

                string query4 = "UPDATE logistics SET PRODUCT_VOLUME = '" + productVolume + "' WHERE LOGISTICS_ID = '" + logisticsId + "'";
                MySqlCommand cmd4 = new MySqlCommand(query4, conn4);
                cmd4.ExecuteNonQuery();

                string query = "DELETE FROM product WHERE PRODUCT_ID = '" + selectedRowId + "'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                conn2.Close();
                conn3.Close();
                conn4.Close();
                RefreshProductDatatableAdmin();
                RefreshLogisticsDatatableAdmin();
                MessageBox.Show("Succesfuly Deleted");


            }
            catch(FormatException MyException) { }

            

        }

        private void button14_Click(object sender, EventArgs e)
        {
            string customerIdStr = textBox41.Text;
            int customerId = int.Parse(customerIdStr);

            string logisticsIdStr = textBox40.Text;
            int logisticsId = int.Parse(logisticsIdStr);

            string productCode = textBox39.Text;
            string productName = textBox43.Text;

            string productQuantityStr = textBox48.Text;
            int productQuantity;
            if (productQuantityStr == "")
            {
                productQuantity = 1;
            }
            else
            {
                productQuantity = int.Parse(productQuantityStr);

            }


            string productUnitPriceStr = textBox5.Text;
            double productUnitPrice;
            if(productUnitPriceStr == "")
            {
                productUnitPrice = 0.0;
            }
            else
            {
                productUnitPrice = double.Parse(productUnitPriceStr);
            }

            string currency = textBox21.Text;
            string productSerialNo = textBox47.Text;
            string productAntSerialNo = textBox46.Text;
            string productConfiguration = textBox42.Text;
            string productWarrantyPeriod = textBox50.Text;
            string productManufacturerSupportPeriod = textBox60.Text;
            string productEndOfLifeDate = textBox59.Text;
            string productEndOfSupportDate = textBox53.Text;
            string productRepairDate = textBox52.Text;
            string productRepairDetails = textBox51.Text;
            string productReturnDate = textBox49.Text;
            string productRenewalDetails = textBox45.Text;
            string teknoaSupportPeriod = textBox44.Text;
            string teknoaSupportRenewalDate = textBox56.Text;
            string customerRegDate = textBox55.Text;
            string teknoaOfferDate = textBox54.Text;
            string customerPoDate = textBox64.Text;
            string teknoaInvoiceDate = textBox63.Text;

            string approvedListPriceStr = textBox25.Text;
            double approvedListPrice;
            if(approvedListPriceStr == "")
            {
                approvedListPrice = 0.0;
            }
            else
            {
                approvedListPrice = double.Parse(approvedListPriceStr);
            }

            string teknoaUnitSalesPriceStr = textBox6.Text;
            double teknoaUnitSalesPrice;
            if(teknoaUnitSalesPriceStr == "")
            {
                teknoaUnitSalesPrice = 0;
            }
            else
            {
                 teknoaUnitSalesPrice = double.Parse(teknoaUnitSalesPriceStr);
            }

            string saleCurrency = textBox26.Text;
            string resellerName = textBox66.Text;
            string resellerAddress = textBox65.Text;
            string resellerBillingDetails = textBox62.Text;
            string resellerDeliveryDetails = textBox61.Text;
            string customerBillingDetails = textBox58.Text;
            string customerDeliveryDetails = textBox57.Text;

            


            MySqlConnection conn = ConnectDatabase();
            MySqlConnection conn2 = ConnectDatabase();
            MySqlConnection conn3 = ConnectDatabase();
            MySqlConnection conn4 = ConnectDatabase();
            conn.Open();
            conn2.Open();
            conn3.Open();
            conn4.Open();
            try
            {
                

                string query2 = "SELECT PRODUCT_VOLUME FROM logistics WHERE LOGISTICS_ID = '"+logisticsId+"'";
                MySqlCommand cmd2 = new MySqlCommand(query2, conn2);
                MySqlDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                int currentVolume = dr.GetInt32(0);

                currentVolume = currentVolume + productQuantity;

                string query3 = "UPDATE logistics SET PRODUCT_VOLUME = '"+currentVolume+"' WHERE LOGISTICS_ID = '"+logisticsId+"'";
                MySqlCommand cmd3 = new MySqlCommand(query3, conn3);
                cmd3.ExecuteNonQuery();

                string query4 = "SELECT AGENT_INVOICE FROM logistics WHERE LOGISTICS_ID = '" + logisticsId + "'";
                MySqlCommand cmd4 = new MySqlCommand(query4, conn4);
                MySqlDataReader dr2 = cmd4.ExecuteReader();
                dr2.Read();
                double agentInvoice = dr2.GetDouble(0);

                double productCost = agentInvoice + productUnitPrice;

                double listPrice = productCost * 1.35;

                string query = "INSERT INTO product (CUSTOMER_ID,LOGISTICS_ID,PRODUCT_CODE,PRODUCT_NAME,PRODUCT_QUANTITY,PRODUCT_UNIT_PRICE,PRODUCT_UNIT_COST,CURRENCY,PRODUCT_SERIAL_NO,PRODUCT_ANT_SERIAL_NO,PRODUCT_CONFIGURATION,PRODUCT_WARRANTY_PERIOD,PRODUCT_MANUFACTURER_SUPPORT_PERIOD,PRODUCT_ENDOFLIFE_DATE,PRODUCT_ENDOFSUPPORT_DATE,PRODUCT_REPAIR_DATE,PRODUCT_REPAIR_DETAILS,PRODUCT_RETURN_DATE,PRODUCT_RENEWAL_DETAILS,TEKNOA_SUPPORT_PERIOD,TEKNOA_SUPPORT_RENEWAL_DATE,CUSTOMER_REG_DATE,TEKNOA_OFFER_DATE,CUSTOMER_PO_DATE,TEKNOA_INVOICE_DATE,LIST_PRICE,APPROVED_LIST_PRICE,TEKNOA_UNIT_SALES_PRICE,SALE_CURRENCY,RESELLER_NAME,RESELLER_ADDRESS,RESELLER_BILLING_DETAILS,RESELLER_DELIVERY_DETAILS,CUSTOMER_BILLING_DETAILS,CUSTOMER_DELIVERY_DETAILS) VALUES ('" + customerId + "','" + logisticsId + "','" + productCode + "','" + productName + "','" + productQuantity + "','" + productUnitPrice + "','"+productCost+"','"+currency+"','" + productSerialNo + "','" + productAntSerialNo + "','" + productConfiguration + "','" + productWarrantyPeriod + "','" + productManufacturerSupportPeriod + "','" + productEndOfLifeDate + "','" + productEndOfSupportDate + "','" + productRepairDate + "','" + productRepairDetails + "','" + productReturnDate + "','" + productRenewalDetails + "','" + teknoaSupportPeriod + "','" + teknoaSupportRenewalDate + "','" + customerRegDate + "','" + teknoaOfferDate + "','" + customerPoDate + "','" + teknoaInvoiceDate + "','"+listPrice+"','"+approvedListPrice+"','" + teknoaUnitSalesPrice + "','"+saleCurrency+"','" + resellerName + "','" + resellerAddress + "','" + resellerBillingDetails + "','" + resellerDeliveryDetails + "','" + customerBillingDetails + "','" + customerDeliveryDetails + "')";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                conn2.Close();
                conn3.Close();
                conn4.Close();
                RefreshProductDatatableAdmin();
                RefreshLogisticsDatatableAdmin();
                MessageBox.Show("Succesfuly added");
            }
            catch(MySql.Data.MySqlClient.MySqlException theException)
            {
                MessageBox.Show("Missing ID!");
            }
            
        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox38.Text;
                string query = "SELECT * FROM product WHERE CONCAT(PRODUCT_ID,CUSTOMER_ID,LOGISTICS_ID,PRODUCT_CODE,PRODUCT_NAME,PRODUCT_QUANTITY,PRODUCT_UNIT_PRICE,PRODUCT_UNIT_COST,CURRENCY,PRODUCT_SERIAL_NO,PRODUCT_ANT_SERIAL_NO,PRODUCT_CONFIGURATION,PRODUCT_WARRANTY_PERIOD,PRODUCT_MANUFACTURER_SUPPORT_PERIOD,PRODUCT_ENDOFLIFE_DATE,PRODUCT_ENDOFSUPPORT_DATE,PRODUCT_REPAIR_DATE,PRODUCT_REPAIR_DETAILS,PRODUCT_RETURN_DATE,PRODUCT_RENEWAL_DETAILS,TEKNOA_SUPPORT_PERIOD,TEKNOA_SUPPORT_RENEWAL_DATE,CUSTOMER_REG_DATE,TEKNOA_OFFER_DATE,CUSTOMER_PO_DATE,TEKNOA_INVOICE_DATE,TEKNOA_UNIT_SALES_PRICE,RESELLER_NAME,RESELLER_ADDRESS,RESELLER_BILLING_DETAILS,RESELLER_DELIVERY_DETAILS,CUSTOMER_BILLING_DETAILS,CUSTOMER_DELIVERY_DETAILS) LIKE '%" + searchedKeyword + "%'";
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

        private void textBox37_TextChanged(object sender, EventArgs e)
        {
            
            try{
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox37.Text;
                string choosedFilter = comboBox4.SelectedItem.ToString();
                string query = "SELECT * FROM product WHERE `" + choosedFilter + "` LIKE '%" + searchedKeyword + "%'";
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

        private void button15_Click(object sender, EventArgs e)
        {
            textBox19.Text = "";
            textBox18.Text = "";
            textBox17.Text = "";
            textBox16.Text = "";
            textBox15.Text = "";
            textBox12.Text = "";
            textBox24.Text = "";
            textBox23.Text = "";
            textBox22.Text = "";
            textBox33.Text = "";
            textBox20.Text = "";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox10.Text = "";
            textBox9.Text = "";
            textBox11.Text = "";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox41.Text = "";
            textBox40.Text = "";
            textBox39.Text = "";
            textBox43.Text = "";
            textBox48.Text = "";
            textBox5.Text = "";
            textBox21.Text = "";
            textBox47.Text = "";
            textBox46.Text = "";
            textBox42.Text = "";
            textBox50.Text = "";
            textBox60.Text = "";
            textBox59.Text = "";
            textBox53.Text = "";
            textBox52.Text = "";
            textBox51.Text = "";
            textBox49.Text = "";
            textBox45.Text = "";
            textBox44.Text = "";
            textBox56.Text = "";
            textBox55.Text = "";
            textBox54.Text = "";
            textBox64.Text = "";
            textBox63.Text = "";
            textBox25.Text = "";
            textBox6.Text = "";
            textBox26.Text = "";
            textBox66.Text = "";
            textBox65.Text = "";
            textBox62.Text = "";
            textBox61.Text = "";
            textBox58.Text = "";
            textBox57.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView2.CurrentCell.RowIndex;
            string selectedRowStr = dataGridView2.Rows[rowIndex].Cells["LOGISTICS_ID"].FormattedValue.ToString();
            int selectedRowId = int.Parse(selectedRowStr);

            MySqlConnection conn = ConnectDatabase();
            conn.Open();
            string query = "DELETE FROM logistics WHERE LOGISTICS_ID = '" + selectedRowId + "'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Succesfuly Deleted");
            RefreshLogisticsDatatableAdmin();
        }

        

        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6 || e.ColumnIndex == 24)
            {
                dataGridView3.Columns[e.ColumnIndex].DefaultCellStyle.BackColor = Color.White;
            }
            if (e.ColumnIndex == 5)
            {
                dataGridView3.Columns[e.ColumnIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }
        

        private void button18_Click(object sender, EventArgs e)
        {
            RefreshLogsDatatableAdmin();
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = ConnectDatabase();
                string searchedKeyword = textBox27.Text;
                string query = "SELECT ((event_time)),((user_host)),((thread_id)),((server_id)),((command_type)),CONVERT((argument) USING utf8) FROM mysql.general_log WHERE CONVERT((argument) USING utf8) != 'SELECT * FROM users' && CONVERT((argument) USING utf8) NOT LIKE '%((event_time))%' && command_type LIKE '%Query%' && (CONVERT((argument) USING utf8) LIKE '%users%' || CONVERT((argument) USING utf8) LIKE '%INSERT%' || CONVERT((argument) USING utf8) LIKE '%DELETE%' || CONVERT((argument) USING utf8) LIKE '%UPDATE%') && CONCAT(((event_time)),((user_host)),((thread_id)),((server_id)),((command_type)),CONVERT((argument) USING utf8)) LIKE '%"+ searchedKeyword + "%'";
                MySqlDataAdapter adp = new MySqlDataAdapter(query, conn);
                conn.Open();
                DataTable dt = new DataTable();
                adp.Fill(dt);
                dataGridView4.DataSource = dt;
                conn.Close();
            }
            catch (MySqlException myException)
            {
                Console.WriteLine(myException);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox20.Text = "0";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["CUSTOMER_ID"].Visible = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["LOGISTICS_ID"].Visible = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_CODE"].Visible = checkBox4.Checked;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_NAME"].Visible = checkBox5.Checked;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_QUANTITY"].Visible = checkBox9.Checked;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_UNIT_PRICE"].Visible = checkBox8.Checked;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["CURRENCY"].Visible = checkBox6.Checked;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_SERIAL_NO"].Visible = checkBox7.Checked;
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_ANT_SERIAL_NO"].Visible = checkBox13.Checked;
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_CONFIGURATION"].Visible = checkBox12.Checked;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_WARRANTY_PERIOD"].Visible = checkBox10.Checked;
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_MANUFACTURER_SUPPORT_PERIOD"].Visible = checkBox11.Checked;
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_ENDOFLIFE_DATE"].Visible = checkBox25.Checked;
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_ENDOFSUPPORT_DATE"].Visible = checkBox24.Checked;
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_REPAIR_DATE"].Visible = checkBox22.Checked;
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_REPAIR_DETAILS"].Visible = checkBox23.Checked;
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_RETURN_DATE"].Visible = checkBox21.Checked;
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["PRODUCT_RENEWAL_DETAILS"].Visible = checkBox20.Checked;
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["TEKNOA_SUPPORT_PERIOD"].Visible = checkBox18.Checked;
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["TEKNOA_SUPPORT_RENEWAL_DATE"].Visible = checkBox19.Checked;
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["CUSTOMER_REG_DATE"].Visible = checkBox17.Checked;
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["TEKNOA_OFFER_DATE"].Visible = checkBox16.Checked;
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["CUSTOMER_PO_DATE"].Visible = checkBox14.Checked;
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["TEKNOA_INVOICE_DATE"].Visible = checkBox15.Checked;
        }

        private void checkBox33_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["APPROVED_LIST_PRICE"].Visible = checkBox33.Checked;
        }

        private void checkBox32_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["TEKNOA_UNIT_SALES_PRICE"].Visible = checkBox32.Checked;
        }

        private void checkBox30_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["RESELLER_NAME"].Visible = checkBox30.Checked;
        }

        private void checkBox31_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["RESELLER_ADDRESS"].Visible = checkBox31.Checked;
        }

        private void checkBox29_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["RESELLER_BILLING_DETAILS"].Visible = checkBox29.Checked;
        }

        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["RESELLER_DELIVERY_DETAILS"].Visible = checkBox28.Checked;
        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["CUSTOMER_BILLING_DETAILS"].Visible = checkBox26.Checked;
        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["CUSTOMER_DELIVERY_DETAILS"].Visible = checkBox27.Checked;
        }

        private void checkBox34_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView3.Columns["SALE_CURRENCY"].Visible = checkBox34.Checked;
        }
    }
}
