using System;
using System.Collections.Generic;
using System.Text;

namespace Covinfo
{
    class FAQ
    {
        public int id { get; set; }
        public string keyword { get; set; }
        public string head { get; set; }
        public string body { get; set; }

        public FAQ(int id, string keyword, string head, string body)
        {
            this.id = id;
            this.keyword = keyword;
            this.head = head;
            this.body = body;
        }
    }
}
