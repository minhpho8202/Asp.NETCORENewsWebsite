using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteTinTuc03.Common.DAL;
using WebsiteTinTuc03.Common.Rsp;
using WebsiteTinTuc03.DAL.Models;

namespace WebsiteTinTuc03.DAL
{
    public class LikeRep : GenericRep<WebsiteTinTuc03Context, Like>
    {
        public LikeRep()
        {

        }

        public int Remove(int articleId, int userId)
        {
            var existingRecord = All.FirstOrDefault(like => like.ArticleId == articleId && like.UserId == userId);

            if (existingRecord == null)
            {
                return -1;
            }

            using (var context = new WebsiteTinTuc03Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Likes.Remove(existingRecord);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return -1;
                    }
                }
            }

            return existingRecord.Id;
        }



        public SingleRsp createLike(Like like)
        {
            var res = new SingleRsp();
            using (var context = new WebsiteTinTuc03Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var u = context.Likes.Add(like);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }

        public bool isLiked(int articleId, int userId)
        {
            var context = new WebsiteTinTuc03Context();
            var existingRecord = context.Likes.FirstOrDefault(like => like.ArticleId == articleId && like.UserId == userId);
            if(existingRecord != null)
            {
                return true;
            }
            return false;
            
        }

    }
}
