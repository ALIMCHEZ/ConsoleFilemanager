using ConsoleFilemanager.Services;
using ConsoleFilemanager.UI;

namespace ConsoleFilemanager
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            UserInterface ui = new UserInterface();
            await ui.RunAsync("C:\\Users\\5\\Documents");
        }
    }
}
