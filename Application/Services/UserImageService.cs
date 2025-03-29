//using Microsoft.AspNetCore.Http;
//using System.IO;
//using System.Threading.Tasks;

//namespace Application.Services
//{
//    public static class UserImageService
//    {
//        public static async Task<string> SaveUserImageAsync(IFormFile userImage)
//        {
//            // Masaüstü yolunu doğru bir şekilde almak
//            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
//            var uploadDirectory = Path.Combine(desktopPath, "uploads");  // Masaüstünde "uploads" klasörü

//            // Klasör yoksa oluştur
//            if (!Directory.Exists(uploadDirectory))
//            {
//                Directory.CreateDirectory(uploadDirectory);
//            }

//            // Dosya ismi oluşturma
//            var fileName = $"{Guid.NewGuid()}.jpg";
//            var filePath = Path.Combine(uploadDirectory, fileName);

//            try
//            {
//                // Dosyayı kaydetme
//                using (var stream = new FileStream(filePath, FileMode.Create))
//                {
//                    await userImage.CopyToAsync(stream);
//                }

//                // Kaydedilen dosyanın yolunu döndürme
//                return Path.Combine("uploads", fileName);
//            }
//            catch (Exception ex)
//            {
//                // Hata oluşursa loglamak veya hata mesajı döndürmek
//                // Loglama yapmanız gerekebilir, ama şu anda basit bir hata mesajı dönüyoruz.
//                throw new Exception($"Error saving image: {ex.Message}");
//            }
//        }
//    }
//}


