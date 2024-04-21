using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace OOAD_alutoday
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == null)
            {
                MessageBox.Show("Fail Information!");
            }
            else
            {
                DB_query();
            }

        }

        public void DB_query()
        {
            try
            {
               
                MySqlConnection con = new MySqlConnection("datasource=localhost;port=3307;username=root;password=;database=calendar");
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select id from calendar.user where username = @username", con);
                cmd.Parameters.AddWithValue("@username", textBox1.Text);                
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();                
                Int32 id_para=-1;              
               
                if (mySqlDataReader.HasRows)
                {
                    if (mySqlDataReader.Read())  id_para = mySqlDataReader.GetInt32(0);
                    MessageBox.Show("Sign in successfully");

                }
                else
                {
       
                    mySqlDataReader.Close();
                    cmd = new MySqlCommand("insert into calendar.user (username) values(@username)", con);
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sign up successfully");                   

                    cmd = new MySqlCommand("select id from calendar.user where username = @username", con);
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    
                    mySqlDataReader = cmd.ExecuteReader();                    
                    if (mySqlDataReader.Read()) id_para = mySqlDataReader.GetInt32(0);
                    mySqlDataReader.Close();


                }
                con.Close();
                MainUI mainUI = new MainUI(id_para, textBox1.Text,this);
                this.Hide();
                mainUI.ShowDialog();               

            }
            catch (Exception ex) { MessageBox.Show("Error " + ex); }



        }
    }
}
