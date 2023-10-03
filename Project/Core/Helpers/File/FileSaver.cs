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
            string filePath=Path.Combine(path,"Agilearn.png");
            if(file.Length > 0 )
            {
                filePath = Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
                using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }
            return filePath.Replace("wwwroot","~");
        }
    }
}
