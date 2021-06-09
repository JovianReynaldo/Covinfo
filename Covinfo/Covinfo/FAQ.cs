using System;
using System.Collections.Generic;
using System.Text;

namespace Covinfo
{
    class FAQ
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }

        public FAQ(int id, string keyword, string head, string body)
        {
            Id = id;
            Keyword = keyword;
            Head = head;
            Body = body;
        }
    }
}
