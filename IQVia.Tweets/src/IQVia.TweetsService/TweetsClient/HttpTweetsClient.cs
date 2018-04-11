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

        public async Task<IEnumerable<Tweet>> List(DateTime startDate, DateTime endDate)
        {
            List<Tweet> tweets = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(this.URL);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                UriBuilder builder = new UriBuilder(string.Format("{0}/api/v1/Tweets",URL))
                {
                    Query = String.Format("startDate={0}&endDate={1}", startDate.ToString(), endDate.ToString())
                };

                HttpResponseMessage response = await httpClient.GetAsync(builder.Uri);

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
