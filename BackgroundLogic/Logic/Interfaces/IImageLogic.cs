using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.Logic.Interfaces
{
    public interface IImageLogic
    {
        void FormatAndSaveImg(string fullPath, Stream fileStream, int size);
        string GetBase64StringForImage(string fullPath);
        void CleanupImages();
        string HandleBadImagePaths(string fullPath);
    }
}
