using Xunit;
using System.Collections.Generic;
//using IQvia.TweetsService;
using Microsoft.AspNetCore.Mvc;
//using IQvia.TweetsService.Models;
//using IQvia.TweetsService.TweetsClient;
using System;
using IQVia.TweetsWeb.TweetsClient;
using IQVia.TweetsWeb.Models;
using IQVia.TweetsWeb.Controllers;

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

            DateTime startDate = new DateTime(2016, 01, 01, 0, 0, 0,0, DateTimeKind.Utc);
            DateTime endDate = new DateTime(2017, 12, 31,23, 59, 59,0, DateTimeKind.Utc);
            TweetsController controller = new TweetsController(memoryTweetsCleint);
            var rawTweets = (IEnumerable<Tweet>)(await controller.GetAllTweets(startDate,endDate) as ObjectResult).Value;
            List<Tweet> tweets = new List<Tweet>(rawTweets);
            Assert.Equal(2, tweets.Count);
            Assert.Equal("04/10/2018", tweets[0].Stamp);
            Assert.Equal("04/11/2018", tweets[1].Stamp);
        }
    }
}
