using LSC.MostUsedFeatures.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace LSC.MostUsedFeatures.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperimentController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ExperimentController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadUserProfile([FromForm] UserProfile userProfile)
        {
            // Process the userProfile and profileImage here
            // You can save the image to a storage service and update the ProfileImageUrl property in the model
            if (!IsValidFile(userProfile.ProfileImage))
            {
                return BadRequest(new { message = "Invalid file extensions" });
            }

            //byte[] fileBytes = null;
            //using (var stream = new MemoryStream())
            //{
            //    await userProfile.ProfileImage.CopyToAsync(stream);
            //    fileBytes = stream.ToArray();
            //}

            // Save the image to the specified directory
            if (userProfile.ProfileImage != null && userProfile.ProfileImage.Length > 0)
            {
                var ticks = DateTime.UtcNow.Ticks;
                var fileName = $"{userProfile.FirstName}_{userProfile.LastName}_{ticks}{Path.GetExtension(userProfile.ProfileImage.FileName)}";
                var filePath = Path.Combine(configuration["AppSettings:FileStoragePath"], fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
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
            List<string> validFormats = new List<string>() { ".jpg", ".png",  ".jpeg" };
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return validFormats.Contains(extension);
        }
    }
}
