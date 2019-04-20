using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using YoutubeApiHandler.Results;

namespace YoutubeApiHandler
{
    public class YoutubeClient
    {
        private readonly YouTubeService _youtubeService;

        public YoutubeClient()
        {
            _youtubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = ApiKey,
                ApplicationName = "AtlasBot"
            });
        }

        public static string ApiKey { private get; set; }

        public async Task<YoutubeChannelResult> GetYouTuberByNameAsync(string name)
        {
            var searchList = _youtubeService.Search.List("snippet");
            searchList.Q = name;
            searchList.MaxResults = 50;
            searchList.Type = "channel";

            var result = await searchList.ExecuteAsync();
            var channel = result.Items.FirstOrDefault(x => x.Id.Kind == "youtube#channel");
            if (channel == null) return null;

            var resultObject = new YoutubeChannelResult
            {
                Url = $"https://youtube.com/channel/{channel.Id.ChannelId}",
                Name = channel.Snippet.ChannelTitle,
                Description = channel.Snippet.Description,
                IconUrl = channel.Snippet.Thumbnails.Default__.Url
            };

            var channelSearch = _youtubeService.Channels.List("snippet,statistics");
            channelSearch.Id = channel.Id.ChannelId;
            var channelResult = await channelSearch.ExecuteAsync();

            if (channelResult?.Items == null || !channelResult.Items.Any()) return null;

            var firstItem = channelResult.Items.FirstOrDefault();
            if (firstItem != null && firstItem.Statistics.SubscriberCount.HasValue)
                resultObject.SubscriberCount = firstItem.Statistics.SubscriberCount.Value;

            var videoRequest = _youtubeService.Search.List("snippet");
            videoRequest.ChannelId = channel.Id.ChannelId;
            videoRequest.Type = "video";
            videoRequest.Order = SearchResource.ListRequest.OrderEnum.ViewCount;
            videoRequest.MaxResults = 5;
            var videosResult = await videoRequest.ExecuteAsync();

            resultObject.Videos = videosResult.Items
                .Select(x => new VideoResult($"https://youtube.com/v?id={x.Id.VideoId}", x.Snippet.Title)).ToList();
            return resultObject;
        }
    }
}