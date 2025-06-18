using IniParser;
using System.Diagnostics;
using System.IO;

namespace QuickTick
{
    public partial class MainForm : Form
    {
        private List<Games> entries = new List<Games>();
        private string ExecutablePath = string.Empty;
        public string IniPath = string.Empty;
        private string EditorPath = string.Empty;
        private string BannerPath = string.Empty;
        private string RecommendedFixSite = string.Empty;
        private string EditorFixSite = string.Empty;
        private string InstallLocation = string.Empty;
        private string GameINI = string.Empty;
        public MainForm()
        {
            InitializeComponent();
            detectEntries();
        }
        public void detectEntries()
        {
            string iniDirectory = Path.Combine(Application.StartupPath, "INIs");
            if (!Directory.Exists(iniDirectory)) { Directory.CreateDirectory(iniDirectory); return; }
            string[] iniFiles = Directory.GetFiles(iniDirectory, "*.ini", SearchOption.TopDirectoryOnly);
            foreach (var iniFile in iniFiles.Take(65535))
            {
                IniFile file = new IniFile(iniFile);
                string game = file.Read("Name", "Game");
                string test = Path.GetFileNameWithoutExtension(iniFile);
                string name = string.IsNullOrWhiteSpace(game) ? test : game;
                listBox1.Items.Add(string.IsNullOrWhiteSpace(name) ? test : name);
                entries.Add(new Games { Name = name, Path = iniFile });
            }
        }
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
                    BannerPath = iniFile.Read("Banner", "Game");
                    BannerPath = Path.Combine("INIs", BannerPath);
                    InstallLocation = iniFile.Read("InstallLocation", "Game");
                    GameINI = iniFile.Read("GameINI", "Game");
                    // Button Activation
                    button1.Enabled = !string.IsNullOrWhiteSpace(ExecutablePath) || !string.IsNullOrWhiteSpace(iniFile.Read("SteamID", "Game"));
                    button2.Enabled = !string.IsNullOrWhiteSpace(iniFile.Read("RegistryRoot", "Game"));
                    button3.Enabled = !string.IsNullOrWhiteSpace(EditorPath);
                    button7.Enabled = !string.IsNullOrWhiteSpace(GameINI);
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
                    else
                    {
                        button4.Enabled = false;
                        button4.Visible = false;
                        if (!string.IsNullOrWhiteSpace(GameFix)) { ExecutablePath = GameFix; }
                    }
                    EditorFixSite = iniFile.Read("EditorFix" + "Site", "Game");
                    string EditorFix = iniFile.Read("EditorFix", "Game");
                    if (!File.Exists(Path.Combine(InstallLocation, EditorFix)) && !string.IsNullOrWhiteSpace(EditorFixSite))
                    {
                        MessageBox.Show("Recommended fix for the " + "Editor" + " not found. Using this fix is advised.");
                        button5.Enabled = true;
                        button5.Visible = true;
                    }
                    else
                    {
                        button5.Enabled = false;
                        button5.Visible = false;
                        if (!string.IsNullOrWhiteSpace(EditorFix)) { EditorPath = EditorFix; }
                    }
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
        private void startProcess(string process, string executable)
        {
            string path = Path.Combine(InstallLocation, executable);
            // Steam Support
            IniFile iniFile = new IniFile($"{IniPath}");
            string SteamID = iniFile.Read("SteamID", "Game");
            if (!string.IsNullOrWhiteSpace(SteamID) && process == "Game") { launchProcess($"steam://rungameid/{SteamID}"); return; }
            if (!File.Exists(path)) { MessageBox.Show(process + " " + "Executable" + " not found. Please check the path in the ini file."); return; }
            launchProcess(path);
        }
        private void launchProcess(string process) { Process.Start(new ProcessStartInfo(process) { UseShellExecute = true }); }
        private void button1_Click(object sender, EventArgs e) { startProcess("Game", ExecutablePath); Close(); }
        private void button2_Click(object sender, EventArgs e) { newForm(new RegistryEditor(IniPath)); }
        private void button3_Click(object sender, EventArgs e) { startProcess("Editor", EditorPath); Close(); }
        private void button4_Click(object sender, EventArgs e) { launchProcess(RecommendedFixSite); }
        private void button5_Click(object sender, EventArgs e) { launchProcess(EditorFixSite); }
        private void button6_Click(object sender, EventArgs e) { newForm(new ModsForm(IniPath)); }
        private void button7_Click(object sender, EventArgs e)
        {
            //string iniPath = GameINI.Contains(":") ? Path.Combine(InstallLocation, GameINI) : GameINI;
            string iniPath = GameINI.Contains(":") ? GameINI : Path.Combine(InstallLocation, GameINI);
            if (File.Exists(iniPath)) { launchProcess(iniPath); }
            else { MessageBox.Show("Game INI file not found."); }
        }
    }
}