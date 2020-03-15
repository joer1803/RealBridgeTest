using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XamarinLearningAPI.Models;

namespace XamarinLearningAPI
{
    public class ApiService
    {
        private static string baseUrl = "https://api.sr.se/api/v2/";
        public async Task<List<RadioEpisode>> GetRadioSchedule(string channel, string date = "")
        {
            
            var scheduleEpisodes = new List<RadioEpisode>();

            using (var httpClient = new HttpClient())
            {
                try
                {
                    string ask = $"{baseUrl}scheduledepisodes?channelid={channel}?date={(date == "" ? DateTime.Now.ToString("yyyy-MM-dd") : date)}";
                    var response = await httpClient.GetAsync($"{baseUrl}scheduledepisodes?channelid={channel}&date={(date == "" ? DateTime.Now.ToString("yyyy-MM-dd") : date)}");
                if (response.IsSuccessStatusCode)
                {
                    XDocument xmlresponse = XDocument.Parse(response.Content.ReadAsStringAsync().Result);

                        foreach (var item in xmlresponse.Descendants("scheduledepisode"))
                        {
                            var ch = item.Element("channel");

                            var episode = new RadioEpisode
                            {
                                EpisodeId = (string)item.Element("episodeid") ?? "",
                                Title = (string)item.Element("title") ?? "",
                                Description = (string)item.Element("description") ?? "",
                                StartTimeUtc = (DateTime)item.Element("starttimeutc"),
                                EndTimeUtc = (DateTime)item.Element("endtimeutc"),
                                ImageUrl = (string)item.Element("imageurl") ?? "",
                                ChannelName = (string)ch.Attribute("name") ?? "",
                                ChannelId = (string)ch.Attribute("id") ?? ""
                            };

                            scheduleEpisodes.Add(episode);
                        }

                        return scheduleEpisodes;
                }
                }
                catch (Exception ex)
                {

                }
            }
            
            
            
            return null;

        }
        public async Task<List<RadioChannel>> GetRadioChannels()
        {
            var channels = new List<RadioChannel>();
            using(var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(baseUrl + "channels");
                if (response.IsSuccessStatusCode)
                {
                    XDocument xmlresponse = XDocument.Parse(response.Content.ReadAsStringAsync().Result);
                    foreach(var item in xmlresponse.Descendants("channel"))
                    {

                        var channel = new RadioChannel()
                        {
                            Name = (string)item.Attribute("name"),
                            Id = (string)item.Attribute("id")
                        };
                        channels.Add(channel);
                    }
                }
            }
            return channels;
        }
    }
}