using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Linq;
using System.Threading.Tasks;

namespace YoutubeApiHandler
{
    using YoutubeApiHandler.Results;

    public class YoutubeRequester
    {
        public static string ApiKey { get; set; }
        private YouTubeService _youtubeService;

        public YoutubeRequester()
        {
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ApiKey,
                ApplicationName = "AtlasBot"
            });
        }

        public async Task<YoutubeChannelResult> GetYoutuberByNameAsync(string name)
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
            if (channel == null)
                return null;
            channelSearch.Id = channel.Id.ChannelId;
            var channelResult = await channelSearch.ExecuteAsync();
            if (channelResult.Items[0].Statistics.SubscriberCount.HasValue)
                resultObject.SubscriberCount = channelResult.Items[0].Statistics.SubscriberCount.Value;
            var videoRequest = this._youtubeService.Search.List("snippet");
            videoRequest.ChannelId = channel.Id.ChannelId;
            videoRequest.Type = "video";
            videoRequest.Order = SearchResource.ListRequest.OrderEnum.ViewCount;
            videoRequest.MaxResults = 5;
            var videosResult = await videoRequest.ExecuteAsync();
            resultObject.Videos = videosResult.Items.Select(x => new VideoResult() { Name = x.Snippet.Title, Url = $"https://youtube.com/v?id={x.Id.VideoId}" }).ToList();
            return resultObject;
        }
    }
}