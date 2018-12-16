using Discord.Commands;
using CSharpOsu;
using CSharpOsu.Module;
using System.Threading.Tasks;

using System;
using Discord;

namespace DC_ConnorBot.Discord.Commands
{
    [Group("osu!")]
    public class OsuRelated : ModuleBase<SocketCommandContext>
    {
        private OsuClient _osuClient;
        public OsuRelated()
        {
            _osuClient = new OsuClient("e67076e1c5bfe86de61c3faa4eb109c507192afd");
        }


        [Command]
        public async Task osuHelp()
        {
            await ReplyAsync("Osu Help! blablablabla");
        }

        [Command(), Summary("Returns the account of the specified user")]
        public async Task UserDetails(string osuUsername)
        {
            OsuUser[] _osuUser = _osuClient.GetUser(osuUsername);
            OsuUser User = _osuUser[0];
            
            var builder = new EmbedBuilder()
                .WithTitle(User.username + " #" + User.pp_rank)
                .WithDescription(User.osuchan)
                .WithUrl(User.url)
                .WithColor(new Color(0xDC98A4))
                .WithThumbnailUrl(User.image)
                .AddInlineField("<:osu:518082419195117584>", "o!Standard")
                .AddInlineField(":taiko:", "o!Mania")
                .AddInlineField(":ctb:", "o!Taiko")
                .AddInlineField(":mania:", "o!CTB")

                .WithTimestamp(DateTimeOffset.FromUnixTimeMilliseconds(1543589102163))
                .WithFooter(footer => {
                     footer
                         .WithText("Osu! API")
                         .WithIconUrl("https://github.com/Xferno2/CSharpOsu");
                })
                ;
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync("",
                embed: embed)
                .ConfigureAwait(false);
            
            //Console.WriteLine();
            //await Context.Channel.SendMessageAsync("Osu! API: " + _osuUser[0].username + " " + _osuUser[0].country);
        }
    }
}
//:osu: :taiko: :ctb: :mania:
