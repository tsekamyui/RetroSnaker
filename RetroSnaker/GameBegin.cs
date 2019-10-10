using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroSnaker
{
    public partial class GameBegin : Form
    {
        public static int timeInterval = 100;

        public GameBegin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(this);
            if (radioButton1.Checked)
            {
                timeInterval = 200;
            } else if (radioButton3.Checked)
            {
                timeInterval = 50;
            } else if (radioButton4.Checked)
            {
                timeInterval = 20;
            } else
            {
                timeInterval = 100;
            }
            form.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
