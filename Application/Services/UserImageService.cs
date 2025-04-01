//using Microsoft.AspNetCore.Http;
//using System.IO;
//using System.Threading.Tasks;

//namespace Application.Services
//{
//    public static class UserImageService
//    {
//        public static async Task<string> SaveUserImageAsync(IFormFile userImage)
//        {
//            
//            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//            var uploadDirectory = Path.Combine(desktopPath, "uploads"); 

//            if (!Directory.Exists(uploadDirectory))
//            {
//                Directory.CreateDirectory(uploadDirectory);
//            }

//            var fileName = $"{Guid.NewGuid()}.jpg";
//            var filePath = Path.Combine(uploadDirectory, fileName);

//            try
//            {
//                using (var stream = new FileStream(filePath, FileMode.Create))
//                {
//                    await userImage.CopyToAsync(stream);
//                }

//                return Path.Combine("uploads", fileName);
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Error saving image: {ex.Message}");
//            }
//        }
//    }
//}


