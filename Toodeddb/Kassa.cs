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
        SqlDataAdapter adapter_kat, adapter_nimi, adapter_toode;
        SqlCommand cmd, cmd2, cmd3;
        SqlDataReader reader;
        TabControl kategooriad;
        DataGridView dataGrid;
        ListBox listBox1 , listBox2;
        TableLayoutPanel tabLP;
        public Kassa()
        {
            Kategooriad();
            InitializeComponent();
        }
        int kat_Id;
        public void Kategooriad()
        {
            listBox1 = new ListBox();
            listBox2 = new ListBox();
            kategooriad = new TabControl();
            kategooriad.Name = "Kategooriad";
            kategooriad.Width = 891;
            kategooriad.Height = 313;
            connect.Open();
            adapter_kat = new SqlDataAdapter("SELECT Id, Kategooria_nimetus FROM Kategooriatable", connect);
            DataTable dt_kat = new DataTable();
            adapter_kat.Fill(dt_kat);
            ImageList iconsList = new ImageList();
            iconsList.ColorDepth = ColorDepth.Depth32Bit;
            iconsList.ImageSize = new Size(25, 25);

            int i = 0;
            connect.Close();
            foreach (DataRow nimetus in dt_kat.Rows)
            {
                kategooriad.TabPages.Add((string)nimetus["Kategooria_nimetus"]);
                iconsList.Images.Add(Image.FromFile(@"..\..\katimg\" + (string)nimetus["Kategooria_nimetus"] + ".jpg"));
                kategooriad.TabPages[i].ImageIndex = i;
                i++;
                kat_Id = (int)nimetus["Id"];
                //dataGrid = new DataGridView
                //{
                //    Width = 891,
                //    Height = 285,
                //    RowHeadersVisible = false,
                //    ColumnHeadersVisible = false
                //};
                tabLP = new TableLayoutPanel
                {
                    Width = 891,
                    Height = 285
                };
                kategooriad.TabPages[i - 1].Controls.Add(tabLP);
                //dataGrid.CellClick += DataGrid_CellClick;
                int colWidth;
                int rowHeight;
                PictureBox pbox;
                Random rnd = new Random();

                tabLP.Controls.Clear();
                tabLP.ColumnStyles.Clear();
                tabLP.RowStyles.Clear();

                
                connect.Open();
                cmd = new SqlCommand("SELECT id, Pilt FROM Toodetable WHERE Kategooria_id = " + kat_Id, connect);
                reader = cmd.ExecuteReader();
                List<string> name_pilt = new List<string>();
                List<int> toode_id = new List<int>();
                while (reader.Read())
                {
                    name_pilt.Add(reader["Pilt"].ToString());
                    toode_id.Add((int)reader["id"]);
                }
                connect.Close();
                int tick = 0;
                string[] strings = name_pilt.ToArray();
                
                if (name_pilt.Count != 0)
                {
                    int rows = strings.Skip(strings.Length / 2).ToArray().Length;
                    int cols = strings.Take(strings.Length / 2).ToArray().Length;
                    colWidth = 100 / cols;
                    if (100 % cols != 0)
                        colWidth--;

                    rowHeight = 100 / rows;
                    if (100 % rows != 0)
                        rowHeight--;
                    tabLP.ColumnCount = cols;
                    for (int f = 0; f < rows; f++)
                    {
                        tabLP.RowStyles.Add(new RowStyle(SizeType.Percent, rowHeight));
                        for (int j = 0; j < cols; j++)
                        {
                            if (name_pilt.Count - 1 >= tick)
                            {
                                tabLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colWidth));
                                pbox = new PictureBox() { Image = Image.FromFile(@"..\..\pictures\" + name_pilt[tick]) };
                                pbox.Dock = DockStyle.Fill;
                                pbox.SizeMode = PictureBoxSizeMode.StretchImage;
                                pbox.Click += Pbox_Click;
                                pbox.Tag = toode_id[tick];
                                pbox.Name = (string)nimetus["Kategooria_nimetus"];
                                tick++;
                                tabLP.Controls.Add(pbox, j, f);                         
                            }
                        }
                        
                    }
                }
                //DataTable dt_toode = new DataTable();
                //adapter_toode = new SqlDataAdapter("SELECT id, Pilt FROM Toodetable WHERE Kategooria_id = " + kat_Id, connect);
                //adapter_toode.Fill(dt_toode);
                //dataGrid.DataSource = dt_toode;
                //List<string> name_pilt = new List<string>();
                //List<int> toode_id = new List<int>();
                //while (reader.Read())
                //{
                //    name_pilt.Add(reader["Pilt"].ToString());
                //    toode_id.Add((int)reader["id"]);
                //}
                //for (int j = 0; j < name_pilt.Count; j++)
                //{
                //    Image img = Image.FromFile(@"..\..\pictures\" + name_pilt[j]);
                //    DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
                //    iconColumn.Width = 100;
                //    iconColumn.Image = img;
                //    iconColumn.Name = "Icon_name" + j;
                //    iconColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                //    dataGrid.Columns.Add(iconColumn);
                //    dataGrid.Rows[dataGrid.NewRowIndex].Cells["Icon_name" + j].Value = img;
                //    dataGrid.Rows[dataGrid.NewRowIndex].Cells["Icon_name" + j].Tag = toode_id[j];
                //    dataGrid.Rows[dataGrid.NewRowIndex].Cells["Icon_name" + j].ToolTipText = (string)nimetus["Kategooria_nimetus"];
                //    dataGrid.Rows[dataGrid.NewRowIndex].Height = 80;
                //}
                //List<int> temp = Split(name_pilt.Count, 3);
                //int tick = 0;
                //for (int x = 0; x < temp.Count; x++)
                //{
                //    DataGridViewImageColumn column = new DataGridViewImageColumn();
                //    dataGrid.Columns.Add(column);

                //    for (int y = 0; y < temp[x]; y++)
                //    {
                //  //      row.Cells[y].Value = Image.FromFile(@"..\..\pictures\" + name_pilt[tick]);
                //        tick++;
                //    }
                //}

                //for (int j = 0; j < name_pilt.Count; j++)
                //{
                //    Image img = Image.FromFile(@"..\..\pictures\" + name_pilt[j]);
                //    DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
                //    iconColumn.Width = 100;
                //    iconColumn.Image = img;
                //    iconColumn.Name = "Icon_name" + j;
                //    iconColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                //    dataGrid.Columns.Add(iconColumn);
                //    dataGrid.Rows[dataGrid.NewRowIndex].Cells["Icon_name" + j].Value = img;
                //    dataGrid.Rows[dataGrid.NewRowIndex].Cells["Icon_name" + j].Tag = toode_id[j];
                //    dataGrid.Rows[dataGrid.NewRowIndex].Cells["Icon_name" + j].ToolTipText = (string)nimetus["Kategooria_nimetus"];
                //    dataGrid.Rows[dataGrid.NewRowIndex].Height = 80;
                //}
                //decimal total = name_pilt.Count;
                //decimal max = 3;
                //decimal tempa = Math.Ceiling(total/max);
                //decimal tempb = Math.Floor(total / tempa);
                //decimal result = total % tempb;
                /*int temp = 0;
                int tempx = 0;
                for (int x = 0; x < name_pilt.Count; x++)
                {
                    DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
                    iconColumn.Width = 100;
                    iconColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                    dataGrid.Columns.Insert(1, iconColumn);
                    //for (int y = 0; y < 2; y++)
                    
                        DataGridViewRow iconRow = new DataGridViewRow();
                        iconRow.Height = 80;
                        Image img = Image.FromFile(@"..\..\pictures\" + name_pilt[temp]);

                        dataGrid.Rows[0].Cells[x].Value = img;
                        dataGrid.Rows[0].Cells[x].Tag = toode_id[temp];
                        dataGrid.Rows[0].Cells[x].ToolTipText = (string)nimetus["Kategooria_nimetus"];
                        if (temp + 1 >= name_pilt.Count)
                        {
                            break;
                        }
                        temp++;
                    dataGrid.Rows.Insert(0, iconRow);

                    break;
                    DataGridViewRow iconRow = new DataGridViewRow();
                    dataGrid.Rows.Add(iconRow);

                }*/
            }
            kategooriad.ImageList = iconsList;
            this.Controls.Add(kategooriad);
            listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 18;
            listBox1.Location = new System.Drawing.Point(647, 418);
            listBox1.Size = new Size(259, 76);
            listBox1.TabIndex = 30;
            this.Controls.Add(listBox1);
            listBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 18;
            listBox2.Location = new System.Drawing.Point(285, 450);
            listBox2.Size = new Size(259, 100);
            listBox2.TabIndex = 30;
            this.Controls.Add(listBox2);
            Naita_nimi();
        }

        private void Pbox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            try
            {
                connect.Open();
                cmd3 = new SqlCommand("SELECT Toodenimetus, Kogus, Hind FROM Toodetable WHERE id = " + pictureBox.Tag, connect);
                reader = cmd3.ExecuteReader();
                while (reader.Read())
                {
                    label1.Text = reader["Toodenimetus"].ToString();
                    label2.Text = reader["Kogus"].ToString();
                    label3.Text = reader["Hind"].ToString();
                }
                label4.Text = pictureBox.Name;
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

        private static List<int> Split(int amount, int maxPerGroup)
        {
            int amountGroups = amount / maxPerGroup;

            if (amountGroups * maxPerGroup < amount)
            {
                amountGroups++;
            }

            List<int> result = new List<int>();
            for (int i = 0; i < amountGroups; i++)
            {
                result.Add(Math.Min(maxPerGroup, amount));
                amount -= Math.Min(maxPerGroup, amount);
            }
            return result;
        }
        
        private void Naita_nimi()
        {
            adapter_nimi = new SqlDataAdapter("SELECT isikukood FROM [Kliendid]", connect);
            DataTable dt_kategooria = new DataTable();
            adapter_nimi.Fill(dt_kategooria);
            listBox1.Items.Add("vali");
            foreach (DataRow nimetus in dt_kategooria.Rows)
            {
                listBox1.Items.Add(nimetus["isikukood"].ToString());
            }
        }

        private void DataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


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
                listBox2.Items.Add(label1.Text + "; Kogus " + numericUpDown1.Value.ToString());
            }
        }

        string kaart_;

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Remove(listBox2.SelectedIndex);
            tsekk_array.RemoveAt(listBox2.SelectedIndex);
            kokku_hind.RemoveAt(listBox2.SelectedIndex);
        }

        decimal allahindlus;
        string result;
        string nimi;
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
            connect.Open();
            if (listBox1.Items[listBox1.SelectedIndex].ToString() != "vali") 
            {
                cmd = new SqlCommand("SELECT kliendikaartID FROM Kliendid WHERE isikukood = '" + listBox1.Items[listBox1.SelectedIndex].ToString() + "'", connect);
                result = cmd.ExecuteScalar().ToString();
                cmd = new SqlCommand("SELECT nimi FROM Kliendid WHERE isikukood = '" + listBox1.Items[listBox1.SelectedIndex].ToString() + "'", connect);
                nimi = cmd.ExecuteScalar().ToString();
                cmd = new SqlCommand("SELECT kliendikaart, allahindlus_summ FROM Kliendikaart WHERE id = (SELECT kliendikaartID FROM Kliendid WHERE isikukood = '" + listBox1.Items[listBox1.SelectedIndex].ToString() + "')", connect);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    kaart_ = reader["kliendikaart"].ToString();
                    if (result != "4")
                    {
                        allahindlus = (decimal)reader["allahindlus_summ"];
                    }
                }
            }
            
            connect.Close();       
            TextFragment kooku_text;
            TextFragment kliendid_text;
            if (listBox1.Items[listBox1.SelectedIndex].ToString() == "vali" || listBox1.Items[listBox1.SelectedIndex].ToString() == null)
            {
                for (int i = 0; i < kokku_hind.Count; i++)
                {
                    kokkuh += kokku_hind[i];
                }
                kooku_text = new TextFragment("Kokku hind: " + kokkuh.ToString() + "\u20AC");
                kliendid_text = new TextFragment("");
            }
            else if (result == "4")
            {
                for (int i = 0; i < kokku_hind.Count; i++)
                {
                    kokkuh += kokku_hind[i];
                }
                kooku_text = new TextFragment("Kokku hind: " + kokkuh.ToString() + "\u20AC");
                kliendid_text = new TextFragment("Kliendi nimi: " + nimi + "  Kliendikaart: " + kaart_);

            }
            else 
            {
                for (int i = 0; i < kokku_hind.Count; i++)
                {
                    kokkuh += kokku_hind[i];
                }
                kokkuh = Math.Round(kokkuh * allahindlus, 2);
                decimal a = 100 - allahindlus*100;
                kooku_text = new TextFragment("Kokku hind: " + kokkuh.ToString() + "\u20AC");
                kliendid_text = new TextFragment("Kliendi nimi: " + nimi + "  Kliendikaart: " + kaart_ + "  Allahindlus: "+ a +"%");

            }
            int punktid = Decimal.ToInt32(Math.Round(kokkuh * 0.05M,0, MidpointRounding.AwayFromZero));
            connect.Open();
            cmd2 = new SqlCommand("SELECT bonuspunktid FROM Kliendid WHERE isikukood = '" + listBox1.Items[listBox1.SelectedIndex].ToString() + "'", connect);
            int bonus_ = (int)cmd2.ExecuteScalar();
            int final_punkt = punktid + bonus_;
            cmd = new SqlCommand("UPDATE Kliendid SET bonuspunktid = " + final_punkt + "WHERE isikukood = '" + listBox1.Items[listBox1.SelectedIndex].ToString() + "'", connect);
            cmd.ExecuteNonQuery();
            connect.Close();
            kooku_text.Position = new Position(90, y - 20);
            kooku_text.TextState.FontSize = 13;
            kooku_text.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            kooku_text.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray);
            kooku_text.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Red);
            TextBuilder textBuilder_kokku = new TextBuilder(page);
            kliendid_text.Position = new Position(90, y - 40);
            kliendid_text.TextState.FontSize = 13;
            kliendid_text.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            kliendid_text.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray);
            kliendid_text.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Red);
            TextBuilder kliendid_text_build = new TextBuilder(page);
            textBuilder_kokku.AppendText(kooku_text);
            kliendid_text_build.AppendText(kliendid_text);
            tsekk.Save($@"..\..\Arved\Tšekk{now.ToString("_ddMMyyyy_HHmmss")}.pdf");
            System.Diagnostics.Process.Start($@"..\..\Arved\Tšekk{now.ToString("_ddMMyyyy_HHmmss")}.pdf");
            tsekk.Pages.Delete();
            tsekk_array.Clear();
            kokku_hind.Clear();
        }

    }
}