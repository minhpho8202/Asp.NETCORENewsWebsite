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

        public SingleRsp updateComment(CommentReq commentReq)
        {
            var res = new SingleRsp();

            Comment existingComment = _rep.Read(commentReq.Id);

            if (existingComment == null)
            {
                res.SetError("Comment not found");
                return res;
            }
            existingComment.Content = commentReq.Content;

            res = _rep.updateComment(existingComment);

            return res;
        }

        public int Remove(int id) 
        {

            commentRep.Remove(id);

            return id;
        }

        public int countComments(DateTime? startDate = null, DateTime? endDate = null)
        {
            return commentRep.countComments(startDate, endDate);
        }
    }
}
