using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReviewService.DAL;
using ReviewService.Models;

namespace ReviewService.Controllers
{
    [Route("[controller]")]
    public class ReviewController : Controller
    {
        private readonly ReviewDAL _reviewDal;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(/*ReviewDbContext dbContext,*/ ILogger<ReviewController> logger)
        {
            _reviewDal = new ReviewDAL(/*dbContext*/);
            _logger = logger;
        }

        [HttpGet("list")]
        public IActionResult GetReviews()
        {
            _logger.LogInformation("Get reviews");
            List<Review> reviews;
            try
            {
                reviews = _reviewDal.GetAllReviews();
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.GetBaseException().ToString());
                throw;
            }
            return Ok(new { reviews = reviews });
        }
    }
}
