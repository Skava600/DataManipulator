using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManipulatorConsole
{
    internal class ResolverConfig
    {
        private const string file = "appsettings.json";
        public IConfiguration Configuration { get; }

        public ResolverConfig()
        {
            this.Configuration = new ConfigurationBuilder()
                .SetBasePath(GetAppSettingsPath())
                .AddJsonFile(file)
                .AddEnvironmentVariables()
                .Build();
        }

        private string GetAppSettingsPath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string? projectDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent?.FullName;
            if (Directory.GetFiles(currentDirectory).Any(f => Path.GetFileName(f) == file))
            {
                return currentDirectory;
            }
            else if (projectDirectory is not null && Directory.GetFiles(projectDirectory).Any(f => Path.GetFileName(f) == file))
            {
                return projectDirectory;
            }

            throw new ArgumentException("There is no appsettings.json");
        }
    }
}
