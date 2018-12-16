using DC_ConnorBot.Discord;
using DC_ConnorBot.Discord.Entities;
using DC_ConnorBot.Storage;
using Discord.Commands;
using Discord;
using System;
using System.Threading.Tasks;
using DC_ConnorBot.Discord.Commands;

namespace DC_ConnorBot
{
    class ConnorBot_CORE
    {
        private readonly IDataStorage _storage;
        private readonly Connection _connection;
       

        public ConnorBot_CORE(IDataStorage storage, Connection connection)
        {
            _storage = storage;
            _connection = connection;
        }

        public async Task Start()
        {
            Console.WriteLine("CONNOR BOT INITIALIZING...");
            
            await _connection.ConnectAsync(new ConnorBotConfig
            {
                Token = _storage.RestoreObject<string>("Config/BotToken")
            });

            var CommReceiever = Unity.Resolve<CommandReceiver>();
            await CommReceiever.Start();



        }
    }
}
