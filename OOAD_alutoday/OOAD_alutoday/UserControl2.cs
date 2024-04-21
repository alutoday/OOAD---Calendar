using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOAD_alutoday
{
    public partial class UserControl2 : UserControl
    {
        public DateTime _dt;
        public Int32 _id;
        public UserControl2(Int32 idd, DateTime dt)
        {
            InitializeComponent();
            _id = idd;
             _dt = dt;
        }
        public void days(int i)
        {
            label1.Text = i.ToString();
            label2.Text = "";
        }
        public void add_appointment(string name,DateTime st)
        {
            label2.Text=label2.Text+name+" ("+st.Hour.ToString()+": "+st.Minute.ToString()+")\n";
            
        }

        

        private void UserControl2_Click(object sender, EventArgs e)
        {
            Detail detail = new Detail(_dt, _id);
            detail.Show();
        }
    }
}
