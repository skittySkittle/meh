using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RedditCringe.RedditApi;

namespace RedditCringe.Pages
{
    public class IndexModel : PageModel
    {
        public Subreddit RedditPosts;

        public async Task OnGet()
        {
            var redditData = new DataLayer();
            var subredditPosts = await redditData.GetSubRedditDetails();
            RedditPosts = subredditPosts;
        }
    }
}
