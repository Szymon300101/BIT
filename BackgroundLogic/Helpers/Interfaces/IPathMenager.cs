using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.Helpers.Interfaces
{
    public interface IPathMenager
    {
        string ProgData { get; }
        string GetProgDataPath(string partPath);
        string GetPartPath(string directory, string fullPath);
        string GetPartPath(string fullPath);

        string CreatureImages { get; }
        string NoImage { get; }
    }
}
