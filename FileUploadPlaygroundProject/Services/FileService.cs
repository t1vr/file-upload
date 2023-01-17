using FileUploadPlaygroundProject.Data;
using FileUploadPlaygroundProject.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace FileUploadPlaygroundProject.Services
{
    public class FileService : IFileService
    {
        private const int ThumbnailWidth = 300;
        private const int FullScreenWidth = 1000;
        private readonly ApplicationDbContext _dbContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public FileService(IServiceScopeFactory serviceScopeFactory, ApplicationDbContext dbContext)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _dbContext = dbContext;
        }

        public async Task<List<string>> GetAllImages()
        {
            return await _dbContext.ImageData.Select(i => i.Id.ToString()).ToListAsync();
        }


        public async Task<Stream> GetFullScreenImageById(Guid id)
        {
            return await GetImageData(id, "Fullscreen");
        }

        public async Task<Stream> GetThumbnails(Guid id)
        {
            return await GetImageData(id, "Thumbnail");
        }

        //Not completed yet
        private async Task<Stream> GetImageData(Guid id, string size)
        {
            var database = _dbContext.Database;
            var dbConnection = (NpgsqlConnection)database.GetDbConnection();
            var command = new NpgsqlCommand($"SELECT \"{size}Content\" FROM public.\"ImageData\" WHERE \"Id\" = @id;", dbConnection);
            command.Parameters.Add(new NpgsqlParameter("@id", id));
            dbConnection.Open();
            var reader = await command.ExecuteReaderAsync();
            Stream result = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result = reader.GetStream(0);
                }
            }
            reader.Close();
            return result;
        }


        public async Task Process(IEnumerable<ImageInputModel> images)
        {
            var tasks = images.Select(image => Task.Run(async () =>
            {
                using var imageResult = await Image.LoadAsync(image.Content);

                var originial = await SaveImage(imageResult, imageResult.Width);
                var fullscreen = await SaveImage(imageResult, FullScreenWidth);
                var thumbnail = await SaveImage(imageResult, ThumbnailWidth);

                var database = _serviceScopeFactory
                    .CreateScope()
                    .ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

                database.ImageData.Add(new ImageData
                {
                    OriginalFileName = image.Name,
                    OriginalType = image.Type,
                    OriginalContent = originial,
                    ThumbnailContent = thumbnail,
                    FullscreenContent = fullscreen
                });

                await database.SaveChangesAsync();
            }));


            await Task.WhenAll(tasks);
        }

        private async Task<byte[]> SaveImage(Image image, int resizeWidth)
        {
            var width = image.Width;
            var height = image.Height;

            if (width > resizeWidth)
            {
                height = (int)((double)resizeWidth / width * height);
                width = resizeWidth;
            }
            image.Mutate(i => i.Resize(new Size(width, height)));
            image.Metadata.ExifProfile = null;

            var memoryStream = new MemoryStream();

            await image.SaveAsJpegAsync(memoryStream, new JpegEncoder()
            {
                Quality = 75
            });
            return memoryStream.ToArray();
        }
    }
}
