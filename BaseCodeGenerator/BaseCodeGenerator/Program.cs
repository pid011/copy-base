using Figgle;

using Newtonsoft.Json;

using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace BaseCodeGenerator
{
    //TODO: Custom I/O (Write, Read to Console)
    internal class Program
    {
        private const string SettingsFileName = "settings.json";

        private readonly string programName;
        private readonly string version;

        private ProgramSettings Settings;
        private string SettingsFilePath => Path.Combine(Directory.GetCurrentDirectory(), SettingsFileName);

        private static void Main(string[] arg)
        {
            var program = new Program();
            program.Run(arg);
        }

        public Program()
        {
            var assembly = Assembly.GetExecutingAssembly().GetName();
            this.programName = assembly.Name;
            var version = assembly.Version;
            this.version = $"v{version.Major}.{version.Minor}";
        }

        public void Run(string[] arg)
        {
            Console.WriteLine(FiggleFonts.Standard.Render("BaseCodeGenerator"));
            var name = $"[{this.programName}] - {this.version}";
            Console.WriteLine(name);
            DisplayTextLine(name.Length);
            Console.WriteLine();

            if (arg.Length == 1 && arg[0] == "setup")
            {
                SetupProcess();
                return;
            }

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
                Console.WriteLine("Settings file doesn't exist. Start the setup process.");
                this.Settings = SetupProcess();
            }

            if (!File.Exists(this.Settings.BaseCodeFilePath))
            {
                Console.WriteLine("The base code file doesn't exist.");
                Console.WriteLine($"Current base code file: {this.Settings.BaseCodeFilePath}");
                bool isSetupStart = UserChoiceProcess("Do you want to start the setup process?");
                if (isSetupStart)
                {
                    this.Settings = SetupProcess();
                }
                else
                {
                    return;
                }
            }

            DisplaySettingsInfo();

            Console.WriteLine("If you want to setup file path, add command argument [setup] when execute this program.");
            Console.WriteLine("e.g. [BaseCodeGenerator.exe setup]");
            Console.WriteLine();
            bool choose = UserChoiceProcess("Are you sure you want to copy the base code?");
            if (!choose) return;
            try
            {
                File.Copy(this.Settings.BaseCodeFilePath, this.Settings.TargetCodeFilePath, true);
                Console.WriteLine("Copy completed!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
#if DEBUG
                throw;
#endif
            }
        }

        private void DisplayTextLine(int repeat)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < repeat; i++)
            {
                builder.Append('=');
            }

            Console.WriteLine(builder.ToString());
        }

        private void DisplaySettingsInfo()
        {
            DisplaySettingsInfo(this.Settings);
        }

        private void DisplaySettingsInfo(ProgramSettings settings)
        {
            string baseCodeFilePath = settings.BaseCodeFilePath;
            string targetCodeFilePath = settings.TargetCodeFilePath;

            var baseCodeFileMsg = $"Base code file: {baseCodeFilePath}";
            var targetCodeFileMsg = $"Target code file: {targetCodeFilePath}";

            var repeat = baseCodeFileMsg.Length > targetCodeFileMsg.Length ? baseCodeFileMsg.Length : targetCodeFileMsg.Length;

            DisplayTextLine(repeat);
            Console.WriteLine(baseCodeFileMsg);
            Console.WriteLine(targetCodeFileMsg);
            DisplayTextLine(repeat);
        }

        private bool UserChoiceProcess(string msg)
        {
            Console.WriteLine(msg);
            bool chooseYes = false;
            bool chooseNo = false;

            while (!chooseYes && !chooseNo)
            {
                Console.Write("(y/n): ");
                var choose = Console.ReadLine().ToLower();
                switch (choose)
                {
                    case "y":
                    case "yes":
                        chooseYes = true;
                        break;

                    case "n":
                    case "no":
                        chooseNo = true;
                        break;
                }
            }
            return chooseYes;
        }

        private ProgramSettings SetupProcess()
        {
            Console.WriteLine("You can press Ctrl + C to exit the setup.");
            var settings = new ProgramSettings();
            while (true)
            {
                settings.BaseCodeFilePath = GetValidPathFromUser("Please enter the base code file path", false);
                settings.TargetCodeFilePath = GetValidPathFromUser("Please enter the file path to generate the base code", true);
                DisplaySettingsInfo(settings);
                if (UserChoiceProcess("Is this right? If you want to restart the setup, say 'no'."))
                {
                    break;
                }
                DisplayTextLine(5);
            }

            using (StreamWriter writer = File.CreateText(this.SettingsFilePath))
            {
                new JsonSerializer().Serialize(writer, settings);
            }
            Console.WriteLine("Setup completed!");
            var msg = "Settings File is always created in the directory where this program is located.";
            Console.WriteLine(msg);
            DisplayTextLine(msg.Length);
            return settings;
        }

        private string GetValidPathFromUser(string msg, bool createFile)
        {
            string input;
            while (true)
            {
                Console.WriteLine($"{msg}:");
                input = Console.ReadLine();
                if (!File.Exists(input))
                {
                    if (createFile)
                    {
                        var directoryName = Path.GetDirectoryName(input);
                        if (Directory.Exists(directoryName))
                        {
                            Console.WriteLine("Target file doesn't exist.");
                            Console.WriteLine("But you can create a file with the following path:");
                            Console.WriteLine(input);
                            DisplayTextLine(input.Length);
                            bool isCreateFile = UserChoiceProcess("Do you want to create a file with that path?");
                            if (isCreateFile)
                            {
                                File.Create(input);
                                Console.WriteLine("File created.");
                                break;
                            }
                        }
                    }

                    Console.WriteLine("The file path is not valid.");
                    continue;
                }
                break;
            }
            return input;
        }
    }

    internal class ProgramSettings
    {
        public string BaseCodeFilePath { get; set; }
        public string TargetCodeFilePath { get; set; }
    }
}