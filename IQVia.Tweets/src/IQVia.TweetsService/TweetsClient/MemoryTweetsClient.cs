using IQvia.TweetsService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQvia.TweetsService.TweetsClient
{
    public class MemoryTweetsClient : ITweetsClient
    {
        public List<Tweet> Tweets { get; set; }
        public MemoryTweetsClient()
        {
            this.Tweets = new List<Tweet>();
        }

        public async Task<IEnumerable<Tweet>> List()
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
