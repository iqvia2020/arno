using IQvia.TweetsService.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace IQvia.TweetsService
{
    [Route("[controller]")]
    public class TweetsController : Controller
    {
        ITweetsRepository repository;
        public TweetsController(ITweetsRepository repo)
        {
            this.repository = repo;
        }

        [HttpGet]
        public virtual IActionResult GetAllTweets()
        {
           return this.Ok(repository.List());
        }        
    }
}