using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;





namespace TeknoA_Database
{

    

    public partial class LoginScreen : Form
    {


        public bool isUserAdmin;
        public string loggedInUsername;
        public string loggedInPassword;

        public LoginScreen()
        {

            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }

 

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetTextboxes();
        }

        private void ResetTextboxes()
        {
            usernameTxtbox.Text = "";
            passwordTxtbox.Text = "";
        }

        private void SendWarningMsg(string msg,string title)
        {
            MessageBox.Show(msg,title,MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

      

        private void loginButton_Click(object sender, EventArgs e)
        {
            CheckUsernamePassword();

        }

        private void passwordTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                CheckUsernamePassword();
            }

        }

        private void OpenDatabaseScreen()
        {
            DatabaseScreen dbs = new DatabaseScreen();
            this.Hide();
            dbs.ShowDialog();
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


        private void CheckUsernamePassword()
        {
            MySqlConnection conn = ConnectDatabase();
            conn.Open();

            string query = "select * from users WHERE uname = '" + usernameTxtbox.Text + "' AND upassword = '"+passwordTxtbox.Text+"'";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                loggedInUsername = reader["uname"].ToString();
                loggedInPassword = reader["upassword"].ToString();
                string isAdminStr = reader["isAdmin"].ToString();
                if (isAdminStr == "True")
                {
                    isUserAdmin = true;
                }
                else
                {
                    isUserAdmin = false;
                }
                OpenDatabaseScreen();
               
            }
            else
            {
                ResetTextboxes();
                SendWarningMsg("Username or Password Incorrect", "Error!");
            }

            conn.Close();

        }

        private void usernameTxtbox_Enter(object sender, EventArgs e)
        {
            if (usernameTxtbox.Text == "Username")
            {
                usernameTxtbox.Text = "";
                usernameTxtbox.ForeColor = Color.Black;
            }
        }

        private void usernameTxtbox_Leave(object sender, EventArgs e)
        {
            if (usernameTxtbox.Text == "")
            {
                usernameTxtbox.Text = "Username";
                usernameTxtbox.ForeColor = Color.Silver;
            }
        }

        private void passwordTxtbox_Enter(object sender, EventArgs e)
        {
            if (passwordTxtbox.Text == "Password")
            {
                passwordTxtbox.Text = "";
                passwordTxtbox.ForeColor = Color.Black;
            }
        }

        private void passwordTxtbox_Leave(object sender, EventArgs e)
        {
            if (passwordTxtbox.Text == "")
            {
                passwordTxtbox.Text = "Password";
                passwordTxtbox.ForeColor = Color.Silver;
            }
        }
    }
}
