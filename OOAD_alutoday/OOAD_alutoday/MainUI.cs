using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OOAD_alutoday
{ 
    public partial class MainUI : Form
    {
        public static int year { get; private set; }
        public static int month { get; private set; }
        Int32 _id;
        String _username;
        Login _login;
        public MainUI(Int32 idd, String usernamee, Login login)
        {
            InitializeComponent();
            _id = idd;
            _username = usernamee;
            _login = login;
            time_now();
        }
        private void MainUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            _login.Show();
        }
        public void Display(int yy, int mm)
        {
            flowLayoutPanel1.Controls.Clear();
            DateTime start_month = new DateTime(yy, mm, 1);
            int days = DateTime.DaysInMonth(yy, mm);
            int day_first_week = Convert.ToInt32(start_month.DayOfWeek.ToString("d"));
            for (int i = 1; i < day_first_week; i++)
            {
                UserControl1 uc = new UserControl1();
                flowLayoutPanel1.Controls.Add(uc);
            }
            MySqlConnection con = new MySqlConnection("datasource=localhost;port=3307;username=root;password=;database=calendar");
            con.Open();
            MySqlCommand cmd;
            DateTime now = new DateTime(yy,mm,1);      
           

            now.AddMonths(mm);
            for (int i = 1; i <= days; i++)
            {
                UserControl2 uc = new UserControl2(_id,now);
                uc.days(i);
                flowLayoutPanel1.Controls.Add(uc);
                
                //Console.WriteLine(now.ToString());
                cmd= new MySqlCommand("select name,start_time from calendar.appointment where id_user = @id_user AND DATE(start_time)=DATE(@now)", con);
                cmd.Parameters.AddWithValue("@id_user",_id);
                cmd.Parameters.AddWithValue("@now", now);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                String name;
                DateTime st;
               while(mySqlDataReader.Read())
                {
                    name= mySqlDataReader.GetString(0);
                    st = mySqlDataReader.GetDateTime(1);
                    uc.add_appointment(name, st);
                }              
                mySqlDataReader.Close();
                now = now.AddDays(1);

            }
            con.Close();

        }
      
        private void button1_Click(object sender, EventArgs e)
        {            
            month--;
            if (month == 0) { month = 12; year--; }
            label4.Text = month.ToString() + "/" + year.ToString();
            Display(year, month);
        }
        
        private void button2_Click(object sender, EventArgs e)
        {            
            month++;
            if (month == 13) { month = 1; year++; }
            label4.Text = month.ToString() + "/" + year.ToString();
            Display(year, month);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Create_appointment ca = new Create_appointment(_id,this,year,month);
            ca.ShowDialog();                   
                
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Appointment_manager appointment_Manager = new Appointment_manager(_id);
            appointment_Manager.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Reminder_manager reminder_Manager = new Reminder_manager(_id);
            reminder_Manager.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Group_meeting_manager group_Meeting_Manager=new Group_meeting_manager(_id);
            group_Meeting_Manager.ShowDialog();
        }

        void time_now()
        {
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            label2.Text = "Username :" + _username;
            label3.Text = "Today: " + DateTime.Now.ToString("d");
            label4.Text = month.ToString() + "/" + year.ToString();
            Display(year, month);
        }
    }
}
