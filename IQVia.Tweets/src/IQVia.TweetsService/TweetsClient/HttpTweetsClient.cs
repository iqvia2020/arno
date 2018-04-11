using IQvia.TweetsService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IQvia.TweetsService.TweetsClient
{
    public class HttpTweetsClient : ITweetsClient
    {
        public String URL { get; set; }

        public HttpTweetsClient(string url)
        {
            this.URL = url;
        }

        public async Task<IEnumerable<Tweet>> List()
        {
            List<Tweet> tweets = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(this.URL);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync("/api/v1/Tweets");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    tweets = JsonConvert.DeserializeObject<List<Tweet>>(json);
                }
            }

            return tweets;
        }

        public void Add(Tweet tweet)
        {
            
        }
    }
}
