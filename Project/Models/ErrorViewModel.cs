namespace Project.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        //testing git 
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}