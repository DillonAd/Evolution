using System;
using System.Text;

namespace Evolution.Data.Entity
{
    public struct Evolution : IEvolution
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }
        public int CheckPoint { get; set; }

        public Evolution(string name, string file, string content)
        {
            Id = Guid.NewGuid();
            Name = name;
            FileName = file;
            Content = content;
            CheckPoint = 0;
        }
    }
}
