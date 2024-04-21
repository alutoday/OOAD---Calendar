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

namespace OOAD_alutoday
{
    public partial class Detail : Form
    {
        DateTime _dt;
        Int32 _id;
        public Detail(DateTime dt, Int32 idd)
        {
            InitializeComponent();
            _dt = dt;
            _id = idd;
            sql_data();
           
        }
        void sql_data()
        {
            MySqlConnection con = new MySqlConnection("datasource=localhost;port=3307;username=root;password=;database=calendar");
            try
            {
                using (MySqlCommand cmd1 = new MySqlCommand("SELECT name,location,start_time,end_time FROM calendar.appointment WHERE id_user = @id_user AND DATE(start_time)=DATE(@dt) ", con))
                {
                    cmd1.Parameters.AddWithValue("@id_user", _id);
                    cmd1.Parameters.AddWithValue("@dt", _dt);

                    con.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd1))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dt.DefaultView.Sort = "start_time ASC";
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();

            }
        }
    }
}
