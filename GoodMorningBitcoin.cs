using Oxide.Core.Libraries.Covalence;
using Oxide.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("GoodMorningBitcoin", "RustySats", "0.1.0")]
    [Description("Plugin to display 'now playing' information from Good Morning Bitcoin radio.")]
    public class GoodMorningBitcoin : CovalencePlugin
    {
        private string apiEndpoint;
        private string cachedNowPlaying;
        private DateTime cacheExpiry = DateTime.MinValue;

        private void Init()
        {
            // Load the API endpoint from the plugin configuration
            apiEndpoint = Config["ApiEndpoint"]?.ToString() ?? "https://radio.goodmorningbitcoin.com/api/nowplaying";
        }

        protected override void LoadDefaultConfig()
        {
            Config["ApiEndpoint"] = "https://radio.goodmorningbitcoin.com/api/nowplaying";
            SaveConfig();
        }

        [Command("goodmorningbitcoin")]
        private void GoodMorningBitcoinCommand(IPlayer player, string command, string[] args)
        {
            if (DateTime.UtcNow < cacheExpiry && !string.IsNullOrEmpty(cachedNowPlaying))
            {
                player.Reply($"Now Playing: {cachedNowPlaying}");
                return;
            }

            webrequest.EnqueueGet(apiEndpoint, (code, response) =>
            {
                if (code != 200 || string.IsNullOrEmpty(response))
                {
                    player.Reply("Unable to retrieve now playing information at the moment. Please try again later.");
                    return;
                }

                try
                {
                    // Deserialize the JSON response as a list
                    var parsedData = JsonConvert.DeserializeObject<List<NowPlayingResponse>>(response);
                    var nowPlaying = parsedData?[0]?.NowPlaying?.Song?.Text; // Access the first element in the array

                    if (!string.IsNullOrEmpty(nowPlaying))
                    {
                        cachedNowPlaying = nowPlaying;
                        cacheExpiry = DateTime.UtcNow.AddSeconds(30); // Cache for 30 seconds
                        player.Reply($"Now Playing: {nowPlaying}");
                    }
                    else
                    {
                        player.Reply("No song information available.");
                    }
                }
                catch (Exception ex)
                {
                    LogError($"GoodMorningBitcoin: Error processing response - {ex.Message}");
                    player.Reply("Error processing the now playing information.");
                }
            }, this);
        }

        private class NowPlayingResponse
        {
            [JsonProperty("now_playing")]
            public NowPlayingDetails NowPlaying { get; set; }
        }

        private class NowPlayingDetails
        {
            public SongInfo Song { get; set; }
        }

        private class SongInfo
        {
            public string Text { get; set; }
        }
    }
}
