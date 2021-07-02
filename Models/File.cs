namespace FileBot.Models
{
    public class File
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long DirectoryId { get; set; }

        public virtual Directory Directory { get; set; }

        public byte[] Data { get; set; }
    }
}
