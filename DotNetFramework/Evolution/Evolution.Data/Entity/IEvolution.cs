using System;

namespace Evolution.Data.Entity
{
    public interface IEvolution
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string FileName { get; set; }
        byte[] Hash { get; set; }
        string Content { get; set; }
        int CheckPoint { get; set; }
    }
}
