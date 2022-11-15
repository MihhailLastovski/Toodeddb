using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Toodeddb
{
    public partial class LogInForm : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane.TTHK\source\repos\Lastovski_TARpv21\Toodeddb\Toodeddb\AppData\Tooded_DB.mdf;Integrated Security=True");
        SqlCommand cmd;
        public LogInForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new RegForm().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null)
            {
                object result;
                cmd = new SqlCommand("SELECT Role FROM Roles WHERE Login = @login and Password = @password;", connect);
                connect.Open();
                cmd.Parameters.AddWithValue("@login", textBox1.Text);
                cmd.Parameters.AddWithValue("@password", textBox2.Text);
                result = cmd.ExecuteScalar();
                connect.Close();
                if (result != null)
                {
                    new OpenForm(result.ToString()).ShowDialog();
                    this.Close();
                }
            }
        }
    }
}
