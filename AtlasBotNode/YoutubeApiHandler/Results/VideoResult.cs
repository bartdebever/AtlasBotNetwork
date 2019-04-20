namespace YoutubeApiHandler.Results
{
    public class VideoResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VideoResult" /> class.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        public VideoResult(string url, string name)
        {
            Url = url;
            Name = name;
        }

        public string Url { get; set; }

        public string Name { get; set; }
    }
}