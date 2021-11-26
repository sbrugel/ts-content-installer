using FenLineInstaller.Properties;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FenLineInstaller
{
    public partial class Form5 : Form
    {
        string gd; //game directory as inputted in previous form
        bool de;
        string tempPath = Path.GetTempPath(); //the PCs temporary file directory
        int numFiles;

        bool installationFinished = false;
        public Form5(string dir, bool german)
        {
            InitializeComponent();
            gd = dir;
            de = german;

            if (de)
            {
                textBox1.Text = "Die Konfiguration ist nun abgeschlossen und der Inhalt kann installiert werden.";
                button2.Text = "Abbrechen";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(installStuff);
            Thread t2 = new Thread(updateDisplay);
            Thread t3 = new Thread(updateBG);
            t1.Start();
            t2.Start();
            t3.Start();
            button1.Enabled = false; //so user cannot install the file twice
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        /// <summary>
        /// The main function that installs all content in the Resources folder, based on the specified file
        /// </summary>
        private void installStuff()
        {
            //no read only attribute just in case there is one
            var di = new DirectoryInfo(tempPath);
            di.Attributes &= ~FileAttributes.ReadOnly;

            //copy zip folder, extract it too. 
            //IMPORTANT: Ensure the "FenLine.zip" and "FenLine" in ALL of the lines below is replaced with whatever your content's ZIP/folder file name is.
            File.WriteAllBytes(tempPath + "\\FenLine.zip", Resources.FenLine); //Also change the "Resources.FenLine" to whatever your content's resource name is
            if (Directory.Exists(tempPath + "\\FenLine"))
            {
                Directory.Delete(tempPath + "\\FenLine", true);
            }
            System.IO.Compression.ZipFile.ExtractToDirectory(tempPath + "\\FenLine.zip", tempPath + "\\FenLine");

            // for Assets (copy everything)
            //based on the FenLine.zip extracted folder in temp, find the proper Assets and Content folders
            numFiles = Directory.GetFiles(tempPath + "\\FenLine\\Railworks\\Assets", "*.*", SearchOption.AllDirectories).Length + 
                Directory.GetFiles(tempPath + "\\FenLine\\Railworks\\Content", "*.*", SearchOption.AllDirectories).Length;

            if (textBox2.InvokeRequired)
            {
                if (de)
                {
                    textBox2.Invoke(new MethodInvoker(delegate { textBox2.Text = "Installation läuft - dies kann einige Minuten dauern"; }));
                } else
                {
                    textBox2.Invoke(new MethodInvoker(delegate { textBox2.Text = "Now installing - this may take a few minutes"; }));
                }
                
            }
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new MethodInvoker(delegate { progressBar1.Style = ProgressBarStyle.Blocks; }));
                progressBar1.Invoke(new MethodInvoker(delegate { progressBar1.Maximum = numFiles; }));
            }

            //for copying preloads custom made.
            //These were added as part of the original Fen Line package. This loop goes through all of these preloads, checks if all prerequisites are present for each (by checking if
            //a GEOPCDX file exists) and copies them if this is the case.
            //You can modify this code accordingly to suit your own needs

            //assume the player does NOT have any AP enhancements installed. These are set to false as they are detected in the below loop.
            bool dmuDefault = true, emuDefault = true, freightDefault = true;

            foreach (var file in Directory.GetFiles(tempPath + "\\FenLine\\Railworks\\Assets", "*.*", SearchOption.AllDirectories))
            {
                if (!Directory.Exists(gd + "\\Assets" + Path.GetDirectoryName(file).Substring(Path.GetDirectoryName(file).IndexOf("Assets") + 6)))
                {
                    Directory.CreateDirectory(gd + "\\Assets" + Path.GetDirectoryName(file).Substring(Path.GetDirectoryName(file).IndexOf("Assets") + 6));
                }
                try
                {
                    if (file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass158cCT.bin"))
                    {
                        if (File.Exists(gd + "\\Assets\\RSC\\Class159Pack01\\RailVehicles\\Diesel\\Class159\\SWR_AP\\DMSL\\159_dmsl.GeoPcDx"))
                        { //cummins EP installed
                            File.Copy(file, (gd + "\\Assets" + Path.GetFullPath(file).Substring(Path.GetFullPath(file).IndexOf("Assets") + 6)), true);
                            dmuDefault = false;
                        }
                    }
                    if (file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass158pCT.bin"))
                    {
                        if (File.Exists(gd + "\\Assets\\RSC\\Class159Pack01\\RailVehicles\\Diesel\\Class159\\ATW_AP\\DMSL\\159_dmsl.GeoPcDx"))
                        { //perkins EP installed
                            File.Copy(file, (gd + "\\Assets" + Path.GetFullPath(file).Substring(Path.GetFullPath(file).IndexOf("Assets") + 6)), true);
                            dmuDefault = false;
                        }
                    }
                    if (file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass170CT.bin") || file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass170ONE.bin"))
                    {
                        if (File.Exists(gd + "\\Assets\\Thomson\\Class170Pack01\\RailVehicles\\Class170\\CT_AP\\Engine\\170_DMSL.GeoPcDx"))
                        { //170 EP installed
                            File.Copy(file, (gd + "\\Assets" + Path.GetFullPath(file).Substring(Path.GetFullPath(file).IndexOf("Assets") + 6)), true);
                            dmuDefault = false;
                        }
                    }

                    if (file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass317FCC.bin") || file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass317NXEA.bin"))
                    {
                        if (File.Exists(gd + "\\Assets\\AP\\Class317Pack01\\RailVehicles\\Class317_1\\Liveries\\GN\\DTSO\\c317_dtso.GeoPcDx"))
                        { //317 EP installed
                            File.Copy(file, (gd + "\\Assets" + Path.GetFullPath(file).Substring(Path.GetFullPath(file).IndexOf("Assets") + 6)), true);
                            emuDefault = false;
                        }
                    }
                    if (file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass365FCC.bin"))
                    {
                        if (File.Exists(gd + "\\Assets\\RSC\\ECMLS\\RailVehicles\\Electric\\Class365\\Class365\\GN_AP\\dmoc.GeoPcDx"))
                        { //365 EP installed
                            File.Copy(file, (gd + "\\Assets" + Path.GetFullPath(file).Substring(Path.GetFullPath(file).IndexOf("Assets") + 6)), true);
                            emuDefault = false;
                        }
                    }


                    if (file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass66FEA.bin"))
                    {
                        if (File.Exists(gd + "\\Assets\\RSC\\Class66Pack02\\RailVehicles\\Diesel\\Class 66\\FL\\emd_66_FL.GeoPcDx"))
                        { //66 EP installed
                            File.Copy(file, (gd + "\\Assets" + Path.GetFullPath(file).Substring(Path.GetFullPath(file).IndexOf("Assets") + 6)), true);
                            freightDefault = false;
                        }
                    }

                    // dont copy preloads all the time
                    if (file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\DEFClass47FTA.bin")
                        || file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\DEFClass158RR.bin")
                        || file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\DEFClass365FCC.bin")
                        || file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass158cCT.bin")
                        || file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass158pCT.bin")
                        || file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass170CT.bin")
                        || file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass170ONE.bin")
                        || file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass317FCC.bin")
                        || file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass317NXEA.bin")
                        || file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass365FCC.bin")
                        || file.Equals(tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\APClass66FEA.bin"))
                    {
                        continue;
                    }

                    //copy anything else that is NOT a preload
                    File.Copy(file, (gd + "\\Assets" + Path.GetFullPath(file).Substring(Path.GetFullPath(file).IndexOf("Assets") + 6)), true);
                }
                catch (Exception ex)
                {
                    if (de)
                    {
                        MessageBox.Show("Während der Installation ist ein Fehler aufgetreten. Daher ist die Installation fehlgeschlagen und wird nicht fortgesetzt: " + ex.Message, "Install Complete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("An error has occurred during installation. As such, installation has failed and will not proceed: " + ex.Message, "Install Complete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Environment.Exit(Environment.ExitCode);
                }
                if (progressBar1.InvokeRequired)
                {
                    progressBar1.Invoke(new MethodInvoker(delegate { progressBar1.PerformStep(); }));
                }
            }

            foreach (var file in Directory.GetFiles(tempPath + "\\FenLine\\Railworks\\Content", "*.*", SearchOption.AllDirectories))
            {
                if (!Directory.Exists(gd + "\\Content" + Path.GetDirectoryName(file).Substring(Path.GetDirectoryName(file).IndexOf("Content") + 7)))
                {
                    Directory.CreateDirectory(gd + "\\Content" + Path.GetDirectoryName(file).Substring(Path.GetDirectoryName(file).IndexOf("Content") + 7));
                }
                try
                {
                    File.Copy(file, (gd + "\\Content" + Path.GetFullPath(file).Substring(Path.GetFullPath(file).IndexOf("Content") + 7)), true);
                } catch (Exception ex)
                {
                    if (de)
                    {
                        MessageBox.Show("Während der Installation ist ein Fehler aufgetreten. Daher ist die Installation fehlgeschlagen und wird nicht fortgesetzt: " + ex.Message, "Install Complete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } else
                    {
                        MessageBox.Show("An error has occurred during installation. As such, installation has failed and will not proceed: " + ex.Message, "Install Complete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Environment.Exit(Environment.ExitCode);
                }
                if (progressBar1.InvokeRequired)
                {
                    progressBar1.Invoke(new MethodInvoker(delegate { progressBar1.PerformStep(); }));
                }
            }
            
            //only copy defaults if needed
            if (freightDefault)
            {
                var file = tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\DEFClass47FTA.bin";
                File.Copy(file, (gd + "\\Assets" + Path.GetFullPath(file).Substring(Path.GetFullPath(file).IndexOf("Assets") + 6)), true);
            }
            if (dmuDefault)
            {
                var file = tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\DEFClass158RR.bin";
                File.Copy(file, (gd + "\\Assets" + Path.GetFullPath(file).Substring(Path.GetFullPath(file).IndexOf("Assets") + 6)), true);
            }
            if (emuDefault)
            {
                var file = tempPath + "\\FenLine\\Railworks\\Assets\\4AS\\FenLine\\Preload\\DEFClass365FCC.bin";
                File.Copy(file, (gd + "\\Assets" + Path.GetFullPath(file).Substring(Path.GetFullPath(file).IndexOf("Assets") + 6)), true);
            }

            installationFinished = true;
            if (de)
            {
                MessageBox.Show("Installation erfolgreich abgeschlossen. Dieses Programm wird nun schließen. Genießen sie die Route!", "Install Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
            {
                MessageBox.Show("Installation successfully completed. The program will now close. Enjoy the route!", "Install Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Environment.Exit(Environment.ExitCode);
        }

        /// <summary>
        /// Consistently updates the display while installing as to not freeze the window during installation.
        /// </summary>
        private void updateDisplay()
        {
            if (textBox2.InvokeRequired)
            {
                if (de)
                {
                    textBox2.Invoke(new MethodInvoker(delegate { textBox2.Text = "Extraktion läuft - dies kann einige Minuten dauern"; }));
                } else
                {
                    textBox2.Invoke(new MethodInvoker(delegate { textBox2.Text = "Extraction in progress - this may take a few minutes"; }));
                }
                
            }
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new MethodInvoker(delegate { progressBar1.Style = ProgressBarStyle.Marquee; }));
                progressBar1.Invoke(new MethodInvoker(delegate { progressBar1.MarqueeAnimationSpeed = 30; }));
            }
            Application.DoEvents();
        }

        /// <summary>
        /// At 5 second intervals, updates the background.
        /// </summary>
        private void updateBG()
        {
            while (!installationFinished)
            {
                if (pictureBox1.InvokeRequired)
                {
                    Thread.Sleep(5000);
                    pictureBox1.Invoke(new MethodInvoker(delegate { pictureBox1.BackgroundImage = Resources.fen_4; }));
                    Thread.Sleep(5000);
                    pictureBox1.Invoke(new MethodInvoker(delegate { pictureBox1.BackgroundImage = Resources.fen_5; }));
                    Thread.Sleep(5000);
                    pictureBox1.Invoke(new MethodInvoker(delegate { pictureBox1.BackgroundImage = Resources.fen_6; }));
                    Thread.Sleep(5000);
                    pictureBox1.Invoke(new MethodInvoker(delegate { pictureBox1.BackgroundImage = Resources.fen_7; }));
                    Thread.Sleep(5000);
                    pictureBox1.Invoke(new MethodInvoker(delegate { pictureBox1.BackgroundImage = Resources.fen_8; }));
                    Thread.Sleep(5000);
                    pictureBox1.Invoke(new MethodInvoker(delegate { pictureBox1.BackgroundImage = Resources.fen_9; }));
                    Thread.Sleep(5000);
                    pictureBox1.Invoke(new MethodInvoker(delegate { pictureBox1.BackgroundImage = Resources.fen_3; }));
                }
            }
        }
    }
}
