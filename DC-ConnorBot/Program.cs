using System;
using System.Threading.Tasks;

namespace DC_ConnorBot
{
    internal class Program
    {
        private static async Task Main()
        {
            var ConnorBot = Unity.Resolve<ConnorBot_CORE>();
            await ConnorBot.Start();
            Console.ReadKey();
        }
    }
    
}
