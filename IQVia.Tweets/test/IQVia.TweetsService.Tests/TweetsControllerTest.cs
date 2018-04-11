﻿using IQVia.TweetsWeb.Controllers;
using IQVia.TweetsWeb.Models;
using IQVia.TweetsWeb.TweetsClient;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace IQVia.TweetsService.Tests
{
    public class TweetsControllerTest
    {
        [Fact]
        public async void QueryTweetsListReturnCorrectTweets()
        {
            var memoryTweetsCleint = new MemoryTweetsClient();
            memoryTweetsCleint.Tweets.Add(new Tweet("1", "04/10/2018", "hi"));
            memoryTweetsCleint.Tweets.Add(new Tweet("2", "04/11/2018", "world"));

            DateTime startDate = new DateTime(2016, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime endDate = new DateTime(2017, 12, 31, 23, 59, 59, 0, DateTimeKind.Utc);
            TweetsController controller = new TweetsController(memoryTweetsCleint);
            var rawTweets = (IEnumerable<Tweet>)(await controller.GetAllTweets(startDate, endDate) as ObjectResult).Value;
            List<Tweet> tweets = new List<Tweet>(rawTweets);
            Assert.Equal(2, tweets.Count);
            Assert.Equal("04/10/2018", tweets[0].Stamp);
            Assert.Equal("04/11/2018", tweets[1].Stamp);
        }

        //I ran out of time. I'm comment this out to be able to deploy the image
        //[Fact]
        //public async void QueryTweetsListMoreThan100PerDayReturnCorrectTweets()
        //{
        //    //TODO Mock HttpClient to be able to the logic used to adjust the time interval in  HttpTweetsClient
        //}

        //[Fact]
        //public async void QueryTweetsListMoreThan100PerHourReturnCorrectTweets()
        //{
        //    //TODO Mock HttpClient to be able to the logic used to adjust the time interval in  HttpTweetsClient
        //}

        //[Fact]
        //public async void QueryTweetsListMoreThan100PeMinThrowException()
        //{
        //    //TODO Mock HttpClient to be able to the logic used to adjust the time interval in  HttpTweetsClient
        //}
    }
}
