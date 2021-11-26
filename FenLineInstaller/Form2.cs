using System;
using System.Windows.Forms;

namespace FenLineInstaller
{
    public partial class Form2 : Form
    {
        bool de;
        public Form2(bool german)
        {
            de = german;
            InitializeComponent();

            if (de)
            {
                textBox1.Text = "Durch die Installation dieses Inhalts stimmen Sie den unten aufgeführten Nutzungsbedingungen zu.";
                textBox2.Text = "Die Strecke 'Fen Line: Ely to Kings Lynn', im Folgenden nur noch als 'die Strecke' oder 'die Route' bezeichnet, war von 4 Aspect Simulations erstellt. Dadurch würden mehrere Assets von mehreren Erstellern benutzt, und durch der Nutzung dieser Assets wurden keine Geschäftsbedingungen verletzt."
                    + Environment.NewLine + Environment.NewLine +
                    "In dieser Route sind verschiedene Assets enthalten. Sie dürfen diese Assets in Ihren eigenen Inhalten verwenden (z. B. auf Ihrer eigenen Route). Das Freigeben dieses Inhalts ist ebenfalls zulässig, sofern die folgenden beiden Bedingungen erfüllt sind."
                    + Environment.NewLine +
                    "- Das Herunterladen Ihrer Inhalte führt zu keinem finanziellen Gewinn. Dies schließt direkt bezahlte Inhalte (Payware) ein, die hinter einem Abonnement oder einer Spendenwand gesperrt sind. Grundsätzlich alles, wofür Benutzer bezahlen müssten."
                    + Environment.NewLine +
                    "- Die Assets werden nicht mit Ihren Inhalten neu verteilt."
                    + Environment.NewLine + Environment.NewLine +
                    "Sie sind auch erlaubt (und stark ermutigt) Inhalte für die Strecke zu erstellen, einschließlich, aber nicht beschränkt auf, Szenarien. Sie dürfen diese veröffentlichen, solange die beiden oben gennanten Bedingunen nicht gebrochen sind."
                    + Environment.NewLine + Environment.NewLine +
                    "Dieser Installer und alle Dateien der Route wurden auf Sicherheit überprüft. 4 Aspect Simulations haftet nicht für Schäden, die durch die Installation oder Verwendung dieser Route verursacht werden.";
                radioButton1.Text = "Ich stimme diesen Geschäftsbedingungen zu.";
                radioButton2.Text = "Ich stimme diesen Geschäftsbedingungen nicht zu.";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = radioButton1.Checked; //toggles the Next button if the Agree radio button is ticked
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetVisibleCore(false);
            new Form3(de).ShowDialog();
            Dispose();
        }
    }
}
