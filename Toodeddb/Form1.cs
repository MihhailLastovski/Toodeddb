using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Toodeddb
{
    public partial class Form1 : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\source\repos\Lastovski_TARpv21\Toodeddb\Toodeddb\AppData\Tooded_DB.mdf;Integrated Security=True");
        SqlDataAdapter adapter_toode, adapter_kategooria;
        SqlCommand cmd;

        public Form1()
        {
            InitializeComponent();
            Naita_Andmed();
        }
        public void Naita_Andmed() 
        {
            connect.Open(); 
            DataTable dt_toode = new DataTable();
            //cmd = new SqlCommand("SELECT * FROM Toodetable");
            adapter_toode = new SqlDataAdapter("SELECT * FROM Toodetable", connect);
            adapter_toode.Fill(dt_toode);
            dataGridView1.DataSource = dt_toode;
            Image img = Image.FromFile("../../../question.png");
            Bitmap finalimg = new Bitmap(img, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = finalimg;
            Naita_kat();
            connect.Close();
        }

        private void lisaUusKategooria(object sender, EventArgs e)
        {
            connect.Open();
            kustuta_andmed();
            cmd = new SqlCommand("INSERT INTO Kategooriatable (Kategooria_nimetus) VALUES (@kat)", connect);
            cmd.Parameters.AddWithValue("@kat", comboBox1.Text);
            cmd.ExecuteNonQuery();
            Naita_kat();
            connect.Close();
        }
        private void Naita_kat() 
        {
            adapter_kategooria = new SqlDataAdapter("SELECT Kategooria_nimetus FROM Kategooriatable", connect);
            DataTable dt_kategooria = new DataTable();
            adapter_kategooria.Fill(dt_kategooria);
            foreach (DataRow nimetus in dt_kategooria.Rows)
            {
                comboBox1.Items.Add(nimetus["Kategooria_nimetus"].ToString());
            }
        }
        public void kustuta_andmed() 
        {
            toodedtxt.Text = "";
            kogustxt.Text = "";
            hindtxt.Text = "";
            comboBox1.Items.Clear();
        }

        private void Kustutabtn_Click(object sender, EventArgs e)
        {
            connect.Open();

            if (MessageBox.Show("Toode - Jah / Kategooria - Ei", "Mida soovite kustutada?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dataGridView1.SelectedRows.Count == 0) 
                { 
                    connect.Close();
                    return;
                }

                string sql = "DELETE FROM Toodetable WHERE Id = @rowID";

                using (SqlCommand deleteRecord = new SqlCommand(sql, connect))
                {
                    int selectedIndex = dataGridView1.SelectedRows[0].Index;
                    int rowID = Convert.ToInt32(dataGridView1[0, selectedIndex].Value);

                    deleteRecord.Parameters.AddWithValue("@rowID", rowID);
                    deleteRecord.ExecuteNonQuery();

                    dataGridView1.Rows.RemoveAt(selectedIndex);
                }
            }
            else 
            {
                if (comboBox1.Text == "") 
                {
                    connect.Close();
                    return;
                }

                string sql = "DELETE FROM Kategooriatable WHERE Kategooria_nimetus = @kat";

                using (SqlCommand deleteRecord = new SqlCommand(sql, connect))
                {
                    deleteRecord.Parameters.AddWithValue("@kat", comboBox1.Text);
                    deleteRecord.ExecuteNonQuery();      
                }
            }
            connect.Close();
        }

        private void otsi_btn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ext = Path.GetExtension(openFileDialog1.FileName);
                pictureBox1.Load(openFileDialog1.FileName);
                Bitmap finalImg = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height); //Venitab pilti
                pictureBox1.Image = finalImg;
                pictureBox1.Show();
                string destinationFile = @"..\..\pictures\" + toodedtxt.Text + ext;
                File.Copy(openFileDialog1.FileName, destinationFile);
            }
        }


        private void Lisabtn_Click(object sender, EventArgs e)
        {
            if (toodedtxt.Text.Trim() != string.Empty && kogustxt.Text.Trim() != string.Empty && hindtxt.Text.Trim() != string.Empty && comboBox1.SelectedItem != null)
            {
                try
                {
                    string path = pictureBox1.ImageLocation;
                    FileInfo fi = new FileInfo(path);
                    string extn = fi.Extension;
                    cmd = new SqlCommand("INSERT INTO Toodetable (Toodenimetus, Kogus, Hind, Pilt, Kategooria_Id) " +
                        "VALUES (@toode, @kogus, @hind, @pilt, @kat)", connect);
                    connect.Open();
                    cmd.Parameters.AddWithValue("@toode", toodedtxt.Text);
                    cmd.Parameters.AddWithValue("@hind", hindtxt.Text);
                    cmd.Parameters.AddWithValue("@kogus", kogustxt.Text);
                    cmd.Parameters.AddWithValue("@pilt", toodedtxt.Text + extn);
                    cmd.Parameters.AddWithValue("@kat", comboBox1.SelectedIndex + 1);
                    cmd.ExecuteNonQuery();
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
    }
}
