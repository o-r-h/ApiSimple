using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Entities.Customs
{
    public class Message
    {
        public const string NORMAL_PRIORITY = "NORMAL";
        public const string HIGH_PRIORITY = "HIGH";
        public const string LOW_PRIORITY = "LOW";

        public string From { get; set; }
        public string FromName { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHTML { get; set; }
        public string Priority { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<Resource> Resources { get; set; }

        public Message()
        {
            IsHTML = true;
            Priority = NORMAL_PRIORITY;
        }
    }
}
