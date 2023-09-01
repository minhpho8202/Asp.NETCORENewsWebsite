using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteTinTuc03.Common.DAL;
using WebsiteTinTuc03.Common.Req;
using WebsiteTinTuc03.Common.Rsp;
using WebsiteTinTuc03.DAL.Models;

namespace WebsiteTinTuc03.DAL
{
    public class UserRep:GenericRep<WebsiteTinTuc03Context, User>
    {
        public UserRep()
        {

        }
        public override User Read(int id)
        {
            var res = All.FirstOrDefault(u => u.Id == id);
            return res;
        }

        public int Remove(int id)
        {
            var existingRecord = All.FirstOrDefault(u => u.Id == id);

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
                        context.Users.Remove(existingRecord);
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


        public SingleRsp createUser(User user)
        {
            var res = new SingleRsp();
            using(var context = new WebsiteTinTuc03Context())
            {
                using(var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var u = context.Users.Add(user);
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

        public SingleRsp updateUser(User user)
        {
            var res = new SingleRsp();
            using (var context = new WebsiteTinTuc03Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var u = context.Users.Update(user);
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

        public List<User> searchUser(string keyword)
        {
            return All.Where(u => u.Username.Contains(keyword)).ToList();
        }

        public SingleRsp checkLogin(string username, string password)
        {
            var res = new SingleRsp();
            using (var context = new WebsiteTinTuc03Context())
            {
                try
                {
                    var user = context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

                    if (user == null)
                    {
                        res.SetError("Invalid username or password");
                    }
                    else
                    {
                        res.Data = user;
                    }
                }
                catch (Exception ex)
                {
                    res.SetError(ex.StackTrace);
                }
            }
            return res;
        }

        public int countUsers(DateTime? startDate = null, DateTime? endDate = null)
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
                    int count = context.Users
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
