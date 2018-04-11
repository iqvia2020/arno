using IQVia.TweetsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IQVia.TweetsWeb.TweetsClient
{
    public class MemoryTweetsClient : ITweetsClient
    {
        public List<Tweet> Tweets { get; set; }
        public MemoryTweetsClient()
        {
            this.Tweets = new List<Tweet>();
        }

        public async Task<IEnumerable<Tweet>> List(DateTime startDate, DateTime endDate)
        {
            return await Task.Run(() =>
            {
                return Tweets;
            });
        }

        public void Add(Tweet newTweet)
        {
            if (newTweet == null) return;
            if (!Tweets.Exists(tweet => String.Equals(tweet.Id, newTweet.Id, StringComparison.OrdinalIgnoreCase)))
            {
                Tweets.Add(newTweet);
            }
        }
    }
}
