using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toodeddb
{
    public partial class Admin : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane.TTHK\source\repos\Lastovski_TARpv21\Toodeddb\Toodeddb\AppData\Tooded_DB.mdf;Integrated Security=True");
        SqlDataAdapter adapter_toode, adapter_kategooria;
        SqlCommand cmd, cmd2;

        public Admin()
        {
            Naita_Andmed();
            InitializeComponent();
        }
        public void Naita_Andmed()
        {
            connect.Open();
            DataTable dt_toode = new DataTable();
            adapter_toode = new SqlDataAdapter("SELECT * FROM Kliendid", connect);
            adapter_toode.Fill(dt_toode);
            Naita_kat();
            connect.Close();
        }
        private void Naita_kat()
        {
            comboBox1.SelectedIndex = 0;
            adapter_kategooria = new SqlDataAdapter("SELECT kliendikaart FROM [Kliendikaart]", connect);
            DataTable dt_kategooria = new DataTable();
            adapter_kategooria.Fill(dt_kategooria);
            foreach (DataRow nimetus in dt_kategooria.Rows)
            {
                comboBox1.Items.Add(nimetus["kliendikaart"].ToString());
            }
        }
        private void Lisabtn_Click(object sender, EventArgs e)
        {
            if (txt_name.Text.Trim() != string.Empty && txt_isik.Text.Trim() != string.Empty)
            {
                try
                {
                    cmd = new SqlCommand("INSERT INTO Kliendid (nimi, tel, isikukood, kliendikaartID) " +
                        "VALUES (@nimi, @tel, @isik, (SELECT [Id] FROM [Kliendikaart] WHERE [kliendikaart]=@kaart))", connect);
                    cmd2 = new SqlCommand("SELECT isikukood FROM Kliendid WHERE isikukood = @txt_isik;", connect);
                    connect.Open();
                    cmd2.Parameters.AddWithValue("@txt_isik", txt_isik.Text);
                    object result = cmd2.ExecuteScalar();
                    if (result == null)
                    {
                        cmd.Parameters.AddWithValue("@nimi", txt_name.Text);
                        cmd.Parameters.AddWithValue("@tel", txt_tel.Text);
                        cmd.Parameters.AddWithValue("@isik", txt_isik.Text);
                        cmd.Parameters.AddWithValue("@kaart", comboBox1.Items[comboBox1.SelectedIndex].ToString());
                        cmd.ExecuteNonQuery();

                    }
                    connect.Close();
                    kustuta_andmed();
                    Naita_Andmed();
                }
                catch (Exception)
                {
                    MessageBox.Show("..__..__..__..__", "ERORR");
                }
            }
        }
        public void kustuta_andmed()
        {
            txt_name.Text = "";
            txt_isik.Text = "";
            txt_bonus.Text = "";
            comboBox1.ResetText();
        }
    }
}
