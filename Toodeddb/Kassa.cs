using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Page = Aspose.Pdf.Page;
using Image = System.Drawing.Image;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;
using System.Runtime.InteropServices.ComTypes;
using Path = System.IO.Path;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Toodeddb
{
    public partial class Kassa : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lasto\source\repos\Toodeddb\Toodeddb\AppData\Tooded_DB.mdf;Integrated Security=True");
        SqlDataAdapter adapter_toode, adapter_kat;
        SqlCommand cmd, cmd2, cmd3;
        SqlDataReader reader;
        TabControl kategooriad;
        DataGridView dataGrid;
        public Kassa()
        {
            Kategooriad();
            InitializeComponent();
            Naita_Andmed();
        }
        int kat_Id;
        public void Kategooriad()
        {
            kategooriad = new TabControl();
            kategooriad.Name = "Kategooriad";
            kategooriad.Width = 891;
            kategooriad.Height = 313;
            connect.Open();
            adapter_kat = new SqlDataAdapter("SELECT Id, Kategooria_nimetus FROM Kategooriatable", connect);
            DataTable dt_kat = new DataTable();
            adapter_kat.Fill(dt_kat);
            ImageList iconsList = new ImageList();//
            iconsList.ColorDepth = ColorDepth.Depth32Bit;//
            iconsList.ImageSize = new Size(25, 25);//

            int i = 0;
            connect.Close();
            foreach (DataRow nimetus in dt_kat.Rows)
            {
                kategooriad.TabPages.Add((string)nimetus["Kategooria_nimetus"]);
                iconsList.Images.Add(Image.FromFile(@"..\..\katimg\" + (string)nimetus["Kategooria_nimetus"] + ".jpg"));
                kategooriad.TabPages[i].ImageIndex = i;//
                i++;
                kat_Id = (int)nimetus["Id"];
                dataGrid = new DataGridView
                {
                    Width = 852,
                    Height = 285,
                    RowHeadersVisible = false,
                    ColumnHeadersVisible = false
                };
                kategooriad.TabPages[i - 1].Controls.Add(dataGrid);
                dataGrid.CellClick += DataGrid_CellClick;
                connect.Open();
                cmd = new SqlCommand("SELECT id, Pilt FROM Toodetable WHERE Kategooria_id = "+kat_Id, connect);
                reader = cmd.ExecuteReader();
                List<string> name_pilt = new List<string>();
                List<int> toode_id = new List<int>();
                while (reader.Read())
                {
                    name_pilt.Add(reader["Pilt"].ToString());
                    toode_id.Add((int)reader["id"]);
                }
                for (int j = 0; j < name_pilt.Count; j++)
                {
                    Image img = Image.FromFile(@"..\..\pictures\" + name_pilt[j]);
                    DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
                    iconColumn.Width = 100;
                    iconColumn.Image = img;
                    iconColumn.Name = "Icon_name" + j;
                    iconColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                    dataGrid.Columns.Add(iconColumn);
                    dataGrid.Rows[dataGrid.NewRowIndex].Cells["Icon_name"+j].Value = img;
                    dataGrid.Rows[dataGrid.NewRowIndex].Cells["Icon_name" + j].Tag = toode_id[j];
                    dataGrid.Rows[dataGrid.NewRowIndex].Cells["Icon_name" + j].ToolTipText = (string)nimetus["Kategooria_nimetus"];
                    dataGrid.Rows[dataGrid.NewRowIndex].Height = 80;
                }
                connect.Close();
            }
            kategooriad.ImageList = iconsList;
            this.Controls.Add(kategooriad);
        }

        private void DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView data_grid = (DataGridView)sender;
            try
            {
                connect.Open();
                cmd3 = new SqlCommand("SELECT Toodenimetus, Kogus, Hind FROM Toodetable WHERE id = " + data_grid.CurrentCell.Tag, connect);
                reader = cmd3.ExecuteReader();
                while (reader.Read())
                {
                    label1.Text = reader["Toodenimetus"].ToString();
                    label2.Text = reader["Kogus"].ToString();
                    label3.Text = reader["Hind"].ToString();
                }
                label4.Text = data_grid.CurrentCell.ToolTipText;
                connect.Close();
                numericUpDown1.Value = 0;
                cmd2 = new SqlCommand("SELECT [Kogus] FROM [Toodetable] WHERE Toodenimetus = '" + label1.Text + "'", connect);
                connect.Open();
                numericUpDown1.Maximum = (int)cmd2.ExecuteScalar();
                connect.Close();
            }
            catch
            {
                MessageBox.Show("Vali toode!");
            }
        }

        public void Naita_Andmed()
        {
            connect.Open();
            DataTable dt_toode = new DataTable();
            adapter_toode = new SqlDataAdapter("SELECT * FROM Toodetable", connect);
            adapter_toode.Fill(dt_toode);
            Image img = Image.FromFile("../../../question.png");
            connect.Close();
        }

        /*private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            try
            {
                numericUpDown1.Value = 0;
                label4.Text = null;
                label1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                label2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                label3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                try
                {
                    pictureBox1.Load(@"..\..\pictures\" + dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                }
                catch
                {
                    MessageBox.Show("Fail puudub");
                    pictureBox1.Load(@"..\..\..\question.png");
                }
                Bitmap finalImg = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = finalImg;
                pictureBox1.Show();
                int v = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                cmd = new SqlCommand("SELECT [Kategooria_nimetus] FROM [Kategooriatable] WHERE [Id]=@id", connect);
                cmd2 = new SqlCommand("SELECT [Kogus] FROM [Toodetable] WHERE Toodenimetus = '" + label1.Text + "'", connect);
                connect.Open();
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = v;
                numericUpDown1.Maximum = (int)cmd2.ExecuteScalar();
                label4.Text = cmd.ExecuteScalar().ToString();
                connect.Close();
            }
            catch
            {
                MessageBox.Show("Vali toode!");
            }

        }
        */
        Document tsekk = new Document();
        List <string> tsekk_array = new List<string>();
        List<decimal> kokku_hind = new List<decimal>();
        public void button1_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("UPDATE [Toodetable] SET Kogus = Kogus - "+ numericUpDown1.Value + "WHERE Toodenimetus = '" + label1.Text+"'", connect);
            connect.Open();
            cmd.ExecuteNonQuery();
            connect.Close();
            if (numericUpDown1.Value != 0 && label2.Text != "" && label1.Text != "" && label3.Text != "" && label4.Text != "")
            {
                tsekk_array.Add("Toote nimi " + label1.Text + "; Kogus " + numericUpDown1.Value.ToString() + 
                    "; Hind " + (Decimal.Parse(label3.Text)*numericUpDown1.Value).ToString() +"\u20AC");
                kokku_hind.Add(Decimal.Parse(label3.Text) * numericUpDown1.Value);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            Page page = tsekk.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Tšekk " + now));
            int y = 700;
            decimal kokkuh = 0;
            for (int i = 0; i < tsekk_array.Count; i++)
            {
                TextFragment textFragment = new TextFragment(tsekk_array[i]);
                textFragment.Position = new Position(90, y);
                textFragment.TextState.FontSize = 10;
                textFragment.TextState.Font = FontRepository.FindFont("TimesNewRoman");
                textFragment.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray);
                textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black);
                TextBuilder textBuilder = new TextBuilder(page);
                textBuilder.AppendText(textFragment);
                y -= 20;
            }
            for (int i = 0; i < kokku_hind.Count; i++)
            {
                kokkuh += kokku_hind[i];
            }
            
            TextFragment kooku_text = new TextFragment("Kokku hind: "+kokkuh.ToString() + "\u20AC");
            kooku_text.Position = new Position(90, y-20);
            kooku_text.TextState.FontSize = 13;
            kooku_text.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            kooku_text.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray);
            kooku_text.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Red);
            TextBuilder textBuilder_kokku = new TextBuilder(page);
            textBuilder_kokku.AppendText(kooku_text);

            tsekk.Save($@"C:\Users\lasto\source\repos\Toodeddb\Toodeddb\Arved\Tšekk{now.ToString("_ddMMyyyy_HHmmss")}.pdf");
        }

    }
}
