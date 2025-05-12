namespace ChatEmailProject.Entities
{
    public class Message
    {
        public int MessageID { get; set; }
        public string SenderEmail { get; set; }
        public string RecieverEmail { get; set; }
        public string Subject { get; set; }
        public string MessageDetail { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
    }
}
