using DC_ConnorBot.Discord.Commands;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DC_ConnorBot.Discord
{
    class CommandReceiver
    {
        private readonly DiscordSocketClient _client;
        private CommandService Commands;
        
        public CommandReceiver(DiscordSocketClient client)
        {
            _client = client;

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });
        }

        public async Task Start()
        {
            _client.MessageReceived += Client_MessageReceived;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());
            Console.WriteLine("[INITIALIZATION] Ready to take in commands!");
        }

        private async Task Client_MessageReceived(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(_client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;
            //if (!(Message.HasStringPrefix("a!", ref ArgPos) || Message.HasMentionPrefix(_client.CurrentUser, ref ArgPos))) return;


            var Result = await Commands.ExecuteAsync(Context, ArgPos);
        
            if (!Result.IsSuccess)
            {
                Console.WriteLine($"{DateTime.Now} at Commands | Error executing COMMAND. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
            }
        }
    }
}
