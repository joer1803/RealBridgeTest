using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinLearningAPI
{
    public class RadioEpisode
    {
        public string EpisodeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlTemplate { get; set; }
        public string EpisodeTime
        {
            get { return $"{StartTimeUtc.ToString("hh:mm")} - {EndTimeUtc.ToString("hh:mm")}"; }
        }
        public string ChannelName { get; set; }
        public string ChannelId { get; set; }
    }
}
