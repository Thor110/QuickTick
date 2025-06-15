using Microsoft.Win32;

namespace QuickTick
{
    public class Games
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
    }
    public class RegistryEntry
    {
        public RegistryKey Key { get; set; } = null!;
        public string Path { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public RegistryValueKind Kind { get; set; }
        public object Value { get; set; } = null!;
    }
}
