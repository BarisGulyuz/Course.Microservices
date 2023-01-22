namespace FreeCourse.Services.PhotoStock.Dtos
{
    public class PhotoDto
    {
        public PhotoDto(string url, PhotoStatus status)
        {
            Url = url;
            Status = status;
        }
        public string Url { get; set; }
        public PhotoStatus Status { get; set; }

    }

    public enum PhotoStatus
    {
        Created,
        Deleted
    }
}
