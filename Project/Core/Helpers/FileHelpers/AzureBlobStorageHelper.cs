using Azure.Identity;
using Azure.Storage.Blobs;

namespace Project.Core.Helpers.FileHelpers
{
    public class AzureBlobStorageHelper : IFileHelper
    {
        string _connectionStirng;
        string path;
        string defaultImage;
        public AzureBlobStorageHelper(string connectionString,string path)
        {
            this.path = path;

            this.defaultImage = "default.png";

            this._connectionStirng = connectionString;

        }
        public async Task DeleteFileAsync(string filePath)
        {
            string filePathToDelete = Path.Combine(path, defaultImage);
            if(filePath!= filePathToDelete)
            {
                var blobServiceClient = new BlobServiceClient(_connectionStirng);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("img");
                var fileToDeleteName = filePath.Split("/").Last();
                BlobClient blobClient = containerClient.GetBlobClient(fileToDeleteName);
                await blobClient.DeleteAsync();
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            string filePathToSave = Path.Combine(path, defaultImage);
            if(file is not null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePathToSave = Path.Combine(path,fileName);
                var blobServiceClient = new BlobServiceClient(_connectionStirng);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("img");
                BlobClient blobClient=containerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(file.OpenReadStream());
            }
            return filePathToSave;
        }
    }
}
