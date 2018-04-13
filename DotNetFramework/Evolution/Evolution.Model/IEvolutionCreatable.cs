using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Model
{
    public interface IEvolutionCreatable : IEvolutionTargetable
    {
        string SourceFile { get; set; }
    }
}
