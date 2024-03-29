﻿namespace EventsCalendar.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
using EventsCalendar.Model;
    using Newtonsoft.Json.Linq;

    public class RedditRepository
    {
        public IEnumerable<Post> GetTopPosts()
        {
            var session = new com.reddit.api.Session();
            var posts = com.reddit.api.Sub.GetListing(session, "LondonSocialClub");

            return posts.Select(ParseRedditPost).ToList();
        }

        private static Func<com.reddit.api.Post, Post> ParseRedditPost = p =>
            {
                DateTime? date = null;
                var match = System.Text.RegularExpressions.Regex.Match(p.Title, @"^[\[\(]?(\d\d?)/(\d\d?)/(?:20)?(\d\d)[\]\)]?\s+");
                if (match.Success)
                {
                    date = new DateTime(
                        Int32.Parse(match.Groups[3].Value) + 2000,
                        Int32.Parse(match.Groups[2].Value),
                        Int32.Parse(match.Groups[1].Value));
                    p.Title = p.Title.Substring(match.Value.Length);
                }

                var post = new Post
                {
                    RID = p.ID,
                    Title = p.Title,
                    SelfTextHtml = p.SelfTextHtml,
                    SelfText = p.SelfText,
                    Url = p.Url,
                    AuthorName = p.Author,
                    Created = p.Created,
                    Meeting = date,
                };
                return post;
            };

        public OperationStatus TestCredentials(string login, string password)
        {
            try
            {
                var session = com.reddit.api.User.Login(login, password);
                return new OperationStatus { Status = true };
            }
            catch (com.reddit.api.RedditException ex)
            {
                var obj = JObject.Parse(ex.Message);
                var error = "Unknown error";

                var json = obj["json"];
                if (json != null)
                {
                    var errors = json["errors"];
                    if (errors != null && errors.HasValues)
                    {
                        error = errors[0][1].ToString();
                    }
                }

                return new OperationStatus { Status = false, Message = error };
            }
        }
    }
}
