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
        public const string SettingsFileName = "settings.json";

        public readonly string ProgramName;
        public readonly string Version;
        public string BaseCodePath => Path.Combine(Directory.GetCurrentDirectory(), "BaseCode.cs");
        public string TargetCodeFilePath { get; }
        public string SettingsFilePath => Path.Combine(Directory.GetCurrentDirectory(), SettingsFileName);

        // private string targetFileName;

        private static void Main()
        {
            var program = new Program();
            program.Run();
        }

        public Program()
        {
            var assembly = Assembly.GetExecutingAssembly().GetName();
            ProgramName = assembly.Name;
            var version = assembly.Version;
            Version = $"v{version.Major}.{version.Minor}";
        }

        public async void Run()
        {
            Console.WriteLine(FiggleFonts.Standard.Render("BaseCodeGenerator"));
            Console.WriteLine($"[{ProgramName}] - {Version}");
            for (int i = 0; i < Version.Length; i++) Console.Write('-');
            Console.WriteLine();

            bool check = File.Exists(SettingsFilePath);
            ProgramSettings settings = null;
            if (check)
            {
                settings = JsonConvert.DeserializeObject<ProgramSettings>(File.ReadAllText(SettingsFilePath));
            }
            if (!check || settings is null)
            {
                settings = SettingsDialog();
            }

            (bool success, string baseCode) result = await GetBaseCodeFromFileAsync();
            if (!result.success) return;
        }

        public ProgramSettings SettingsDialog()
        {
            ProgramSettings settings = new ProgramSettings();
            return settings;
        }

        public async Task<(bool, string)> GetBaseCodeFromFileAsync()
        {
            var baseCodePath = Path.Combine(Directory.GetCurrentDirectory(), "BaseCode.cs");
            if (!File.Exists(baseCodePath))
            {
                Console.WriteLine("Base code file doesn't exist.");
                return (false, null);
            }

            string baseCode = null;
            (bool, string) result;
            try
            {
                using StreamReader reader = new StreamReader(new FileStream(baseCodePath, FileMode.Open, FileAccess.Read));
                baseCode = await reader.ReadToEndAsync();
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
