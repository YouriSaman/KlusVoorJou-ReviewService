using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Models
{
    public class Review
    {
        public string Name { get; set; }
        [Key]
        public string ReviewId { get; set; }
        public Guid CompanyId { get; set; }
        public Reviewer Reviewer { get; set; }
        public StarRating StarRating { get; set; }
        public string Comment { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public ReviewReply ReviewReply { get; set; }

        public Review(string name, string reviewId, Guid companyId, Reviewer reviewer, StarRating starRating, string comment, string createTime, string updateTime, ReviewReply reviewReply)
        {
            Name = name;
            ReviewId = reviewId;
            CompanyId = companyId;
            Reviewer = reviewer;
            StarRating = starRating;
            Comment = comment;
            CreateTime = createTime;
            UpdateTime = updateTime;
            ReviewReply = reviewReply;
        }

        public Review()
        {
            
        }
    }
}
