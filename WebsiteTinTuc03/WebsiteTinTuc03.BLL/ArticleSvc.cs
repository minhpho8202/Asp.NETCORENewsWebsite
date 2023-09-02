using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteTinTuc03.Common.BLL;
using WebsiteTinTuc03.DAL.Models;
using WebsiteTinTuc03.DAL;
using WebsiteTinTuc03.Common.Req;
using WebsiteTinTuc03.Common.Rsp;

namespace WebsiteTinTuc03.BLL
{
    public class ArticleSvc : GenericSvc<ArticleRep, Article>
    {
        private ArticleRep articleRep;
        public ArticleSvc()
        {
            articleRep = new ArticleRep();
        }

        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            return res;
        }

        public SingleRsp createArticle(ArticleReq articleReq)
        {
            var res = new SingleRsp();
            Article article = new Article();
            article.Id = articleReq.Id;
            article.UserId = articleReq.UserId;
            article.Title = articleReq.Title;
            article.Content = articleReq.Content;
            res = _rep.createArticle(article);
            return res;
        }

        public SingleRsp updateArticle(ArticleReq articleReq)
        {
            var res = new SingleRsp();

            Article existingArticle = _rep.Read(articleReq.Id);

            if (existingArticle == null)
            {
                res.SetError("Article not found");
                return res;
            }
            existingArticle.UserId = articleReq.UserId;
            existingArticle.Title = articleReq.Title;
            existingArticle.Content = articleReq.Content;

            res = _rep.updateArticle(existingArticle);

            return res;
        }

        public int Remove(int id)
        {
            var existingRecord = All.FirstOrDefault(i => i.Id == id);

            if (existingRecord == null)
            {
                return -1;
            }

            articleRep.Remove(existingRecord.Id);

            return existingRecord.Id;
        }

        public SingleRsp searchArticle(SearchArticleReq searchArticleReq)
        {
            var res = new SingleRsp();
            var articles = _rep.searchArticle(searchArticleReq.keyword);
            int countArticle, totalPage, offset;
            offset = searchArticleReq.size * (searchArticleReq.page - 1);
            countArticle = articles.Count();
            totalPage = (countArticle % searchArticleReq.size) == 0 ? countArticle / searchArticleReq.size : (countArticle / searchArticleReq.size) + 1;
            var s = new
            {
                Data = articles.Skip(offset).Take(searchArticleReq.size).ToList(),
                Page = searchArticleReq.page,
                Size = searchArticleReq.size
            };
            res.Data = s;
            return res;
        }

        public SingleRsp getPopularArticles()
        {
            var res = new SingleRsp();
            res.Data = articleRep.getPoppularArticles();
            return res;
        }

        public SingleRsp getLatestArticles()
        {
            var res = new SingleRsp();
            res.Data = articleRep.getLatestArticles();
            return res;
        }
        public int countArticles(DateTime? startDate = null, DateTime? endDate = null)
        {
            return articleRep.countArticles(startDate, endDate);
        }

        public int countAllArticles()
        {
            return articleRep.countAllArticles();
        }

        public SingleRsp getArticlesByUserId(int userId)
        {
            var res = new SingleRsp();
            res.Data = articleRep.getArticlesByUserId(userId);
            return res;
        }
    }
}
