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
        SqlDataAdapter adapter_kaart;
        SqlCommand cmd, cmd2;
        DataGridView dr;
        ComboBox comboBox1;

        public Admin()
        {
            InitializeComponent();
            Naita_Andmed();
        }
        public void Naita_Andmed()
        {
            dr = new DataGridView();
            dr.Location = new Point(121, 185);
            dr.Size = new Size(544, 157);
            dr.TabIndex = 35;
            Update_Table();
            dr.RowHeaderMouseClick += Dr_RowHeaderMouseClick;
            this.Controls.Add(dr);
            comboBox1 = new ComboBox();
            comboBox1.Font = new Font("Microsoft Sans Serif", 8.25F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(429, 64);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(142, 21);
            comboBox1.TabIndex = 22;
            this.Controls.Add(comboBox1);
            Naita_kaart();
            txt_tel.MaxLength = 13;
            txt_isik.MaxLength = 11;
        }
        private void Update_Table() 
        {
            connect.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT nimi, tel, isikukood, bonuspunktid, kliendikaart = (SELECT kliendikaart FROM Kliendikaart WHERE id = kliendikaartID) FROM Kliendid", connect);
            DataSet ds = new DataSet();
            da.Fill(ds, "Kliendid");
            dr.DataSource = ds.Tables["Kliendid"].DefaultView;
            connect.Close();
        }

        private void Dr_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try 
            {
                comboBox1.Text = null;
                txt_name.Text = dr.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_bonus.Text = dr.Rows[e.RowIndex].Cells[3].Value.ToString();
                txt_isik.Text = dr.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_tel.Text = dr.Rows[e.RowIndex].Cells[1].Value.ToString();
                cmd2 = new SqlCommand("SELECT kliendikaartID FROM Kliendid WHERE isikukood = '" + txt_isik.Text + "'", connect);
                connect.Open();
                object v = cmd2.ExecuteScalar();
                cmd = new SqlCommand("SELECT [kliendikaart] FROM [Kliendikaart] WHERE [id]=@id", connect);
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = v;
                comboBox1.SelectedItem = cmd.ExecuteScalar().ToString();
                connect.Close();
            }
            catch 
            {
                MessageBox.Show("Vali andmed!");
            }
        }

        private void Naita_kaart()
        {
            adapter_kaart = new SqlDataAdapter("SELECT kliendikaart FROM [Kliendikaart]", connect);
            DataTable dt_kategooria = new DataTable();
            adapter_kaart.Fill(dt_kategooria);
            foreach (DataRow nimetus in dt_kategooria.Rows)
            {
                comboBox1.Items.Add(nimetus["kliendikaart"].ToString());
            }
        }

        private void Lisabtn_Click_1(object sender, EventArgs e)
        {
            if (txt_name.Text.Trim() != string.Empty && txt_isik.Text.Trim() != string.Empty)
            {
                try
                {
                    cmd = new SqlCommand("INSERT INTO Kliendid (nimi, tel, isikukood, bonuspunktid, kliendikaartID) " +
                        "VALUES (@nimi, @tel, @isik, @bonuspunkt, (SELECT [Id] FROM [Kliendikaart] WHERE [kliendikaart]=@kaart))", connect);
                    cmd2 = new SqlCommand("SELECT isikukood FROM Kliendid WHERE isikukood = @txt_isik;", connect);
                    connect.Open();
                    cmd2.Parameters.AddWithValue("@txt_isik", txt_isik.Text);
                    object result = cmd2.ExecuteScalar();
                    if (result == null)
                    {
                        cmd.Parameters.AddWithValue("@nimi", txt_name.Text);
                        cmd.Parameters.AddWithValue("@tel", txt_tel.Text);
                        cmd.Parameters.AddWithValue("@isik", txt_isik.Text);
                        cmd.Parameters.AddWithValue("@bonuspunkt", 0);
                        cmd.Parameters.AddWithValue("@kaart", comboBox1.Items[comboBox1.SelectedIndex].ToString());
                        cmd.ExecuteNonQuery();

                    }
                    connect.Close();
                    kustuta_andmed();
                    Update_Table();
                }
                catch (Exception)
                {
                    MessageBox.Show("..__..__..__..__", "ERORR");
                }
            }
        }

        private void Kustutabtn_Click(object sender, EventArgs e)
        {
            connect.Open();

            if (MessageBox.Show("Kas olete kindel, et soovite kliendi kustutada?", "Kustutamine", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dr.SelectedRows.Count == 0)
                {
                    connect.Close();
                    return;
                }
                cmd2 = new SqlCommand("DELETE FROM Kliendid WHERE isikukood = '" + txt_isik.Text + "'", connect);     
                cmd2.ExecuteNonQuery();
            }
            else { }
            connect.Close();
            kustuta_andmed();
            Update_Table();
        }

        private void Uuendabtn_Click(object sender, EventArgs e)
        {
            if (txt_isik.Text.Trim() != string.Empty && txt_name.Text.Trim() != string.Empty && txt_tel.Text.Trim() != string.Empty && comboBox1.Text != null)
            {
                    int selectedIndex = dr.SelectedRows[0].Index;
                    string isik = dr[2, selectedIndex].Value.ToString();
                    cmd = new SqlCommand("UPDATE Kliendid SET nimi = @nimi, tel = @tel, isikukood = @isikukood, kliendikaartID = (SELECT [id] FROM [Kliendikaart] WHERE [kliendikaart] = @kart) WHERE isikukood = @isik", connect);
                    connect.Open();
                    cmd.Parameters.AddWithValue("@isik", isik);
                    cmd.Parameters.AddWithValue("@nimi", txt_name.Text);
                    cmd.Parameters.AddWithValue("@tel", txt_tel.Text);
                    cmd.Parameters.AddWithValue("@isikukood", txt_isik.Text);
                    cmd.Parameters.AddWithValue("@kart", comboBox1.Items[comboBox1.SelectedIndex].ToString());
                    cmd.ExecuteNonQuery();
                    connect.Close();
                    kustuta_andmed();
                    Update_Table();
            }
        }

        public void kustuta_andmed()
        {
            txt_name.Text = "";
            txt_isik.Text = "";
            txt_bonus.Text = "";
            txt_tel.Text = "+372";
            comboBox1.ResetText();
        }
    }
}
