using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CopyBase.Settings
{
    internal static class CopyBaseSettings
    {
        public const string SettingsFileName = "copy-base-settings.json";

        public static List<CopyBasePathItem> Items { get; set; }

        public static void LoadFromFile()
        {
            if (!File.Exists(SettingsFileName))
            {
                Items = new List<CopyBasePathItem>();
                SaveToFile();
                return;
            }
            var json = File.ReadAllText(SettingsFileName);
            Items = JsonConvert.DeserializeObject<List<CopyBasePathItem>>(json);
        }

        public static void SaveToFile()
        {
            var json = JsonConvert.SerializeObject(Items, Formatting.Indented);
            File.WriteAllText(SettingsFileName, json);
        }
    }

    internal class CopyBasePathItem
    {
        public string Alias { get; set; }
        public FilePathElement Element { get; set; }

        public class FilePathElement
        {
            public string BaseFilePath { get; set; }
            public string TargetFilePath { get; set; }
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine($"[{Alias}]");
            output.AppendLine($"\tBase file path: {Element.BaseFilePath}");
            output.AppendLine($"\tTarget file path: {Element.TargetFilePath}");
            return output.ToString();
        }
    }
}