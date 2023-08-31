using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteTinTuc03.BLL;
using WebsiteTinTuc03.Common.Req;
using WebsiteTinTuc03.Common.Rsp;

namespace WebsiteTinTuc03.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private CommentSvc commentSvc;
        public CommentController()
        {
            commentSvc = new CommentSvc();
        }

        [HttpPost("create-comment")]
        public IActionResult createUser([FromBody] CommentReq commentReq)
        {
            var res = new SingleRsp();
            res = commentSvc.createComment(commentReq);
            return Ok(res);
        }

        [HttpDelete("comment")]
        public IActionResult deleteComment(int articleId, int userId)
        {
            int res;
            res = commentSvc.Remove(articleId, userId);
            return Ok(res);
        }
    }
}
