using Discord;
using System;
using System.Threading.Tasks;

namespace FTW_Chronicler_Bot
{
    class Chronicler
    {
        public static void Main(string[] args)
            => new Chronicler().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
