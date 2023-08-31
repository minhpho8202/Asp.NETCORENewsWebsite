using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteTinTuc03.BLL;
using WebsiteTinTuc03.Common.Req;
using WebsiteTinTuc03.Common.Rsp;

namespace WebsiteTinTuc03.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserSvc userSvc;
        public UserController()
        { 
            userSvc = new UserSvc();
        }

        //[HttpPost("get-user-by-id")]
        //public IActionResult getUserById([FromBody] SimpleReq simpleReq)
        //{
        //    var res = new SingleRsp();
        //    res = userSvc.Read(simpleReq.Id);
        //    return Ok(res);
        //}

        [HttpGet("get-user-by-id/{userId}")]
        public IActionResult getUserById(int userId)
        {
            var res = new SingleRsp();
            res = userSvc.Read(userId);
            return Ok(res);
        }


        [HttpPost("create-user")]
        public IActionResult createUser([FromBody] UserReq userReq)
        {
            var res = new SingleRsp();
            res = userSvc.createUser(userReq);
            return Ok(res);
        }

        [HttpPost("update-user")]
        public IActionResult updateUser([FromBody] UserReq userReq)
        {
            var res = new SingleRsp();
            res = userSvc.updateUser(userReq);
            return Ok(res);
        }

        [HttpDelete("user")]
        public IActionResult deleteUser(int userId)
        {
            int res;
            res = userSvc.Remove(userId);
            return Ok(res);
        }

        [HttpPost("search-user")]
        public IActionResult searchUser([FromBody] SearchUserReq searchUserReq)
        {
            var res = new SingleRsp();
            res = userSvc.searchUser(searchUserReq);
            return Ok(res);
        }

        [HttpPost("check-login")]
        public IActionResult CheckLogin([FromBody] UserReq userReq)
        {
            var res = new SingleRsp();
            res = userSvc.CheckLogin(userReq.Username, userReq.Password);
            return Ok(res);
        }

    }
}
