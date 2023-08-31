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
            var res = All.FirstOrDefault(u => u.Id == id);
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

        public List<Article> searchArticle(string keyword)
        {
            // Lấy danh sách bài viết dựa trên tiêu đề
            var articles = All
                .Where(u => u.Title.Contains(keyword))
                .Include(u => u.Likes)    // Liên kết thông tin về likes
                .Include(u => u.Comments) // Liên kết thông tin về comments
                .ToList();

            return articles;
        }




    }
}
