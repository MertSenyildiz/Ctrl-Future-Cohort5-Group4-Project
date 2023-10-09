namespace Project.Core.Helpers.FileHelpers
{
    public interface IFileHelper
    {
        Task<string> SaveFileAsync(IFormFile file);
        Task DeleteFileAsync(string filePath);
    }
}
