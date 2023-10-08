using System.IO;
namespace Project.Core.Helpers.FileHelpers
{
    public class FileSaver : IFileHelper
    {
        string path;
        string defaultImage;
        public FileSaver(string path)
        {

            this.path = path;
            defaultImage = "default.png";
        }
        public async Task<string> SaveFileAsync(IFormFile? file)
        {
            string filePath=Path.Combine(path,defaultImage);
            if(file is not null)
            {
                filePath = Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                using FileStream fileStream = new(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }
            return filePath.Replace("wwwroot","");
        }

        public void DeleteFile(string filePath)
        {
            var imgPath = Path.Combine(path, defaultImage);
            var deletePath = $"{Environment.CurrentDirectory}/wwwroot/{filePath}".Replace("/", "\\");
            if (File.Exists(deletePath))
            {
                if (filePath != imgPath.Replace("wwwroot",""))
                {
                    File.Delete(deletePath);
                }
            }
        }
    }
}
