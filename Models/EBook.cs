namespace ScientiaMobilis.Models
{
    public class EBook
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string FileUrl { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
