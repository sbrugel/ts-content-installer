using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FenLineInstaller
{
    public partial class Form3 : Form
    {
        string gameDirectory;

        bool de;
        public Form3(bool german)
        {
            de = german;
            InitializeComponent();

            // get TS path from registry
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Railsimulator.com\RailWorks");
                object objRegisteredValue = key.GetValue("EXE_Path");
                gameDirectory = objRegisteredValue.ToString().Substring(0, objRegisteredValue.ToString().Length - 13); //remove executable name
            } catch (Exception ex) //if registry key doesn't exist, fallback on the default Steam directory
            {
                gameDirectory = @"C:\Program Files (x86)\Steam\steamapps\common\RailWorks";
            }

            if (german)
            {
                textBox1.Text = "Bitte navigieren Sie zu ihrem Railworks/Train Simulator Hauptpfad, wenn das Folgende falsch ist.";
                textBox3.Text = "Der Installer prüft dann, ob Sie alle benötigten Addons der Route besitzen. Beachten Sie, dass die Installation fortgesetzt werden kann, wenn Sie eines oder mehrere davon fehlen. Es kann jedoch keine Unterstützung für Probleme gegeben werden, auf die Sie möglicherweise stoßen.";
                textBox4.Text = "Elemente in grün sind installiert. Für nicht gefundene Elemente ist ein orangenes Element für die Routenfunktionalität NICHT unbedingt erforderlich (wird jedoch weiterhin empfohlen). Jedes rote Element ist wichtig und unbedingt benötigt. Wenn ein Element rot ist, wird die Route nicht angezeigt oder funktioniert nicht wie beabsichtigt.";
            }
            checkReqs();
        }

        private void checkReqs() //changes color to green etc.
        {
            textBox2.Text = gameDirectory;

            //Make sure these if statements correspond to each line of the listView
            //These check if the directories exist, using the Content folder for routes, Assets for asset packages
            if (Directory.Exists(gameDirectory + "\\Content\\Routes\\00000046-0000-0000-0000-000000002014"))
                listView1.Items[0].BackColor = Color.LightGreen;
            else listView1.Items[0].BackColor = Color.LightPink;

            if (Directory.Exists(gameDirectory + "\\Content\\Routes\\00000029-0000-0000-0000-000000002014"))
                listView1.Items[1].BackColor = Color.LightGreen;
            else listView1.Items[1].BackColor = Color.LightPink;

            if (Directory.Exists(gameDirectory + "\\Assets\\RSC\\MP02_Town"))
                listView1.Items[2].BackColor = Color.LightGreen;
            else listView1.Items[2].BackColor = Color.LightGoldenrodYellow;

            if (Directory.Exists(gameDirectory + "\\Assets\\RSC\\MPPlatformClutter01"))
                listView1.Items[3].BackColor = Color.LightGreen;
            else listView1.Items[3].BackColor = Color.LightGoldenrodYellow;

            if (Directory.Exists(gameDirectory + "\\Content\\Routes\\30f0de2f-465e-495d-ac2f-1786a9a4e82a"))
                listView1.Items[4].BackColor = Color.LightGreen;
            else listView1.Items[4].BackColor = Color.LightPink;

            if (Directory.Exists(gameDirectory + "\\Content\\Routes\\3a99321a-0bb2-47be-bcad-b20cfe48a945"))
                listView1.Items[5].BackColor = Color.LightGreen;
            else listView1.Items[5].BackColor = Color.LightPink;

            if (Directory.Exists(gameDirectory + "\\Assets\\JustTrains\\CommonLibrary") && Directory.Exists(gameDirectory + "\\Assets\\JustTrains\\MML"))
                listView1.Items[6].BackColor = Color.LightGreen;
            else listView1.Items[6].BackColor = Color.LightPink;

            if (Directory.Exists(gameDirectory + "\\Assets\\Cynx\\SpeedSignage") && Directory.Exists(gameDirectory + "\\Assets\\4AS\\SignagePack"))
                listView1.Items[8].BackColor = Color.LightGreen;
            else listView1.Items[8].BackColor = Color.LightPink;

            if (Directory.Exists(gameDirectory + "\\Assets\\VulcanProductions\\TreePack"))
                listView1.Items[7].BackColor = Color.LightGreen;
            else listView1.Items[7].BackColor = Color.LightPink;
        }

        /// <summary>
        /// Update the game directory
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                gameDirectory = @folderBrowserDialog1.SelectedPath;
                checkReqs();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetVisibleCore(false);
            new Form5(gameDirectory, de).ShowDialog();
            Dispose();
        }
    }
}
