using Xunit;
using System.Collections.Generic;
using IQVia.TweetsService.Tests.Persistence;
using IQvia.TweetsService;
using Microsoft.AspNetCore.Mvc;
using IQvia.TweetsService.Models;

namespace IQVia.TweetsService.Tests
{
    public class TweetsControllerTest
    {
        [Fact]
        public void QueryTweetsListReturnCorrectTweets()
        {
            TweetsController controller = new TweetsController(new TestTweetsRepository());
            var rawTweets = (IEnumerable<Tweet>)(controller.GetAllTweets() as ObjectResult).Value;
            List<Tweet> tweets = new List<Tweet>(rawTweets);
            Assert.Equal(2, tweets.Count);
            Assert.Equal("04/10/2018", tweets[0].Stamp);
            Assert.Equal("04/11/2018", tweets[1].Stamp);
        }
    }
}
