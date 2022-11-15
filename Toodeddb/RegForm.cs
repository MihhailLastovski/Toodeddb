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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Toodeddb
{
    public partial class RegForm : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane.TTHK\source\repos\Lastovski_TARpv21\Toodeddb\Toodeddb\AppData\Tooded_DB.mdf;Integrated Security=True");
        SqlCommand cmd;
        public RegForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null)
            {
                cmd = new SqlCommand("INSERT INTO Roles (Login, Password, Role) " +
                    "VALUES (@login, @password, @role)", connect);
                connect.Open();
                cmd.Parameters.AddWithValue("@login", textBox1.Text);
                cmd.Parameters.AddWithValue("@password", textBox2.Text);
                if (checkBox1.Checked) {   cmd.Parameters.AddWithValue("@role", "Omanik");   }
                else { cmd.Parameters.AddWithValue("@role", "Muuja"); }
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            this.Close();

        }
    }
}
