using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EpicorConnect;

namespace PruebasEpicor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Patern obj = new Patern("ltapia","ltapia","DLMAC");
            obj.insertUD01();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Patern obj2 = new Patern("ltapia", "ltapia", "DLMAC");
            obj2.updateUD01();
        }
    }
}
