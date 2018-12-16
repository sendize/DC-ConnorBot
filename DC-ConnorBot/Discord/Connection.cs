using DC_ConnorBot.Discord.Commands;
using DC_ConnorBot.Discord.Entities;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;


namespace DC_ConnorBot.Discord
{
    class Connection
    {
        private readonly DiscordSocketClient _client;
        private readonly DiscordLogger _DLogger;
        

        public Connection(DiscordLogger DLogger, DiscordSocketClient client)
        {       
            _DLogger = DLogger;
            _client = client;
        }

        internal async Task ConnectAsync(ConnorBotConfig config)
        {
            _client.Log += _DLogger.Log;
            //_client.MessageReceived += client_messsagerecieved;
            
            await _client.LoginAsync(TokenType.Bot, config.Token);
            await _client.StartAsync();
            await _client.SetGameAsync("RK800 being Developed OwO", "https//www.fb.com/IanThePie.XD", StreamType.NotStreaming);


            await Task.Delay(2000);
        }
        
    }
}
