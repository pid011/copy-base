// Copyright (c) Sepi. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace CopyBase.Settings
{
    internal static class CopyBaseSettings
    {
        public const string SettingsFileName = "copy-base-settings.json";

        public static List<CopyBasePathItem> Items { get; set; }


        public static string SettingsFilePath
        {
            get
            {
                if (settingsFilePath == null)
                {
                    settingsFilePath = GetSettingsFilePath();
                }

                return settingsFilePath;
            }
        }

        private static string settingsFilePath;

        public static void LoadFromFile()
        {
            if (!File.Exists(SettingsFilePath))
            {
                Items = new List<CopyBasePathItem>();
                SaveToFile();
                return;
            }
            var json = File.ReadAllText(SettingsFilePath);
            Items = JsonConvert.DeserializeObject<List<CopyBasePathItem>>(json);
        }

        public static void SaveToFile()
        {
            var json = JsonConvert.SerializeObject(Items, Formatting.Indented);
            File.WriteAllText(SettingsFilePath, json);
        }

        private static string GetSettingsFilePath()
        {
            var executeLocation = Assembly.GetExecutingAssembly().Location;
            Debug.WriteLine($"executeLocation is {Assembly.GetExecutingAssembly().Location}");
            var dir = Directory.GetParent(executeLocation).FullName;
            Debug.WriteLine($"dir name is {Assembly.GetExecutingAssembly().Location}");
            return Path.Combine(dir, SettingsFileName);
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
            output.AppendLine($"- [{this.Alias}]");
            output.AppendLine($"  Base file path: [{this.Element.BaseFilePath}]");
            output.AppendLine($"  Target file path: [{this.Element.TargetFilePath}]");
            return output.ToString();
        }
    }
}
