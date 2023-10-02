namespace Project.Core.Helpers.File
{
    public interface IFileHelper
    {
        Task<string> SaveFileAsync(IFormFile file);
    }
}
