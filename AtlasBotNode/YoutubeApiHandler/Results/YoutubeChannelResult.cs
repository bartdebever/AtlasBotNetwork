using System.Collections.Generic;

namespace YoutubeApiHandler.Results
{
    public class YoutubeChannelResult
    {
        public string Name { get; set; }
        
        public string Url { get; set; }
        
        public string IconUrl { get; set; }
        
        public ulong SubscriberCount { get; set; }
        
        public List<VideoResult> Videos { get; set; }
        
        public string Description { get; set; }
    }
}