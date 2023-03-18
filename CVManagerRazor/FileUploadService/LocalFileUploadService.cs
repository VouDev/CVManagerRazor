namespace CVManagerRazor.FileUploadService
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _environment;

        public LocalFileUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            var folderPath = Path.Combine(_environment.WebRootPath, "files");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var filePath = Path.Combine(_environment.WebRootPath, "files", file.FileName);
            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filePath;
        }

        public void DeleteFile(string? fileName)
        {
            if(File.Exists(fileName))
            {           
                File.Delete(fileName);
            }
        }
    }
}
