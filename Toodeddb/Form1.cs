using Aspose.Pdf.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Toodeddb
{
    public partial class Form1 : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane.TTHK\source\repos\Lastovski_TARpv21\Toodeddb\Toodeddb\AppData\Tooded_DB.mdf;Integrated Security=True");
        SqlDataAdapter adapter_toode, adapter_kategooria;
        SqlCommand cmd, cmd2;
        public Form1()
        {
            InitializeComponent();
            Naita_Andmed();
        }
        public void Naita_Andmed() 
        {
            connect.Open(); 
            DataTable dt_toode = new DataTable();
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
            kogustxt.Value = 0;
            hindtxt.Value = 0;
            comboBox1.ResetText();
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
                //connect.Close();
                //cmd = new SqlCommand("SELECT Pilt FROM Toodetable WHERE Toodenimetus=@toodenimetus", connect);
                //connect.Open();
                //cmd.Parameters.AddWithValue("@toodenimetus", toodedtxt.Text);
                //object result = cmd.ExecuteScalar();
                //connect.Close();
                string path = pictureBox1.ImageLocation;
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                    pictureBox1.ImageLocation = null;
                    File.Delete(path);

                }
                pictureBox1.ImageLocation = @"../../../question.png";
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
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
            kustuta_andmed();
            Naita_kat();
        }
        private void otsi_btn_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\Users\opilane.TTHK\Pictures";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Image img = Image.FromFile(openFileDialog1.FileName);
                //Bitmap finalimg = new Bitmap(img, pictureBox1.Width, pictureBox1.Height);
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                //pictureBox1.Image = finalimg;
                //pictureBox1.Load(openFileDialog1.FileName);
                //Bitmap finalImg = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height); 
                //pictureBox1.Image = finalImg;
                //pictureBox1.Show();

            }
        }
        int Id;
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try 
            {
                comboBox1.Text = null;
                Id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                toodedtxt.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                kogustxt.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                hindtxt.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                Image img = null;
                string path = "";
                try
                {
                    path = @"..\..\pictures\" + dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    img = Image.FromFile(path);
                }
                catch
                {
                    MessageBox.Show("Fail puudub");
                    path = @"..\..\..\question.png";
                    img = Image.FromFile(path);
                }
                Bitmap finalImg = new Bitmap(img, pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = finalImg;
                pictureBox1.Show();
                int v = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                cmd = new SqlCommand("SELECT [Kategooria_nimetus] FROM [Kategooriatable] WHERE [Id]=@id", connect);
                connect.Open();
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = v;
                comboBox1.SelectedItem = cmd.ExecuteScalar().ToString();
                connect.Close();
                pictureBox1.ImageLocation = path;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            }
            catch 
            {
                MessageBox.Show("Vali toode!");
            }
            
        }

        private void Uuendabtn_Click(object sender, EventArgs e) 
        {
            if (toodedtxt.Text.Trim() != string.Empty && kogustxt.Text.Trim() != string.Empty && hindtxt.Text.Trim() != string.Empty && comboBox1.Text != null)
            {
                try
                {
                    string path = pictureBox1.ImageLocation;
                    FileInfo fi = new FileInfo(path);
                    string extn = fi.Extension;
                    cmd = new SqlCommand("UPDATE Toodetable SET Toodenimetus = @toode, Kogus = @kogus, Hind = @hind, kategooria_id = (SELECT [Id] FROM [Kategooriatable] WHERE [kategooria_nimetus] = @kat), Pilt = @pilt WHERE Id = @id", connect);
                    connect.Open();
                    cmd.Parameters.AddWithValue("@id", Id);
                    cmd.Parameters.AddWithValue("@toode", toodedtxt.Text);
                    cmd.Parameters.AddWithValue("@hind", hindtxt.Text.Replace(",", "."));
                    cmd.Parameters.AddWithValue("@kogus", kogustxt.Text);
                    cmd.Parameters.AddWithValue("@pilt", toodedtxt.Text + extn);
                    cmd.Parameters.AddWithValue("@kat", comboBox1.Items[comboBox1.SelectedIndex].ToString());
                    cmd.ExecuteNonQuery();
                    connect.Close();
                    string destinationFile = @"..\..\pictures\" + toodedtxt.Text + extn; 
                    File.Copy(fi.FullName, destinationFile);
                    kustuta_andmed();
                    Naita_Andmed();
                }
                catch (Exception)
                {
                    MessageBox.Show("..__..__..__..__", "ERORR");
                }
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
                        "VALUES (@toode, @kogus, @hind, @pilt, (SELECT [Id] FROM [Kategooriatable] WHERE [kategooria_nimetus]=@kat))", connect);
                    cmd2 = new SqlCommand("SELECT Toodenimetus FROM Toodetable WHERE Toodenimetus = @toodedtxt;", connect);
                    connect.Open();
                    cmd2.Parameters.AddWithValue("@toodedtxt", toodedtxt.Text);
                    object result = cmd2.ExecuteScalar();
                    if (result == null)
                    {
                        cmd.Parameters.AddWithValue("@toode", toodedtxt.Text);
                        cmd.Parameters.AddWithValue("@hind", hindtxt.Text.Replace(",", "."));
                        cmd.Parameters.AddWithValue("@kogus", kogustxt.Text);
                        cmd.Parameters.AddWithValue("@pilt", toodedtxt.Text + extn);
                        cmd.Parameters.AddWithValue("@kat", comboBox1.Items[comboBox1.SelectedIndex].ToString());
                        cmd.ExecuteNonQuery();
                        string destinationFile;
                        try
                        {
                            destinationFile = @"..\..\pictures\" + toodedtxt.Text + extn;
                            Image img = Image.FromFile(openFileDialog1.FileName);
                            Bitmap finalImg = new Bitmap(img, pictureBox1.Width, pictureBox1.Height);
                            finalImg.Save(destinationFile);
                        }
                        catch { }

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
    }
}
