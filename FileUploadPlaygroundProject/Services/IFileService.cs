namespace FileUploadPlaygroundProject.Services
{
    public interface IFileService
    {
        public Task Process(IEnumerable<ImageInputModel> images);
        public Task<Stream> GetThumbnails(Guid id);
        public Task<Stream> GetFullScreenImageById(Guid id);
        public Task<List<string>> GetAllImages();
    }
}


public class ImageInputModel
{
    public string Name { get; set; }
    public string Type { get; set; }
    public Stream Content { get; set; }
}