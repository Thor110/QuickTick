using IniParser;
using System.Diagnostics;

namespace QuickTick
{
    public partial class MainForm : Form
    {
        private List<Games> entries = new List<Games>();
        public string GamePath = string.Empty;
        private static readonly IniFile MyIni = new IniFile("games.ini");
        public MainForm()
        {
            InitializeComponent();
            if (!File.Exists("games.ini")) { MessageBox.Show("games.ini is missing."); return; }
            for (int i = 0; i < 999; i++)
            {
                string entry = "Game" + i.ToString();
                if (MyIni.KeyExists(entry, "QuickTick"))
                {
                    string name = MyIni.Read(entry, "QuickTick");
                    if (!File.Exists($"{name}")) { MessageBox.Show($"{name} is missing."); }
                    else
                    {
                        string game = new IniFile($"{name}").Read("Name", "Game");
                        listBox1.Items.Add(game);
                        entries.Add(new Games { Name = game, Path = name });
                    }
                }
                else { break; } // No more games to read
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) { MessageBox.Show("Please select a game to launch."); return; }
            if (!File.Exists(GamePath)) { MessageBox.Show("Game executable not found. Please check the path in the ini file."); return; }
            Process.Start(GamePath);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Games game in entries)
            {
                if (game.Name == listBox1.SelectedItem!.ToString())
                {
                    GamePath = new IniFile($"{game.Path}").Read("Executable", "Game");
                    break;
                }
            }
        }
    }
}
