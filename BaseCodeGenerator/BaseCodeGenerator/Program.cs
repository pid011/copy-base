using Figgle;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace BaseCodeGenerator
{
    class Program
    {
        private const string SettingsFileName = "settings.json";

        private readonly string programName;
        private readonly string version;

        private ProgramSettings Settings;
        private string SettingsFilePath => Path.Combine(Directory.GetCurrentDirectory(), SettingsFileName);

        private static async Task Main()
        {
            var program = new Program();
            await program.Run();
        }

        public Program()
        {
            var assembly = Assembly.GetExecutingAssembly().GetName();
            this.programName = assembly.Name;
            var version = assembly.Version;
            this.version = $"v{version.Major}.{version.Minor}";
        }

        public async Task Run()
        {
            Console.WriteLine(FiggleFonts.Standard.Render("BaseCodeGenerator"));
            Console.WriteLine($"[{this.programName}] - {this.version}");
            for (int i = 0; i < this.version.Length; i++) Console.Write('-');
            Console.WriteLine();

            bool check = File.Exists(this.SettingsFilePath);
            this.Settings = new ProgramSettings();
            if (check)
            {
                using (StreamReader reader = File.OpenText(this.SettingsFilePath))
                {
                    this.Settings = (ProgramSettings)new JsonSerializer().Deserialize(reader, typeof(ProgramSettings));
                }
            }
            if (!check || this.Settings is null)
            {
                this.Settings = SetupProcess();
            }

            (bool success, string baseCode) result = await GetBaseCodeFromFileAsync();
            if (!result.success) return;

            Console.WriteLine(result.baseCode);
        }

        private ProgramSettings SetupProcess()
        {
            Console.WriteLine("You can press Ctrl + C to exit the setup.");
            var settings = new ProgramSettings
            {
                BaseCodeFilePath = GetValidPathFromUser("Please enter the base code file path"),
                TargetCodeFilePath = GetValidPathFromUser("Please enter the file path to generate the base code")
            };

            using (StreamWriter writer = File.CreateText(this.SettingsFilePath))
            {
                new JsonSerializer().Serialize(writer, settings);
            }

            return settings;
        }

        private string GetValidPathFromUser(string text)
        {
            string input;
            while (true)
            {
                Console.WriteLine($"{text}:");
                input = Console.ReadLine();
                if (!File.Exists(input))
                {
                    Console.WriteLine("The file path is not valid.");
                    continue;
                }
                break;
            }
            return input;
        }

        private async Task<(bool, string)> GetBaseCodeFromFileAsync()
        {
            if (!File.Exists(this.Settings.BaseCodeFilePath))
            {
                Console.WriteLine("Base code file doesn't exist.");
                return (false, null);
            }

            string baseCode = null;
            (bool, string) result;
            try
            {
                using (StreamReader reader = File.OpenText(this.Settings.BaseCodeFilePath))
                {
                    baseCode = await reader.ReadToEndAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
#if DEBUG
                throw;
#endif
            }
            finally
            {
                result = (baseCode is null) ? (false, null) : (true, baseCode);
            }

            return result;
        }
    }

    class ProgramSettings
    {
        public string BaseCodeFilePath { get; set; }
        public string TargetCodeFilePath { get; set; }
    }
}
