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
    public class LikeSvc : GenericSvc<LikeRep, Like>
    {
        private LikeRep likeRep;
        public LikeSvc()
        {
            likeRep = new LikeRep();
        }

        public SingleRsp createLike(LikeReq likeReq)
        {
            var res = new SingleRsp();
            Like like= new Like();
            like.Id = likeReq.Id;
            like.ArticleId = likeReq.ArticleId;
            like.UserId = likeReq.UserId;
            res = likeRep.createLike(like);
            return res;
        }
        public int Remove(int articleId, int userId)
        {
            var existingRecord = All.FirstOrDefault(like => like.ArticleId == articleId && like.UserId == userId);

            if (existingRecord == null)
            {
                return -1;
            }

            likeRep.Remove(articleId, userId);

            return existingRecord.Id;
        }
    }
}
