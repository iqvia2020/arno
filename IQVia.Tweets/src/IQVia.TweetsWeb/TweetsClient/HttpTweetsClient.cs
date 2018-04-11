using IQVia.TweetsWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IQVia.TweetsWeb.TweetsClient
{
    public class HttpTweetsClient : ITweetsClient
    {
        public String URL { get; set; }

        public HttpTweetsClient(string url)
        {
            this.URL = url;
        }

        public async Task<IEnumerable<Tweet>> List(DateTime startDate, DateTime endDate)
        {
            List<Tweet> tweets = new List<Tweet>();
            Boolean allGood = true;
            TimeSpan interval = endDate.Subtract(startDate);

            long ts = (long)interval.Divide(new TimeSpan(TimeSpan.TicksPerDay));
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(this.URL);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                DateTime tstart = startDate;
                DateTime tend = startDate.AddTicks(TimeSpan.TicksPerDay);

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
                        if (newTweets.Count == 100)
                        {
                            allGood = false;
                            //TODO further chunking if the count is great than 100!
                            //Thankfully, at TicksPerDay for 2016 to 2017  all the tweets are less than 100.
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
                    tend = tstart.AddTicks(TimeSpan.TicksPerDay);
                }

            }

            return tweets;
        }

        public void Add(Tweet tweet)
        {

        }
    }
}
