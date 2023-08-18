using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSC.MostUsedFeatures.API.Models
{
    public class UserProfile
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public IFormFile? ProfileImage { get; set; }//this is our image format
    }
}
