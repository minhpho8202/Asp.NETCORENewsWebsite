using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteTinTuc03.BLL;
using WebsiteTinTuc03.Common.Req;
using WebsiteTinTuc03.Common.Rsp;

namespace WebsiteTinTuc03.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private LikeSvc likeSvc;
        public LikeController()
        {
            likeSvc = new LikeSvc();
        }

        [HttpPost("create-like")]
        public IActionResult createUser([FromBody] LikeReq likeReq)
        {
            var res = new SingleRsp();
            res = likeSvc.createLike(likeReq);
            return Ok(res);
        }

        [HttpDelete("like")]
        public IActionResult deleteLike(int articleId, int userId)
        {
            int res;
            res = likeSvc.Remove(articleId, userId);
            return Ok(res);
        }
    }
}
