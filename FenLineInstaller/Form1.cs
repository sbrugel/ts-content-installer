using System;
using System.Drawing;
using System.Windows.Forms;

namespace FenLineInstaller
{
    public partial class Form1 : Form
    {

        /*
         * ORDER OF SLIDES
         * 1) Language selection
         * 2) Intro
         * 3) T&C
         * 4) TS directory selection. Also includes checking requirements are present.
         * 5) Content selection to ensure an ideal quickdrive experience. Can be skipped if user is not interested in QD.
         * 6) Install the content.
         */

        /*
         * QUICK DRIVE SETTINGS
         * - AP 158 EP
         * - AP 170 EP
         * - AP 317 V1
         * - AP 365 EP
         * 
         * Ticking either the 158/170 disables the default 158.
         * Ticking either the 317/365 disables the default 365.
         */

        bool de; //is the installer in german?

        public Form1(bool german)
        {
            de = german;
            InitializeComponent();
            label1.BackColor = Color.Transparent;
            if (de) //change language of components if German language is enabled. Will most likely be improved on a future update
            {
                label1.Text = "Wilkommen";
                textBox1.Text = "Dieses Programm fügt die Strecke 'Fen Line: Ely to Kings Lynn' von 4 Aspect Simulations in ihren Dovetail Games Train Simulator 2021 ein.";
                textBox1.Text += Environment.NewLine;
                textBox1.Text += Environment.NewLine;
                textBox1.Text += "Klicken Sie unten auf OK, um fortzufahren, oder auf Abbrechen, um die Installation abzubrechen.";
                button2.Text = "Abbrechen";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (de)
            {
                MessageBox.Show("Sie haben die Installation dieses Addons abgebrochen. Ihr System würde nicht modifiziert.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                MessageBox.Show("You have cancelled the installation of this content. Your system has not been modified.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            Dispose();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SetVisibleCore(false);
            new Form2(de).ShowDialog(); //load the next form and show the dialog
            Dispose();
        }
    }
}
