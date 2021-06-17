using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Models
{
    public class ReviewReply
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public string UpdateTime { get; set; }

        public ReviewReply(string comment, string updateTime)
        {
            Comment = comment;
            UpdateTime = updateTime;
        }

        public ReviewReply()
        {
            
        }
    }
}
