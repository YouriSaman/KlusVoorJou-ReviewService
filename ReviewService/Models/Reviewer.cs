using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Models
{
    public class Reviewer
    {
        public Guid Id { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string DisplayName { get; set; }
        public bool IsAnonymous { get; set; }

        public Reviewer(string profilePhotoUrl, string displayName, bool isAnonymous)
        {
            ProfilePhotoUrl = profilePhotoUrl;
            DisplayName = displayName;
            IsAnonymous = isAnonymous;
        }

        public Reviewer()
        {
            
        }
    }
}
