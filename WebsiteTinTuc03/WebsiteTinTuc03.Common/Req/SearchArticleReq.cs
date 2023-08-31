using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteTinTuc03.Common.Req
{
    public class SearchArticleReq
    {
        public string keyword { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}
