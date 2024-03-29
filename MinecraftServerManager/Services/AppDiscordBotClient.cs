﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Microsoft.Extensions.Options;

namespace MinecraftServerManager.Services
{
    public class AppDiscordBotClient
    {
        private const string ChannelName = "minecraft-management-log";
        private readonly IOptions<AppConfig> _config;

        public AppDiscordBotClient(IOptions<AppConfig> config)
        {
            _config = config;
        }

        public async Task SendMessageToHereAsync(string message)
        {
            using DiscordRestClient client = new();
            await client.LoginAsync(TokenType.Bot, _config.Value.DiscordBotAccessToken);
            try
            {
                var guilds = await client.GetGuildsAsync();
                var tasks = guilds.Select(g => SendMessageToHereAsync(g, message));
                await Task.WhenAll(tasks);
            }
            finally
            {
                await client.LogoutAsync();
            }
        }

        private static async Task SendMessageToHereAsync(RestGuild guild, string message)
        {
            var channel = await GetBotChannelAsync(guild);
            if (channel == null)
            {
                return;
            }

            await channel.SendMessageAsync($"@here {Environment.NewLine}{message}");
        }

        private static async Task<RestTextChannel?> GetBotChannelAsync(RestGuild guild)
        {
            var channels = await guild.GetTextChannelsAsync();
            var targetChannel = channels.FirstOrDefault(x => x.Name == ChannelName);

            return targetChannel;
        }
    }
}
