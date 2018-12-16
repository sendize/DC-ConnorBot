using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DC_ConnorBot.Discord.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("Hello"), Alias("Hey Connor"), Summary("Connor greeting!")]
        public async Task Greet()
        {
            Console.WriteLine(Context.User.Id);
            //308126363099987970 natsu
            if (Context.User.Id == 232112397190561792)
            {
                await Context.Channel.SendMessageAsync("Hello, Lieutenant Sen.");
            }
            else if(Context.User.Id == 308126363099987970)
            {
                await Context.Channel.SendMessageAsync("I'm sorry but you're wanted for being a Pedophile.");
                await Context.Channel.SendMessageAsync("I do not condone any form of criminality, Mr. " + Context.User.Username + ".");
            }
            else
            {
                await Context.Channel.SendMessageAsync("Hello. My name is Connor! I'm the android sent by Sen! " + Context.User.Id);
            }
        }

        [Command("Help"), Alias("Help"), Summary("Connor greeting!")]
        public async Task Help()
        {
            await Context.Channel.SendMessageAsync("Yeah, I can't really do much at this point.");
            await Context.Channel.SendMessageAsync(":<");
        }

        [Command("Test"), Alias("Test"), Summary("Connor greeting!")]
        public async Task Test()
        {
            await Context.Channel.SendMessageAsync("What are you testing there, bud?");
        }

        [Command("Nice"), Alias("Hey Connor"), Summary("Connor greeting!")]
        public async Task Nice()
        {
            await Context.Channel.SendMessageAsync("Very nice indeed.");
        }


    }
}
