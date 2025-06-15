using IniParser;
using Microsoft.Win32;

namespace QuickTick
{
    public partial class RegistryEditor : Form
    {
        private IniFile iniFile;
        private List<RegistryEntry> keys = new List<RegistryEntry>();
        private int lastSelectedIndex = 0;
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
                keys.Add(new RegistryEntry { Key = key, Path = path, Name = valueName, Kind = kind, Value = val });
            }
            foreach (string subKeyName in key.GetSubKeyNames())
            {
                listBox1.Items.Add($"{indent}[{subKeyName}]");
                keys.Add(new RegistryEntry { Name = subKeyName });
                using var subKey = key.OpenSubKey(subKeyName);
                if (subKey != null) { TraverseRegistryKey(subKey, path + "\\" + subKeyName, indent + "  "); }
            }
        }
        private void registryCompare(RegistryKey key, string entry, object value, RegistryValueKind type) { if (key.GetValue(entry)! != value) { key.SetValue(entry, value, type); } }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Save previous entry before switching
            updateValue();
            RegistryEntry selectedKey = keys[listBox1.SelectedIndex];
            if (selectedKey.Path == string.Empty)
            {
                textBox2.Text = "Key";
                textBox3.Text = "";
                textBox3.ReadOnly = true;
                textBox3.Visible = true;
                numericUpDown1.Visible = false;
            }
            else
            {
                textBox2.Text = selectedKey.Kind.ToString();
                textBox3.Text = selectedKey.Value.ToString();
                textBox3.ReadOnly = false;
                textBox3.Visible = false;
                numericUpDown1.Visible = false;
                switch (selectedKey.Kind)
                {
                    case RegistryValueKind.String:
                        textBox3.Visible = true;
                        // Use textBox
                        break;
                    case RegistryValueKind.ExpandString:
                        textBox3.Visible = true;
                        // Use textBox
                        break;
                    case RegistryValueKind.DWord:
                        numericUpDown1.Visible = true;
                        numericUpDown1.Maximum = uint.MaxValue; // 32-bit unsigned
                        numericUpDown1.Value = Convert.ToDecimal(Convert.ToUInt32(selectedKey.Value));
                        // Use numericUpDown
                        break;
                    case RegistryValueKind.QWord:
                        numericUpDown1.Visible = true;
                        numericUpDown1.Maximum = ulong.MaxValue; // safest fallback
                        numericUpDown1.Value = Convert.ToDecimal(Convert.ToUInt64(selectedKey.Value));
                        // Use numericUpDown
                        break;
                    case RegistryValueKind.MultiString:
                        //listBoxMultiString.Visible = true;
                        // Use multiline list or textbox
                        break;
                    case RegistryValueKind.Binary:
                        // Hex input? Maybe readonly.
                        break;
                    default:
                        // Fallback
                        break;
                }
            }
            textBox1.Text = selectedKey.Name;
            lastSelectedIndex = listBox1.SelectedIndex;
        }
        private void RegistryEditor_FormClosing(object sender, FormClosingEventArgs e) { updateValue(); }
        private void updateValue()
        {
            if (lastSelectedIndex >= 0 && keys[listBox1.SelectedIndex].Path != string.Empty)
            {
                RegistryEntry selectedKey = keys[listBox1.SelectedIndex];
                switch (selectedKey.Kind)
                {
                    case RegistryValueKind.String:
                    case RegistryValueKind.ExpandString:
                        registryCompare(selectedKey.Key, selectedKey.Name, textBox3.Text, selectedKey.Kind);
                        selectedKey.Value = textBox3.Text; // update value in the list
                        break;
                    case RegistryValueKind.DWord:
                    case RegistryValueKind.QWord:
                        registryCompare(selectedKey.Key, selectedKey.Name, numericUpDown1.Value, selectedKey.Kind);
                        selectedKey.Value = numericUpDown1.Value; // update value in the list
                        break;
                    case RegistryValueKind.MultiString:
                        // Handle MultiString if needed
                        break;
                    case RegistryValueKind.Binary:
                        // Handle Binary if needed
                        break;
                    default:
                        // Fallback
                        break;
                }
            }
        }
    }
}
