using Microsoft.Extensions.Configuration;
using DataManipulatorConsole.Services;

namespace DataManipulatorConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ResolverConfig().Configuration;
            Settings settings = config.GetRequiredSection("Settings").Get<Settings>();
            FileDataService fileDataService = new FileDataService();

            await fileDataService.ResolveAll(settings);
        }

    }
}