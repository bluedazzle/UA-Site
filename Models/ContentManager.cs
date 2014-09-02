using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test2.Models
{
    public class ContentManager
    {
    }

    public class NewsManager
    {
        public WebsiteDbContext DbContext { get; set; }
        public NewsManager(WebsiteDbContext context)
        {
            DbContext = context;
        }
        public List<ListNewsViewModel> GetNewsList(ContentType type, int count)
        {
            if (type == null)
                return DbContext.News.Where(n => n.Enable).Take(count).Select(n => new ListNewsViewModel()
                    {
                        Id = n.Id,
                        Tittle = n.Tittle,
                        Summery = n.Body.Substring(0, 100),
                        Time = n.PublishTime,
                        //Picture=res
                        Author = n.Author
                    }).ToList();

            return DbContext.News.Where(n => n.Enable && n.ContentTypes.Contains(type)).Take(count).Select(n => new ListNewsViewModel()
            {
                Id = n.Id,
                Tittle = n.Tittle,
                Summery = n.Body.Substring(0, 100),
                Time = n.PublishTime,
                //Picture=res
                Author = n.Author
            }).ToList();
        }
        public List<ContentType> GetTypes()
        {
            return DbContext.ContentTypes.ToList();
        }
        public bool HasNews(int id)
        {
            return DbContext.News.Any(n => n.Enable && n.Id == id);
        }
        public NewsDetailModel GetDetail(int id)
        {
            return DbContext.News.Where(n => n.Enable && n.Id == id)
                .Select(n => new NewsDetailModel()
                {
                    Id = n.Id,
                    Tittle = n.Tittle,
                    Body = n.Body,
                    VisiteCount = n.VisiteCount,
                    Time = n.PublishTime,
                    Author = n.Author
                }).First();
        }
        public bool CreateNews(InsertNewsModel news,string publisherId)
        {
            News n = new News()
            {
                Tittle = news.Tittle,
                Author = news.Author,
                Body = news.Body,
                LastEditTime = DateTime.Now,
                PublishTime = DateTime.Now,
                Publisher = DbContext.Users.First(u => u.Id == publisherId),
                VisiteCount = 0,
                Enable = true,
            };
            DbContext.News.Add(n);
            DbContext.SaveChanges();
            return true;
        }
    }
}