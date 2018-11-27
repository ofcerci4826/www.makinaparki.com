using System.IO;

namespace Vegatro.Core.IO.Models
{
    public class Message
    {
        public string Path { get; set; }
        public string Text { get; set; }
        public FileMode Mode { get; set; }
        public bool AddNewLine { get; set; }
    }
}
