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
    public class CommentSvc : GenericSvc<CommentRep, Comment>
    {
        private CommentRep commentRep;
        public CommentSvc()
        {
            commentRep = new CommentRep();
        }

        public SingleRsp createComment(CommentReq commentReq)
        {
            var res = new SingleRsp();
            Comment comment= new Comment();
            comment.Id = commentReq.Id;
            comment.ArticleId = commentReq.ArticleId;
            comment.UserId = commentReq.UserId;
            comment.Content = commentReq.Content;
            res = commentRep.createComment(comment);
            return res;
        }
        public int Remove(int articleId, int userId)
        {
            var existingRecord = All.FirstOrDefault(comment => comment.ArticleId == articleId && comment.UserId == userId);

            if (existingRecord == null)
            {
                return -1;
            }

            commentRep.Remove(articleId, userId);

            return existingRecord.Id;
        }
    }
}
