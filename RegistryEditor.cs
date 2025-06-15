using IniParser;
using Microsoft.Win32;

namespace QuickTick
{
    public partial class RegistryEditor : Form
    {
        private IniFile iniFile;
        private List<RegistryEntry> keys = new List<RegistryEntry>();
        public RegistryEditor(string path)
        {
            InitializeComponent();
            iniFile = new IniFile($"{path}");
            string RegistryRoot = iniFile.Read("RegistryRoot", "Game");
            PopulateRegistryTree(RegistryRoot);
        }
        private static (RegistryHive hive, string subKey) ParseRegistryPath(string registryPath)
        {
            string[] parts = registryPath.Split('\\', 2);
            RegistryHive hive = parts[0] switch
            {
                "HKLM" or "HKEY_LOCAL_MACHINE" => RegistryHive.LocalMachine,
                "HKCU" or "HKEY_CURRENT_USER" => RegistryHive.CurrentUser,
                _ => throw new InvalidOperationException("Unsupported registry hive."),
            };
            return (hive, parts[1]);
        }
        private void PopulateRegistryTree(string fullPath)
        {
            try
            {
                var (hive, subKeyPath) = ParseRegistryPath(fullPath);
                RegistryKey baseKey = RegistryKey.OpenBaseKey(hive, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Default);
                using var rootKey = baseKey.OpenSubKey(subKeyPath);
                listBox1.Items.Clear();
                if (rootKey == null) { listBox1.Items.Add("Registry key not found."); return; }
                TraverseRegistryKey(rootKey, subKeyPath);
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }
        private void TraverseRegistryKey(RegistryKey key, string path, string indent = "")
        {
            foreach (string valueName in key.GetValueNames())
            {
                object val = key.GetValue(valueName) ?? "(null)";
                RegistryValueKind kind = key.GetValueKind(valueName);
                listBox1.Items.Add($"{indent}{valueName}");
                keys.Add(new RegistryEntry { Key = key, Path = path, Name = valueName, Kind = kind, Value = val } );
            }
            foreach (string subKeyName in key.GetSubKeyNames())
            {
                listBox1.Items.Add($"{indent}[{subKeyName}]");
                keys.Add(new RegistryEntry { Name = subKeyName } );
                using var subKey = key.OpenSubKey(subKeyName);
                if (subKey != null) { TraverseRegistryKey(subKey, path + "\\" + subKeyName, indent + "  "); }
            }
        }
        private void registryCompare(RegistryKey key, string entry, string value) { if ((string)key.GetValue(entry)! != value) { key.SetValue(entry, value); } }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(keys[listBox1.SelectedIndex].Path == string.Empty)
            {
                textBox2.Text = "Key";
                textBox3.Text = "";
                textBox3.ReadOnly = true;
            }
            else
            {
                textBox2.Text = keys[listBox1.SelectedIndex].Kind.ToString();
                textBox3.Text = keys[listBox1.SelectedIndex].Value.ToString();
                textBox3.ReadOnly = false;
            }
            textBox1.Text = keys[listBox1.SelectedIndex].Name;
        }
    }
}
