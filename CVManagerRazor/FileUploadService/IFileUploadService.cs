namespace CVManagerRazor.FileUploadService
{
    public interface IFileUploadService
    {
        Task<string> UploadFile(IFormFile file);

        void DeleteFile(string? fileName);
    }
}
