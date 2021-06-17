using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReviewService.Logic;
using ReviewService.Models;

namespace ReviewService.DAL
{
    public class ReviewDAL
    {
        private readonly ReviewDbContext _dbContext;

        //public ReviewDAL(ReviewDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        public List<Review> GetAllReviews()
        {
            var reviewer1 = new Reviewer("empty", "Henk van der Zanden", false);
            var reviewer2 = new Reviewer("empty", "Karel van Steen", false);
            var reply1 = new ReviewReply("Graag komen wij terug in contact", DateTime.Now.AddHours(1).ToString());

            var reviews = new List<Review>()
            {
                new Review("Fanstisch geholpen", Guid.NewGuid().ToString(), Guid.NewGuid(), reviewer1, StarRating.FIVE,
                    "Hele fijne mensen die mij erg goed hebben geholpen", DateTime.Now.AddDays(-1).ToString(), DateTime.Now.ToString(), null),
                new Review("Nooit meer", Guid.NewGuid().ToString(), Guid.NewGuid(), reviewer2, StarRating.ONE, "Wat was dit slecht... deze mensen hoeven voor mij geen klussen meer uit te voeren!", DateTime.Now.ToString(), DateTime.Now.ToString(), reply1)
            };

            return reviews;
        }

        //public List<Review> GetAllReviewsOfCompany(Guid id)
        //{
        //    return _dbContext.Reviews.Where(r => r.CompanyId == id).ToList();
        //}

        public void AddReviewsOfCompany()
        {
            //TODO add code
        }
    }
}
