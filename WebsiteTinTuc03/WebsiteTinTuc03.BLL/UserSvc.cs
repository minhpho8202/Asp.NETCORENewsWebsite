using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteTinTuc03.Common.BLL;
using WebsiteTinTuc03.Common.Req;
using WebsiteTinTuc03.Common.Rsp;
using WebsiteTinTuc03.DAL;
using WebsiteTinTuc03.DAL.Models;

namespace WebsiteTinTuc03.BLL
{
    public class UserSvc: GenericSvc<UserRep, User>
    {
        private UserRep userRep;
        public UserSvc()
        {
            userRep = new UserRep();
        }

        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            return res;
        }

        public SingleRsp createUser(UserReq userReq)
        {
            var res = new SingleRsp();
            User user = new User();
            user.Id = userReq.Id;
            user.Username = userReq.Username;
            user.Password = userReq.Password;
            user.CreatedDate = DateTime.Now;
            res = userRep.createUser(user);
            return res;
        }

        public SingleRsp updateUser(UserReq userReq)
        {
            var res = new SingleRsp();

            User existingUser = userRep.Read(userReq.Id);

            if (existingUser == null)
            {
                res.SetError("User not found");
                return res;
            }

            existingUser.Username = userReq.Username;
            existingUser.Password = userReq.Password;

            res = userRep.updateUser(existingUser);

            return res;
        }

        public int Remove(int id)
        {
            var existingRecord = All.FirstOrDefault(i => i.Id == id);

            if (existingRecord == null)
            {
                return -1;
            }

            userRep.Remove(existingRecord.Id);

            return existingRecord.Id;
        }


        public SingleRsp searchUser(SearchUserReq searchUserReq)
        {
            var res = new SingleRsp();
            var users = userRep.searchUser(searchUserReq.keyword);
            int countUser, totalPage, offset;
            offset = searchUserReq.size * (searchUserReq.page - 1);
            countUser = users.Count();
            totalPage = (countUser % searchUserReq.size) == 0 ? countUser / searchUserReq.size : (countUser / searchUserReq.size) + 1;
            var s = new
            {
                Data = users.Skip(offset).Take(searchUserReq.size).ToList(),
                Page = searchUserReq.page,
                Size = searchUserReq.size
            };
            res.Data = s;
            return res;
        }

        public SingleRsp CheckLogin(string username, string password)
        {
            return userRep.checkLogin(username, password);
        }

        public int countUsers(DateTime? startDate = null, DateTime? endDate = null)
        {
            return userRep.countUsers(startDate, endDate);
        }
    }
}
