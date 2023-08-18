using LSC.MostUsedFeatures.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LSC.MostUsedFeatures.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public UserProfileController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadUserProfile([FromForm] UserProfile userProfile)
        {
            //First let's check the incoming file is valid format
            if (!IsValidFile(userProfile.ProfileImage))
            {
                return BadRequest(new { message = "Invalid file format sent" });
            }

            //happy path
            if(userProfile.ProfileImage != null && userProfile.ProfileImage.Length>0)  //there should be a file to process
            { 
                var ticks = DateTime.UtcNow.Ticks;
                var fileName = $"{userProfile.FirstName}_{userProfile.LastName}_{ticks}" +
                    $"{Path.GetExtension(userProfile.ProfileImage.FileName)}";
                
                var filePath = Path.Combine(configuration["AppSettings:FileStoragePath"], fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    //userProfile.ProfileImage.CopyTo(stream);//this is called sync method but we will use async to make it better
                   await userProfile.ProfileImage.CopyToAsync(stream);
                }
            }
            return Ok("Profile uploaded successfully.");

        }


        [HttpPost("uploadToCurrentDirectory")]
        public async Task<IActionResult> UploadUserProfile1([FromForm] UserProfile userProfile)
        {
            // Process the userProfile and profileImage here
            // You can save the image to a storage service and update the ProfileImageUrl property in the model
            if (!IsValidFile(userProfile.ProfileImage))
            {
                return BadRequest(new { message = "Invalid file extensions" });
            }

            // Save the image to the application directory
            if (userProfile.ProfileImage != null && userProfile.ProfileImage.Length > 0)
            {
                var ticks = DateTime.UtcNow.Ticks;
                var fileName = $"{userProfile.FirstName}_{userProfile.LastName}_{ticks}{Path.GetExtension(userProfile.ProfileImage.FileName)}";

                var appDirectory = Directory.GetCurrentDirectory();
                //AppDomain.CurrentDomain.BaseDirectory;
                var storageDirectory = Path.Combine(appDirectory, "UploadedFiles"); // Create a subdirectory for storage

                if (!Directory.Exists(storageDirectory))
                {
                    Directory.CreateDirectory(storageDirectory);
                }

                var filePath = Path.Combine(storageDirectory, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await userProfile.ProfileImage.CopyToAsync(stream);
                }
            }

            return Ok("Profile uploaded successfully.");
        }

        private bool IsValidFile(IFormFile file)
        {
            List<string> validFormats = new List<string>() { ".jpg", ".png", ".jpeg" };
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];// this takes just extension of file
            return validFormats.Contains(extension); //this sends true or false based on condition
        }
    }
}
