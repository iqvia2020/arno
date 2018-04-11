using IQvia.TweetsService.TweetsClient;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IQvia.TweetsService
{
    [Route("[controller]")]
    public class TweetsController : Controller
    {
        private ITweetsClient tweetClient;
        public TweetsController(ITweetsClient tweetClient)
        {
            this.tweetClient = tweetClient;
        }

        [HttpGet]
        public async virtual Task<IActionResult> GetAllTweets(DateTime startDate, DateTime endDate)
        {
           return this.Ok(await tweetClient.List(startDate,endDate));
        }
    }
}