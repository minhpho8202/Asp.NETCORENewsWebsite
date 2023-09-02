using Microsoft.EntityFrameworkCore;
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
    public class CommentRep : GenericRep<WebsiteTinTuc03Context, Comment>
    {
        public CommentRep()
        {

        }

        public override Comment Read(int id)
        {
            var res = All
                .Where(u => u.Id == id).FirstOrDefault();
            return res;
        }

        public int Remove(int id)
        {
            var existingRecord = All.FirstOrDefault(comment => comment.Id == id);

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
                        context.Comments.Remove(existingRecord);
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

        public SingleRsp createComment(Comment comment)
        {
            var res = new SingleRsp();
            using (var context = new WebsiteTinTuc03Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var u = context.Comments.Add(comment);
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

        public SingleRsp updateComment(Comment comment)
        {
            var res = new SingleRsp();
            using (var context = new WebsiteTinTuc03Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var u = context.Comments.Update(comment);
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

        public int countComments(DateTime? startDate = null, DateTime? endDate = null)
        {
            using (var context = new WebsiteTinTuc03Context())
            {
                try
                {
                    if (!startDate.HasValue)
                    {
                        startDate = DateTime.Now.Date;
                    }

                    if (!endDate.HasValue)
                    {
                        endDate = DateTime.Now.Date.AddMonths(1).AddDays(-1);
                    }
                    int count = context.Comments
                        .Where(a => a.CreatedDate >= startDate && a.CreatedDate <= endDate)
                        .Count();

                    return count;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
            }
        }
    }
}
