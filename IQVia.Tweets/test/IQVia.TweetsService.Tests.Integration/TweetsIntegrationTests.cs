using IQvia.TweetsService;
using IQvia.TweetsService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;


namespace Q.TeamService.Tests.Integration
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
            var getResponse = await testClient.GetAsync("/tweets");
            getResponse.EnsureSuccessStatusCode();

            string raw = await getResponse.Content.ReadAsStringAsync();
            List<Tweet> tweets = JsonConvert.DeserializeObject<List<Tweet>>(raw);
            Assert.NotNull(tweets);
        }
    }
}
