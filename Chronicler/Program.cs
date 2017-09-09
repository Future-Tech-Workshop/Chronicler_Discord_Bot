using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SQLite;

namespace Chronicler
{
    public class Program
    {
        private DiscordSocketClient _client;
        private string cmd_prefix;
        private SQLiteConnection m_dbConnection;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            cmd_prefix = "[|] ";

            m_dbConnection = new SQLiteConnection("Data Source=chronicler.db; Version=3;");
            m_dbConnection.Open();

            _client = new DiscordSocketClient();

            _client.Log += Log;
            _client.MessageReceived += MessageReceived;

            string token = "MzU0MDI5NzY5MDM1MjE4OTQ1.DI8jYA.a6pbLuPAOhspXqcdpDolHih-6nI"; // Remember to keep this private!
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            LogMessage(message);

            if (message.Content.StartsWith(cmd_prefix))
            {
                ProcessCommand(message);
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public void ProcessCommand(SocketMessage message)
        {
            
        }

        public void LogMessage(SocketMessage message)
        {
            Console.WriteLine("Writing message " + message.Id.ToString() + " to database.");
            string sql = "insert into message_log (message_id, message_text, server_id, server_name, channel_id, channel_name, user_id, user_name, timestamp) values ('"
                + message.Id.ToString() + "', "
                + message.Content + "', "
                + (message.Author as SocketGuildUser)?.Guild.Id.ToString() + "', "
                + (message.Author as SocketGuildUser)?.Guild.Name + "', "
                + message.Channel.Id.ToString() + "', "
                + message.Channel.Name + "', "
                + (message.Author as SocketGuildUser)?.Id.ToString() + "', "
                + message.Author.Username + "', "
                + message.Timestamp.ToString() + "')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            Console.WriteLine("Does this compile to here?");
            command.ExecuteNonQuery();
        }
    }
}

/* *
 * https://www.flaticon.com/authors/vectors-market
 * */
