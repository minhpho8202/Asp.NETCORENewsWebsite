using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteTinTuc03.BLL;

namespace WebsiteTinTuc03.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private ArticleSvc articleSvc;
        private UserSvc userSvc;
        private CommentSvc commentSvc;
        public StatsController()
        {
            articleSvc = new ArticleSvc();
            userSvc = new UserSvc();
            commentSvc = new CommentSvc();
        }

        [HttpGet("count-article")]
        public IActionResult countArticles([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            int count = articleSvc.countArticles(startDate, endDate);
            return Ok(count);
        }

        [HttpGet("count-user")]
        public IActionResult countUsers([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            int count = userSvc.countUsers(startDate, endDate);
            return Ok(count);
        }

        [HttpGet("count-comment")]
        public IActionResult countComment([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            int count = commentSvc.countComments(startDate, endDate);
            return Ok(count);
        }
    }
}
