using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedditCringe.RedditApi;
using RedditSharp;

namespace RedditCringe
{
    public class DataLayer
    {
        public async Task<Subreddit> GetSubRedditDetails()
        {
            var redditService = new RedditService();
            var subReddit = await redditService.GetSubredditAsync("cringepics");
            return subReddit;
        }
    }
}
