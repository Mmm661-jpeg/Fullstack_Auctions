using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WebApplicationAuctions.Core.Interfaces;
using WebApplicationAuctions.Data.Interfaces;
using WebApplicationAuctions.Domain.DomainModels;

namespace WebApplicationAuctions.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepo imageRepo;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ImageService(IImageRepo imageRepo, IWebHostEnvironment webHostEnvironment)
        {
            this.imageRepo = imageRepo;
            this.webHostEnvironment = webHostEnvironment;
        }



        public async Task<bool> AddAuctionPic(int auctiondID, IFormFile file)
        {
            try
            {
                var imagePath = await SaveFileAsync(file, "auctionspics");

                if (string.IsNullOrEmpty(imagePath))
                {
                    return false;
                }

                var image = new Images()
                {
                    ImagePath = imagePath,
                };

                var imageID = await imageRepo.AddImage(image);

                if (imageID <= 0)
                {
                    DeleteImageFile(imagePath); //delete incase of failure
                    return false;
                }

                var result = await imageRepo.SetAuctionImage(auctiondID, imageID);

                if(!result)
                {
                    var delImgRes = await imageRepo.DeleteImage(imageID);
                    if(delImgRes)
                    {
                        DeleteImageFile(imagePath);
                    }
                   
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                //log
                return false;
            }
        }

        public async Task<bool> AddUserProfilePic(int UserID, IFormFile file)
        {
            try
            {
               
                var imagePath = await SaveFileAsync(file, "userpics");

                if (string.IsNullOrEmpty(imagePath))
                {
                    return false;
                }

                var image = new Images()
                {
                    ImagePath = imagePath,
                };

                var imageID = await imageRepo.AddImage(image);

                if (imageID <= 0)
                {
                    DeleteImageFile(imagePath);
                    return false;
                }

                var result = await imageRepo.SetUserProfilePic(UserID, imageID);

                if (!result)
                {
                    var delImgRes = await imageRepo.DeleteImage(imageID);

                    if(delImgRes)
                    {
                        DeleteImageFile(imagePath);
                    }
                   
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                //log
                return false;
            }
        }

        public async Task<bool> DeleteAuctionPic(int auctionID)
        {
            try
            {
                var idOfImgtoDel = await imageRepo.GetAuctionPic(auctionID);

                if (idOfImgtoDel <= 0)
                {
                    return false;
                }

                var imgToDel = await imageRepo.GetImage(idOfImgtoDel);

                if (imgToDel == null)
                {
                    return false;
                }

                var delRes = await imageRepo.DeleteImage(imgToDel.ImageID); //transaction that rolls back in db

                if (!delRes)
                {
                    return false;
                }
                else
                {
                    DeleteImageFile(imgToDel.ImagePath);
                    return true;
                }
            }
            catch (Exception ex)
            {
                //log
                return false;
            }
        }

        public async Task<bool> DeleteUserProfilePic(int userID)
        {
            try
            {
                var idOfImgtoDel = await imageRepo.GetProfilePic(userID);

                if (idOfImgtoDel <= 0)
                {
                    return false;
                }

                var imgToDel = await imageRepo.GetImage(idOfImgtoDel);

                if (imgToDel == null)
                {
                    return false;
                }

                var delRes = await imageRepo.DeleteImage(imgToDel.ImageID); //transaction that rolls back in db

                if (!delRes)
                {
                    return false;
                }
                else
                {
                    DeleteImageFile(imgToDel.ImagePath);
                    return true;
                }
            }
            catch (Exception ex)
            {
                //log
                return false;
            }
        }

        public async Task<string> GetAuctionPic(int auctionID)
        {
            try
            {
                var imageID = await imageRepo.GetAuctionPic(auctionID);

                if(imageID <= 0)
                {
                    return null;
                }

                var image = await imageRepo.GetImage(imageID);

                if(image == null || string.IsNullOrEmpty(image.ImagePath))
                {
                    return null;
                }
                else
                {
                    return image.ImagePath;
                }


            }
            catch (Exception ex)
            {
                //log
                return null;
            }
        }

        public async Task<string> GetProfilePic(int userID)
        {
            try
            {
                var imageID = await imageRepo.GetProfilePic(userID);

                if (imageID <= 0)
                {
                    return null;
                }

                var image = await imageRepo.GetImage(imageID);

                if (image == null || string.IsNullOrEmpty(image.ImagePath))
                {
                    return null;
                }
                else
                {
                    return image.ImagePath;
                }


            }
            catch (Exception ex)
            {
                //log
                return null;
            }
        }


        private void DeleteImageFile(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                var fullPath = Path.Combine(webHostEnvironment.WebRootPath, imagePath.TrimStart('/'));

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
        }

        private async Task<string> SaveFileAsync(IFormFile file, string subFolder)
        {
            if (webHostEnvironment == null)
            {
                throw new InvalidOperationException("WebHostEnvironment is not initialized.");
            }

            if (file == null || file.Length == 0)
                throw new ArgumentException("No file provided");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException($"Invalid file type. Allowed types: {string.Join(", ", allowedExtensions)}");
            }


            const int maxFileSize = 5 * 1024 * 1024;
            if (file.Length > maxFileSize)
            {
                throw new InvalidOperationException($"File size exceeds {maxFileSize / (1024 * 1024)}MB limit");
            }

            var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/webp" };
            if (!allowedContentTypes.Contains(file.ContentType.ToLowerInvariant()))
            {
                throw new InvalidOperationException($"Invalid content type. Allowed types: {string.Join(", ", allowedContentTypes)}");


            }

            var uploadsPath = Path.Combine(webHostEnvironment.WebRootPath, "uploads", subFolder);

          
            Console.WriteLine($"Uploading file to: {uploadsPath}");  // Log to check path


            try
            {

                Directory.CreateDirectory(uploadsPath);


                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadsPath, uniqueFileName);


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }


                return $"/uploads/{subFolder}/{uniqueFileName}";
            }

            catch (UnauthorizedAccessException ex)
            {
                throw new InvalidOperationException("Permission denied for file upload", ex);
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException("Error saving file", ex);
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException("File upload failed", ex);
            }
        }
    }
}
