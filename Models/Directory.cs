using System.Collections.Generic;

namespace FileBot.Models
{
    public class Directory
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long? ParentId { get; set; }

        public virtual Directory Parent { get; set; }

        public virtual List<Directory> Directories { get; set; } = new List<Directory>();

        public virtual List<File> Files { get; set; } = new List<File>();
    }
}
