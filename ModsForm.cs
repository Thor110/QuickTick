using IniParser;
using System.Diagnostics;

namespace QuickTick
{
    public partial class ModsForm : Form
    {
        private IniFile iniFile;
        public ModsForm(string path)
        {
            InitializeComponent();
            iniFile = new IniFile($"{path}");
            for (int i = 0; i < 1000; i++)
            {
                string entry = "Mod" + i.ToString() + "Name";
                if (iniFile.KeyExists(entry, "Mods"))
                {
                    listBox1.Items.Add(iniFile.Read(entry, "Mods"));
                }
                else { break; } // No more mods to read
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = !string.IsNullOrWhiteSpace(iniFile.Read($"Mod{listBox1.SelectedIndex}Link", "Mods"));
            string description = iniFile.Read($"Mod{listBox1.SelectedIndex}Desc", "Mods");
            richTextBox1.Text = string.IsNullOrWhiteSpace(description) ? "No description available." : description;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo(iniFile.Read($"Mod{listBox1.SelectedIndex}Link")) { UseShellExecute = true });
        }
    }
}
