using System;

namespace Evolution.Data.Entity
{
    public interface IProgression
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string FileName { get; set; }
        byte[] Hash { get; set; }
        bool CheckPoint { get; set; }
    }
}
