using Xunit;
using System.Collections.Generic;
using IQvia.TweetsService;
using Microsoft.AspNetCore.Mvc;
using IQvia.TweetsService.Models;
using IQvia.TweetsService.TweetsClient;

namespace IQVia.TweetsService.Tests
{
    public class TweetsControllerTest
    {
        [Fact]
        public async void QueryTweetsListReturnCorrectTweets()
        {
            var memoryTweetsCleint = new MemoryTweetsClient();
            memoryTweetsCleint.Add(new Tweet("1", "04/10/2018", "hi"));
            memoryTweetsCleint.Add(new Tweet("2", "04/11/2018", "world"));

            TweetsController controller = new TweetsController(memoryTweetsCleint);
            var rawTweets = (IEnumerable<Tweet>)(await controller.GetAllTweets() as ObjectResult).Value;
            List<Tweet> tweets = new List<Tweet>(rawTweets);
            Assert.Equal(2, tweets.Count);
            Assert.Equal("04/10/2018", tweets[0].Stamp);
            Assert.Equal("04/11/2018", tweets[1].Stamp);
        }
    }
}
