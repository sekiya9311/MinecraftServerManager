namespace MinecraftServerManager.Services
{
    public class AppConfig
    {
        public class DropboxConfig
        {
            public string AppKey { get; set; }
            public string AppSecret { get; set; }
            public string RefreshToken { get; set; }
        }

        public DropboxConfig Dropbox { get; set; }
        public string DiscordBotAccessToken { get; set; }
        public string BedrockDirectory { get; set; }
    }
}
