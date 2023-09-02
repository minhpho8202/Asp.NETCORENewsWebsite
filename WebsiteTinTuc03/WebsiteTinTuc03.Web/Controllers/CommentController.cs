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

        [HttpPost("update-comment")]
        public IActionResult updateComment([FromBody] CommentReq commentReq)
        {
            var res = new SingleRsp();
            res = commentSvc.updateComment(commentReq);
            return Ok(res);
        }

        [HttpDelete("comment/{commentId}")]
        public IActionResult deleteComment(int commentId)
        {
            int res;
            res = commentSvc.Remove(commentId);
            return Ok(res);
        }
    }
}
