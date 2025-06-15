using IniParser;
using System.Diagnostics;

namespace QuickTick
{
    public partial class MainForm : Form
    {
        private List<Games> entries = new List<Games>();
        private string ExecutablePath = string.Empty;
        public string IniPath = string.Empty;
        private string EditorPath = string.Empty;
        private string BannerPath = string.Empty;
        private string RegistryPath = string.Empty;
        private string RecommendedFixSite = string.Empty;
        private string EditorFixSite = string.Empty;
        private string InstallLocation = string.Empty;
        private static readonly IniFile MyIni = new IniFile("games.ini");
        public MainForm()
        {
            InitializeComponent();
            if (!File.Exists("games.ini")) { MessageBox.Show("games.ini" + " is missing."); Close(); }
            for (int i = 0; i < 1000; i++)
            {
                string entry = "Game" + i.ToString();
                if (MyIni.KeyExists(entry, "QuickTick"))
                {
                    string name = MyIni.Read(entry, "QuickTick");
                    if (!File.Exists($"{name}")) { MessageBox.Show($"{name}" + " is missing."); }
                    else
                    {
                        string game = new IniFile($"{name}").Read("Name", "Game");
                        listBox1.Items.Add(game);
                        entries.Add(new Games { Name = game, Path = name });
                    }
                }
                else { break; } // No more games to read
            }
            pictureBox1.ImageLocation = "quicktick.png";
        }

        private void button1_Click(object sender, EventArgs e) { startProcess("Game", ExecutablePath); Close(); }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Games game in entries)
            {
                if (game.Name == listBox1.SelectedItem!.ToString())
                {
                    // INI Entries
                    IniPath = game.Path;
                    IniFile iniFile = new IniFile($"{IniPath}");
                    ExecutablePath = iniFile.Read("Executable", "Game");
                    EditorPath = iniFile.Read("Editor", "Game");
                    RegistryPath = iniFile.Read("RegistryRoot", "Game");
                    BannerPath = iniFile.Read("Banner", "Game");
                    InstallLocation = iniFile.Read("InstallLocation", "Game");
                    // Button Activation
                    button1.Enabled = !string.IsNullOrWhiteSpace(ExecutablePath);
                    button2.Enabled = !string.IsNullOrWhiteSpace(RegistryPath);
                    button3.Enabled = !string.IsNullOrWhiteSpace(EditorPath);
                    // Picture Display
                    pictureBox1.ImageLocation = !string.IsNullOrWhiteSpace(BannerPath) && File.Exists(BannerPath) ? BannerPath : null;
                    // Recommended Fix
                    RecommendedFixSite = iniFile.Read("RecommendedFix" + "Site", "Game");
                    string GameFix = iniFile.Read("RecommendedFix", "Game");
                    if (!File.Exists(Path.Combine(InstallLocation, GameFix)) && !string.IsNullOrWhiteSpace(RecommendedFixSite))
                    {
                        MessageBox.Show("Recommended fix for the " + "Game" + " not found. Using this fix is advised.");
                        button4.Enabled = true;
                        button4.Visible = true;
                    }
                    else { button4.Enabled = false; button4.Visible = false; ExecutablePath = GameFix; }
                    EditorFixSite = iniFile.Read("EditorFix" + "Site", "Game");
                    string EditorFix = iniFile.Read("EditorFix", "Game");
                    if (!File.Exists(Path.Combine(InstallLocation, EditorFix)) && !string.IsNullOrWhiteSpace(EditorFixSite))
                    {
                        MessageBox.Show("Recommended fix for the " + "Editor" + " not found. Using this fix is advised.");
                        button5.Enabled = true;
                        button5.Visible = true;
                    }
                    else { button5.Enabled = false; button5.Visible = false; EditorPath = EditorFix; }
                    // Mods
                    if (iniFile.KeyExists("Mod0" + "Name", "Mods")) { button6.Enabled = true; break; }
                    else { button6.Enabled = false; }
                    break;
                }
            }
        }
        private void newForm(Form form)
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Location = this.Location;
            form.Show();
            this.Hide();
            form.FormClosed += (s, args) => this.Show();
            form.Move += (s, args) => { if (this.Location != form.Location) { this.Location = form.Location; } };
        }
        private void button2_Click(object sender, EventArgs e) { newForm(new RegistryEditor()); }
        private void button3_Click(object sender, EventArgs e) { startProcess("Editor", EditorPath); Close(); }
        public void startProcess(string process, string executable)
        {
            string path = Path.Combine(InstallLocation, executable);
            if (!File.Exists(path)) { MessageBox.Show(process + " " + "Executable" + " not found. Please check the path in the ini file."); return; }
            launchProcess(path);
        }
        public void launchProcess(string process) { Process.Start(new ProcessStartInfo(process) { UseShellExecute = true }); }
        private void button4_Click(object sender, EventArgs e) { launchProcess(RecommendedFixSite); }
        private void button5_Click(object sender, EventArgs e) { launchProcess(EditorFixSite); }
        private void button6_Click(object sender, EventArgs e) { newForm(new ModsForm(IniPath)); }
    }
}
