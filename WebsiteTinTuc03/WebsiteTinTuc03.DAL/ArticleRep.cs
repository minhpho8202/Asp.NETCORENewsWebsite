using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteTinTuc03.Common.DAL;
using WebsiteTinTuc03.Common.Rsp;
using WebsiteTinTuc03.DAL.Models;

namespace WebsiteTinTuc03.DAL
{
    public class ArticleRep : GenericRep<WebsiteTinTuc03Context, Article>
    {
        public ArticleRep()
        {

        }
        public override Article Read(int id)
        {
            //var res = All.FirstOrDefault(u => u.Id == id);
            var res = All
                .Where(u => u.Id == id)
                .Include(u => u.Likes)
                .Include(u => u.Comments)
                .Include(u => u.User).FirstOrDefault();
            return res;
        }

        public int Remove(int id)
        {
            var existingRecord = All.FirstOrDefault(u => u.Id == id);

            if (existingRecord == null)
            {
                return -1;
            }

            using (var context = new WebsiteTinTuc03Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Articles.Remove(existingRecord);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return -1;
                    }
                }
            }
            return existingRecord.Id;
        }

        public SingleRsp createArticle(Article article)
        {
            var res = new SingleRsp();
            using (var context = new WebsiteTinTuc03Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var u = context.Articles.Add(article);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public SingleRsp updateArticle(Article article)
        {
            var res = new SingleRsp();
            using (var context = new WebsiteTinTuc03Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var u = context.Articles.Update(article);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public List<Article> getPoppularArticles()
        {
            var articles = All
                .Include(u => u.Likes)
                .Include(u => u.Comments)
                .OrderByDescending(a => a.Likes.Count)
                .ThenByDescending(a => a.Comments.Count)
                .ToList();

            return articles;
        }

        public List<Article> getLatestArticles()
        {
            var articles = All
                .Include (u => u.Likes)
                .Include(u => u.Comments)
                .OrderByDescending(a => a.CreatedDate)
                .ToList();

            return articles;
        }


        public List<Article> searchArticle(string keyword)
        {
            var articles = All
                .Where(u => u.Title.Contains(keyword))
                .ToList();

            return articles;
        }

        public int countArticles(DateTime? startDate = null, DateTime? endDate = null)
        {
            using (var context = new WebsiteTinTuc03Context())
            {
                try
                {
                    if (!startDate.HasValue)
                    {
                        startDate = DateTime.Now.Date;
                    }

                    if (!endDate.HasValue)
                    {
                        endDate = DateTime.Now.Date.AddMonths(1).AddDays(-1);
                    }
                    int count = context.Articles
                        .Where(a => a.CreatedDate >= startDate && a.CreatedDate <= endDate)
                        .Count();

                    return count;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
            }
        }

    }
}
