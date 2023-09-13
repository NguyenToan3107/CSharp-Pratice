namespace BTH1.Models.Interfaces
{
    public interface BufferedFileUploadService
    {
        Task<bool> UploadFile(IFormFile file);
    }
}
