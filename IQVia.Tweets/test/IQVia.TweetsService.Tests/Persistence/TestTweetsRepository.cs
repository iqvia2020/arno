using IQvia.TweetsService.Models;
using IQvia.TweetsService.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQVia.TweetsService.Tests.Persistence
{
    public class TestTweetsRepository : TweetsRepository
    {
        public TestTweetsRepository() : base(CreateInitialFake())
        {

        }

        private static ICollection<Tweet> CreateInitialFake()
        {
            var tweets = new List<Tweet>
            {
                new Tweet("1", "04/10/2018", "hi"),
                new Tweet("2", "04/11/2018", "world")
            };

            return tweets;
        }
    }

    
}
