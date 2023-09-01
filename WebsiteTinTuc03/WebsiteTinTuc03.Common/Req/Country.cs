using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteTinTuc03.Common.Req
{
    public class Country
    {
        public Name Name { get; set; }
        public string Population { get; set; }
        public string Region{ get; set; }
        public List<string> Capital { get; set; }
    }

    public class Name
    {
        public string Common { get; set; }
        public string Official { get; set; }
    }

}
