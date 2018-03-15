using System;

namespace Evolution.Data.Entity
{
    public struct Progression : IProgression
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public byte[] Hash { get; set; }
        public bool CheckPoint { get; set; }
    }
}
