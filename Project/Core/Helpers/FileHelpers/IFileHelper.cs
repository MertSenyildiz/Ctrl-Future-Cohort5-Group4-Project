namespace Project.Core.Helpers.FileHelpers
{
    public interface IFileHelper
    {
        Task<string> SaveFileAsync(IFormFile file);
        void DeleteFile(string filePath);
    }
}
