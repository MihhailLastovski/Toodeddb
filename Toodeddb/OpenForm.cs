using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toodeddb
{
    public partial class OpenForm : Form
    {

        public OpenForm(string role)
        {
            InitializeComponent();
            if (role == "Muuja")
            {
                button2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Kassa().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form1().ShowDialog();
        }
    }
}
