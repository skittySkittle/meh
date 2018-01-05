using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RedditCringe.RedditApi
{
    public class RedditService
    {
        HttpClient client;
        Subreddit subreddit = new Subreddit();
        List<string> redditIds = new List<string>();

        public RedditService()
        {
            client = new HttpClient();
        }

        public async Task<IList<string>> GetSubredditsAsync()
        {
            var subreddits = new List<string>();

            var response = await client.GetStringAsync("http://www.reddit.com/.json");

            var jObject = JObject.Parse(response);

            var jArray = jObject["data"]["children"] as JArray;

            foreach (var obj in jArray)
            {
                subreddits.Add(obj["data"]["subreddit"].ToString());
            }

            subreddits = subreddits.OrderBy(sr => sr).ToList();

            return subreddits;
        }

        public async Task<Subreddit> GetSubredditAsync(string name)
        {
            subreddit.Name = name;

            subreddit.Posts = new List<Post>();

            var response = await client.GetStringAsync("http://www.reddit.com/r/" + name + "/.json?limit=100");

            var jObject = JObject.Parse(response);

            var jArray = jObject["data"]["children"] as JArray;

            foreach (var objItem in jArray)
            {
                var post = new Post();
                post.Title = objItem["data"]["title"].ToString();
                post.Author = objItem["data"]["author"].ToString();
                post.Url = objItem["data"]["url"].ToString();
                subreddit.Posts.Add(post);
            }


            //await GetRedditIDs(jArray);
            //await GetRedditData(name);


            return subreddit;
        }

        private async Task GetRedditData(string name)
        {
            List<JArray> jStuff = new List<JArray>();
            redditIds.RemoveRange(redditIds.Count - 98, 98);

            foreach (var itemId in redditIds)
            {
                var test = "http://www.reddit.com/r/" + name + "/.json?limit=100&after=t3_" + itemId;
                var response = await client.GetStringAsync("http://www.reddit.com/r/" + name + "/.json?limit=100&after=t3_" + itemId);
                var jObject = JObject.Parse(response);
                var jArray = jObject["data"]["children"] as JArray;
                jStuff.Add(jArray);
            }

            if (jStuff.Count > 0)
                foreach (var obj in jStuff)
                {
                    try
                    {
                        
                        foreach (var objItem in obj)
                        {
                            var post = new Post();
                            post.Title = objItem["data"]["title"].ToString();
                            post.Author = objItem["data"]["author"].ToString();
                            post.Url = objItem["data"]["url"].ToString();
                            subreddit.Posts.Add(post);
                        }
                       
                    }
                    catch (Exception e)
                    {

                    }
                }
        }

        private async Task GetRedditIDs(JArray jArray)
        {
            foreach (var obj in jArray)
            {
                var post = new Post();
                post.Id = obj["data"]["id"].ToString();
                redditIds.Add(post.Id);
            }
        }

        public async Task<IList<string>> SearchForSubredditsAsync(string query)
        {
            var subreddits = new List<string>();

            var response = await client.GetStringAsync("http://www.reddit.com/subreddits/search.json?q=" + query);

            var jObject = JObject.Parse(response);

            var jArray = jObject["data"]["children"] as JArray;

            foreach (var obj in jArray)
            {
                subreddits.Add(obj["data"]["display_name"].ToString());
            }

            return subreddits;
        }

        public async Task<Post> GetPostAsync(string id)
        {
            var post = new Post();
            var response = await client.GetStringAsync("http://www.reddit.com/api/info.json?id=t3_" + id);
            var jObject = JObject.Parse(response);
            var obj = (jObject["data"]["children"] as JArray)[0];
            post.Id = obj["data"]["id"].ToString();
            post.Title = obj["data"]["title"].ToString();
            post.Author = obj["data"]["author"].ToString();
            post.Score = obj["data"]["score"].Value<int>();
            post.Thumbnail = obj["data"]["thumbnail"].ToString();
            post.Url = obj["data"]["url"].ToString();
            post.NumberOfComments = obj["data"]["num_comments"].Value<int>();
            return post;
        }
    }
}
