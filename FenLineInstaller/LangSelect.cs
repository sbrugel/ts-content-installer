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
    public partial class LangSelect : Form
    {
        public LangSelect()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetVisibleCore(false);
            new Form1(radioButton2.Checked).ShowDialog();
            Dispose();
        }
    }
}
