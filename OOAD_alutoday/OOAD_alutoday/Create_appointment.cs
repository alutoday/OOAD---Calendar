using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Mysqlx.Session;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Crypto.Fpe;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace OOAD_alutoday
{
    public partial class Create_appointment : Form
    {
        Int32 _id;
        MainUI _mainUI;
        int yy, mm;
        public Create_appointment(Int32 idd,MainUI mainUI,int _yy,int _mm)
        {
            InitializeComponent();            
            _id = idd;
            _mainUI = mainUI;
            yy = _yy;
            mm = _mm;
           
        }
        bool check_information()
        {
            if (textBox1.Text == "" || textBox2.Text == "") return false;
            DateTime st = dateTimePicker1.Value.Date + dateTimePicker2.Value.TimeOfDay;
            DateTime et = dateTimePicker3.Value.Date + dateTimePicker4.Value.TimeOfDay;
            if (st >= et) return false;
            return true;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (check_information())
            {
                try
                {
                  
                    MySqlConnection con = new MySqlConnection("datasource=localhost;port=3307;username=root;password=;database=calendar");
                    con.Open();
                    //check warning
                    DateTime st = dateTimePicker1.Value.Date + dateTimePicker2.Value.TimeOfDay;
                    DateTime et = dateTimePicker3.Value.Date + dateTimePicker4.Value.TimeOfDay;
                    Console.WriteLine(st + "\n" + et+"\n");
                    MySqlCommand cmd = new MySqlCommand("SELECT id,id_user FROM calendar.appointment WHERE ((@st<=start_time AND start_time<@et) OR (@st>start_Time AND @st<end_time)) AND id_user = @id_user", con);
                    cmd.Parameters.AddWithValue("@st", st);
                    cmd.Parameters.AddWithValue("@et", et);
                    cmd.Parameters.AddWithValue("@id_user", _id);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    //replace
                    if (reader.HasRows)
                    {
                        DialogResult rsss = MessageBox.Show("Had appointments in this time! Replace?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (rsss == DialogResult.No)
                        {
                            MessageBox.Show("Please choose another time!", "Message", MessageBoxButtons.OK);
                            reader.Close();
                            return;
                        }

                        List<int> arr_id = new List<int>();
                        while (reader.Read())
                        {
                         arr_id.Add(reader.GetInt32(0));
                        }
                        reader.Close();

                        foreach (int id in arr_id)
                        {
                           /* cmd = new MySqlCommand("delete from group_appointment where id_appointment=@id_appointment AND id_user=@id_user", con);
                            cmd.Parameters.AddWithValue("@id_appointment", id);
                            cmd.Parameters.AddWithValue("@id_user", _id);
                            cmd.ExecuteNonQuery();*/

                            cmd = new MySqlCommand("delete from appointment where id=@id AND id_user=@id_user", con);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@id_user", _id);
                            cmd.ExecuteNonQuery();
                        }
                    } else  reader.Close();


                    //add
                 bool reminder = true;
                 DialogResult rss = MessageBox.Show("Do you want to reminder?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rss == DialogResult.No)
                    {
                            reminder = false;                            
                    }

                    // check group??
                    cmd = new MySqlCommand("SELECT id, id_group FROM calendar.appointment WHERE name=@name AND location=@location AND start_time=@st AND end_time=@et AND id_group is not null", con);
                    cmd.Parameters.AddWithValue("@st", st);
                    cmd.Parameters.AddWithValue("@et", et);
                    cmd.Parameters.AddWithValue("@id_user", _id);
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@location", textBox2.Text);
                    reader = cmd.ExecuteReader();

                  

                    if (reader.HasRows)
                    {
                        DialogResult rsss = MessageBox.Show("This time have the same appointment in meeting group! Do you want to join?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (rsss == DialogResult.Yes)
                        {
                            List<int> arr_id_app = new List<int>();
                            List<int> arr_id_group = new List<int>();
                            if (reader.Read())
                            {
                                arr_id_app.Add(reader.GetInt32(0));
                                arr_id_group.Add(reader.GetInt32(1));
                            }
                            reader.Close();

                            //add gr_app
                            /*cmd = new MySqlCommand("insert into calendar.group_appointment(id,id_user,id_appointment) values (@id,@id_user,@id_appointment)", con);
                            cmd.Parameters.AddWithValue("@id", arr_id_group[0]);
                            cmd.Parameters.AddWithValue("@id_user", _id);
                            cmd.Parameters.AddWithValue("@id_appointment", arr_id_app[0]);
                            cmd.ExecuteNonQuery();*/

                            //add app trung id_app
                            cmd = new MySqlCommand("insert into calendar.appointment(id,id_user,name,location,start_time,end_time,reminder,id_group) " +
                                                                       "values(@id,@id_user,@name,@location,@start_time,@end_time,@reminder,@id_group)", con);
                            cmd.Parameters.AddWithValue("@id", arr_id_app[0]);
                            cmd.Parameters.AddWithValue("@id_user", _id);
                            cmd.Parameters.AddWithValue("@name", textBox1.Text);
                            cmd.Parameters.AddWithValue("@location", textBox2.Text);
                            cmd.Parameters.AddWithValue("@start_time", st);
                            cmd.Parameters.AddWithValue("@end_time", et);
                            cmd.Parameters.AddWithValue("@reminder", reminder);
                            cmd.Parameters.AddWithValue("@id_group", arr_id_group[0]);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Save sucessful!");
                            _mainUI.Display(yy,mm);
                            this.Close();
                            con.Close();
                            return;
                        }

                    }
                    reader.Close();
                    
                    DialogResult rs = MessageBox.Show("Do you want to create a meeting group?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //add app
                    if(rs== DialogResult.No)
                    {
                        
                       cmd = new MySqlCommand("insert into calendar.appointment(id_user,name,location,start_time,end_time,reminder) " +
                                                                            "values(@id_user,@name,@location,@start_time,@end_time,@reminder)", con);
                        cmd.Parameters.AddWithValue("@id_user", _id);
                        cmd.Parameters.AddWithValue("@name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@location", textBox2.Text);
                        cmd.Parameters.AddWithValue("@start_time", st);
                        cmd.Parameters.AddWithValue("@end_time", et);
                        cmd.Parameters.AddWithValue("@reminder", reminder);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Save sucessful!");

                    }
                    //add app+ gr_app( sql id_app)
                    else
                    {   
                        //kiem id_gr random
                        int id_gr = -1;
                        object result;
                        do
                        {
                            Random random = new Random();
                            id_gr = random.Next(0, 100000);
                            cmd = new MySqlCommand("SELECT id_group FROM calendar.appointment WHERE id_group= @id_group", con);
                            cmd.Parameters.AddWithValue("@id_group", id_gr);
                            result = cmd.ExecuteScalar();
                        }
                        while (result != null);
                        

                        //add app voi id_gr
                        cmd = new MySqlCommand("insert into calendar.appointment(id_user,name,location,start_time,end_time,reminder,id_group) " +
                                                                           "values(@id_user,@name,@location,@start_time,@end_time,@reminder,@id_group)", con);
                        cmd.Parameters.AddWithValue("@id_user", _id);
                        cmd.Parameters.AddWithValue("@name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@location", textBox2.Text);
                        cmd.Parameters.AddWithValue("@start_time", st);
                        cmd.Parameters.AddWithValue("@end_time", et);
                        cmd.Parameters.AddWithValue("@reminder", reminder);
                        cmd.Parameters.AddWithValue("@id_group", id_gr);
                        cmd.ExecuteNonQuery();

                       /* //lay id_app
                        cmd = new MySqlCommand("select id from calendar.appointment where id_user=@id_user AND id_group=@id_group", con);
                        cmd.Parameters.AddWithValue("@id_user", _id);
                        cmd.Parameters.AddWithValue("@id_group", id_gr);
                        reader= cmd.ExecuteReader();
                        int id_app = -1;
                        if(reader.Read())
                        {
                            id_app = reader.GetInt32(0);

                        }
                        reader.Close();

                        //add vao gr_app
                        cmd = new MySqlCommand("insert into calendar.group_appointment(id,id_user,id_appointment) values (@id,@id_user,@id_appointment)", con);
                        cmd.Parameters.AddWithValue("@id", id_gr);
                        cmd.Parameters.AddWithValue("@id_user", _id);
                        cmd.Parameters.AddWithValue("@id_appointment",id_app);
                        cmd.ExecuteNonQuery();
                       */
                        MessageBox.Show("Save sucessful!");
                    }

                    _mainUI.Display(yy, mm);
                    con.Close();                    
                    this.Close();

                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex); }
            }
            else
            {
                MessageBox.Show("Fail Information");
            }
        }
    }
 }

