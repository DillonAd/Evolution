using System;
using System.Text;

namespace Evolution.Data.Entity
{
    public class Evolution : IEvolution
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }
        public byte[] Hash { get; set; }
        public int CheckPoint { get; set; }

        public Evolution() { }

        public Evolution(string name, string file, string content)
        {
            Id = Guid.NewGuid();
            Name = name;
            FileName = file;
            Content = content;
            Hash = Encoding.ASCII.GetBytes(name + "-" + file + "-" + content);
            Content = content;
            CheckPoint = 0;
        }
    }
}
