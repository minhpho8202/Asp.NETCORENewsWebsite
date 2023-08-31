using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteTinTuc03.Common.Req
{
    public class ArticleReq
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }
    }
}
