﻿using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Linq;
using AdMortemBot.Logging;

namespace AdMortemBot.Commands
{
    public class Information : ModuleBase<SocketCommandContext>
    {

        [Command("avatar")]
        private async Task PullAvatarAsync(IGuildUser user)
        {
            try
            {
                string avatarURL = user.GetAvatarUrl(format: ImageFormat.Auto, 1024);

                if (avatarURL is null)
                {
                    await Context.Message.Channel.SendMessageAsync($"{user.Mention} does not have a profile picture");
                    return;
                }
                var embed = new EmbedBuilder();
                embed.WithColor(new Color(0, 204, 255));
                embed.WithTitle($"{user.Username}'s avatar");
                embed.WithUrl(avatarURL);
                embed.WithImageUrl(avatarURL);

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            catch (Exception ex)
            {
                LogMessage msg = new LogMessage(LogSeverity.Error, $"{ GetType().FullName }: { Logger.GetAsyncMethodName()}", ex.Message, ex);

                await Logger.LogMessageAsync(msg);
            }
        }

        [Command("RestartBot")]
        private async Task RestartBot()
        {
            try
            {
                await Context.Channel.SendMessageAsync("Restarting bot", false);
                await Program.RestartBot();
            }
            catch (Exception ex)
            {
                LogMessage msg = new LogMessage(LogSeverity.Error, $"{ GetType().FullName }: { Logger.GetAsyncMethodName()}", ex.Message, ex);

                await Logger.LogMessageAsync(msg);

            }
        }

    }
}
