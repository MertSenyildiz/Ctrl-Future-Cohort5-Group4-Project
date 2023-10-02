namespace Project.Core.Helpers.File
{
    public class FileSaver : IFileHelper
    {
        String path;
        public FileSaver(string path)
        {

            this.path = path;

        }
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var stream=file.OpenReadStream();
            
            string filePath=Path.Combine(path, Guid.NewGuid().ToString() +Path.GetExtension(file.FileName));
            using FileStream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return filePath.Replace("wwwroot","~");
        }
    }
}
