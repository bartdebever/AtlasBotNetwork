namespace AtlasModels.Server
{
    public class AtlasServer
    {
        public int Id { get; set; }
        public long DiscordId { get; set; }
        public AtlasServerSettings Settings { get; set; }
    }
}