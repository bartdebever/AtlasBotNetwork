namespace AtlasModels.Server
{
    public class AtlasServer
    {
        public int Id { get; set; }
        public long DiscordId { get; set; }
        public string Name { get; set; }
        public long OwnerId { get; set; }
        public AtlasServerSettings Settings { get; set; }
    }
}