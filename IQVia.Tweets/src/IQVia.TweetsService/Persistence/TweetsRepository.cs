using IQvia.TweetsService.Models;
using System;
using System.Collections.Generic;

namespace IQvia.TweetsService.Persistence
{
    public class TweetsRepository : ITweetsRepository
    {
        protected ICollection<Tweet> tweets;

        public TweetsRepository()
        {
            if (tweets == null)
            {
                tweets = new List<Tweet>();
            }
        }

        public TweetsRepository(ICollection<Tweet> tweets)
        {
            this.tweets = tweets;
        }


        public IEnumerable<Tweet> List()
        {
            return tweets;
        }
    }
}
