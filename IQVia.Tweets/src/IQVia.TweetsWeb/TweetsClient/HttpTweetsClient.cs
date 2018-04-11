using IQVia.TweetsWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IQVia.TweetsWeb.TweetsClient
{
    public class HttpTweetsClient : ITweetsClient
    {
        public String URL { get;}
        public long  CurrentTicks { get; private set; }

        /// <summary>
        /// This client is used to get tweets from Third party REST API. 
        /// </summary>
        /// <param name="url">The base Url for the dependence this component depend on. i.e the Third party component</param>
        /// <param name="currentTicks">The time range for splitting the given time range into smaller chunks.</param>
        public HttpTweetsClient(string url, long currentTicks)
        {
            this.URL = url;
            this.CurrentTicks = currentTicks;
        }

        /// <summary>
        /// Retreives distinct tweets within the given time range.
        /// </summary>
        /// <param name="startDate">Start date inclusive</param>
        /// <param name="endDate">end date also inclusive</param>
        /// <returns></returns>
        public async Task<IEnumerable<Tweet>> List(DateTime startDate, DateTime endDate)
        {
            List<Tweet> tweets = new List<Tweet>();
            TimeSpan interval = endDate.Subtract(startDate);

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(this.URL);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                DateTime tstart = startDate;
                DateTime tend = startDate.AddTicks(CurrentTicks);
                
                while (tend <= endDate)
                {
                    UriBuilder builder = new UriBuilder(string.Format("{0}/api/v1/Tweets", URL))
                    {
                        Query = String.Format("startDate={0}&endDate={1}", tstart.ToString(), tend.ToString())
                    };

                    HttpResponseMessage response = await httpClient.GetAsync(builder.Uri);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        List<Tweet> newTweets = JsonConvert.DeserializeObject<List<Tweet>>(json);
                        if (newTweets.Count >= 100)
                        {
                            //Make the time interval smaller if the result is greater than or equal to 100.
                            //For example if the current query interval is daily change it to hourly.
                            //if its hourly adjust it to per minute.This is the minumium threashold.
                            //TODO: log error message when its at per minutes and the count is 100. i.e tne minimum threshold is exceeded.
                            // Or throw exception.
                            if (CurrentTicks == TimeSpan.TicksPerDay) CurrentTicks = TimeSpan.TicksPerHour;
                            else if (CurrentTicks == TimeSpan.TicksPerHour) CurrentTicks = TimeSpan.TicksPerMinute;
                            else
                            {
                                new InvalidOperationException("Tweets per minute exceeeded was greater than or equal to 100.");
                            }
                            tend = tstart.AddTicks(CurrentTicks);
                            continue;
                        }
                        else 
                        {
                            CurrentTicks = TimeSpan.TicksPerDay;
                        }

                        if (newTweets != null && newTweets.Count > 0)
                        {
                            newTweets.ForEach(newTweet =>
                            {
                                if (tweets.Count == 0) tweets.AddRange(newTweets);
                                else if (!tweets.Exists(existingTweet => String.Equals(existingTweet.Id, newTweet.Id, StringComparison.OrdinalIgnoreCase)))
                                {
                                    tweets.Add(newTweet);
                                }
                            });
                        }
                    }
                    tstart = tend;
                    tend = tstart.AddTicks(CurrentTicks);
                }
            }
            return tweets;
        }
    }
}
