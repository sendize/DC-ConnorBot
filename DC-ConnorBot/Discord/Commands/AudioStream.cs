using Discord;
using Discord.Audio;
using Discord.Commands;
using DC_ConnorBot.Discord;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace DC_ConnorBot.Discord.Commands
{
    public class AudioStream : ModuleBase<SocketCommandContext>
    {
        private readonly AudioService _service = Unity.Resolve<AudioService>();



        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinCmd()
        {
            IVoiceChannel user_vchannel = (Context.User as IVoiceState).VoiceChannel;

            if (user_vchannel != null)
            {
                await Context.Channel.SendMessageAsync($"Attempting to join: {user_vchannel.Name}");
                await _service.JoinAudio(Context.Guild, user_vchannel);
            }
            else
            {
                await Context.Channel.SendMessageAsync("You're not in a voice channel!");
            }
        }

        [Command("leave", RunMode = RunMode.Async)]
        public async Task LeaveCmd()
        {
            await _service.LeaveAudio(Context.Guild);
        }

        [Command("play", RunMode = RunMode.Async)]
        public async Task PlayCmd([Remainder] string song)
        {
            await _service.SendAudioAsync(Context.Guild, Context.Channel, song);
        }

        [Command("stop", RunMode = RunMode.Async)]
        public async Task StopCmd([Remainder] string song)
        {
            await _service.StopFileStream();
        }




    }


    public class AudioService
    {
        private CancellationTokenSource FileStreamCT = new CancellationTokenSource();
        private readonly ConcurrentDictionary<ulong, IAudioClient> ConnectedChannels = new ConcurrentDictionary<ulong, IAudioClient>();


        public async Task JoinAudio(IGuild guild, IVoiceChannel target)
        {
            IAudioClient client;
            if (ConnectedChannels.TryGetValue(guild.Id, out client))
            {
                return;
            }
            if (target.Guild.Id != guild.Id)
            {
                return;
            }

            var audioClient = await target.ConnectAsync();

            if (ConnectedChannels.TryAdd(guild.Id, audioClient))
            {
                // If you add a method to log happenings from this service,
                // you can uncomment these commented lines to make use of that.
                //await Log(LogSeverity.Info, $"Connected to voice on {guild.Name}.");
            }
        }

        public async Task LeaveAudio(IGuild guild)
        {
            IAudioClient client;
            if (ConnectedChannels.TryRemove(guild.Id, out client))
            {
                await client.StopAsync();
                //await Log(LogSeverity.Info, $"Disconnected from voice on {guild.Name}.");
            }
        }

        public async Task StopFileStream()
        {
            FileStreamCT.Cancel();

        }

        public async Task SendAudioAsync(IGuild guild, IMessageChannel channel, string path)
        {
            // Your task: Get a full path to the file if the value of 'path' is only a filename.
            if (!File.Exists(path))
            {
                await channel.SendMessageAsync("File does not exist.");
                return;
            }
            IAudioClient client;

            if (ConnectedChannels.TryGetValue(guild.Id, out client))
            {
                await channel.SendMessageAsync("PLaying!");
                

                try
                {
                    using (var ffmpeg = CreateProcess(path))
                    using (var stream = client.CreatePCMStream(AudioApplication.Music))
                    {
                        try { await ffmpeg.StandardOutput.BaseStream.CopyToAsync(stream, FileStreamCT.Token); }
                        catch (Exception e) { Console.WriteLine($"{e.ToString()}"); }
                        finally { await stream.FlushAsync(); }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.ToString()}");
                }
                //await Log(LogSeverity.Debug, $"Starting playback of {path} in {guild.Name}");

            }
        }

        private Process CreateProcess(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg.exe",
                Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = false
            });
        }
    }
}

