using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FenLineInstaller
{
    public partial class Form4 : Form
    {

        string gd;
        bool de;
        public Form4(string dir, bool german)
        {
            de = german;
            gd = dir;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
