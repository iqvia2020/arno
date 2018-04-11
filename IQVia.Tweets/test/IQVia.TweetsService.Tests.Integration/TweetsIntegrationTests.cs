using IQVia.TweetsWeb;
using IQVia.TweetsWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;


namespace IQVia.TweetsService.Tests.Integration
{
    public class TweetsIntegrationTests
    {
        private readonly TestServer testServer;
        private readonly HttpClient testClient;

        public TweetsIntegrationTests()
        {
            testServer = new TestServer(new WebHostBuilder()
                    .UseStartup<Startup>());
            testClient = testServer.CreateClient();
        }

        [Fact]
        public async void TestTweetGet()
        {
            DateTime startDate = new DateTime(2016, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime endDate = new DateTime(2017, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc);

            UriBuilder builder = new UriBuilder("http://localhost/Tweets")
            {
                Query = String.Format("startDate={0}&endDate={1}", startDate.ToString(), endDate.ToString())
            };

            var getResponse = await testClient.GetAsync(builder.Uri);
            getResponse.EnsureSuccessStatusCode();

            string raw = await getResponse.Content.ReadAsStringAsync();
            List<Tweet> tweets = JsonConvert.DeserializeObject<List<Tweet>>(raw);
            Assert.NotNull(tweets);
            Assert.Equal(11681,tweets.Count);
        }
    }
}
