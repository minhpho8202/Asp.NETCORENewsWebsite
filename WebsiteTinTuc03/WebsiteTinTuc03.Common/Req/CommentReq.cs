using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteTinTuc03.Common.Req
{
    public class CommentReq
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int? ArticleId { get; set; }

        public string? Content { get; set; }
    }
}
