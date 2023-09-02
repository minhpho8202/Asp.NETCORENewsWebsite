using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteTinTuc03.BLL;
using WebsiteTinTuc03.Common.Req;
using WebsiteTinTuc03.Common.Rsp;

namespace WebsiteTinTuc03.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private ArticleSvc articleSvc;
        public ArticleController()
        {
            articleSvc = new ArticleSvc();
        }

        [HttpGet("get-article-by-id/{articleId}")]
        public IActionResult getArticleById(int articleId)
        {
            var res = new SingleRsp();
            res = articleSvc.Read(articleId);
            return Ok(res);
        }


        [HttpPost("create-article")]
        public IActionResult createArticle([FromBody] ArticleReq articleReq)
        {
            var res = new SingleRsp();
            res = articleSvc.createArticle(articleReq);
            return Ok(res);
        }

        [HttpPost("update-article")]
        public IActionResult updateArticle([FromBody] ArticleReq articleReq)
        {
            var res = new SingleRsp();
            res = articleSvc.updateArticle(articleReq);
            return Ok(res);
        }

        [HttpDelete("article")]
        public IActionResult deleteArticle(int articleId)
        {
            int res;
            res = articleSvc.Remove(articleId);
            return Ok(res);
        }

        [HttpPost("search-article")]
        public IActionResult searchArticle([FromBody] SearchArticleReq searchArticleReq)
        {
            var res = new SingleRsp();
            res = articleSvc.searchArticle(searchArticleReq);
            return Ok(res);
        }

        [HttpGet("get-popular-articles")]
        public IActionResult getPopularArticles()
        {
            var res = new SingleRsp();
            res = articleSvc.getPopularArticles();
            return Ok(res);
        }

        [HttpGet("get-latest-articles")]
        public IActionResult getLatestArticles()
        {
            var res = new SingleRsp();
            res = articleSvc.getLatestArticles();
            return Ok(res);
        }

        [HttpGet("get-article-by-userId")]
        public IActionResult getArticleByUserId(int userId)
        {
            var res = new SingleRsp();
            res = articleSvc.getArticlesByUserId(userId);
            return Ok(res);
        }

        [HttpGet("count-all-article")]
        public IActionResult countAllArticles()
        {
            int count = articleSvc.countAllArticles();
            return Ok(count);
        }
    }
}
