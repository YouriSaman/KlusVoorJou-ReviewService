using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReviewService.DAL;
using ReviewService.Models;

namespace ReviewService.Logic
{
    public class ReviewLogic
    {
        private static readonly HttpClient Client = new HttpClient();
        private readonly ReviewDAL _reviewDal;

        public ReviewLogic(ReviewDbContext dbContext)
        {
            _reviewDal = new ReviewDAL(/*dbContext*/);
        }

        /// <summary>
        /// Get reviews from company by placeId from API and add them to the database
        /// </summary>
        /// <param name="message">Message containing companyId and companyPlaceId</param>
        public async Task AddCompanyReviewsOfMessage(NewCompanyMessage message)
        {
            var reviews = await GetCompanyReviewsOfAPI(message);
            foreach (var review in reviews)
            {
                _reviewDal.AddReviewsOfCompany();
            }
        }

        private async Task<List<Review>> GetCompanyReviewsOfAPI(NewCompanyMessage message)
        {
            List<Review> reviews;
            try
            {
                string uri = "https://localhost:5031/review/getReviews";
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(uri),
                    Content = new StringContent($"{{\"placeId\":\"{message.CompanyPlaceId}\"}}", Encoding.UTF8, "application/json")
                };
                using var response = await Client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string contentString = await response.Content.ReadAsStringAsync();
                reviews = JsonConvert.DeserializeObject<List<Review>>(contentString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return reviews;
        }

        public NewCompanyMessage ConvertMessage(string message)
        {
            return JsonConvert.DeserializeObject<NewCompanyMessage>(message);
        }
    }
}
