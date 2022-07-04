    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.Helpers
{
    public static class PathLookup
    {
        public static string ProgData { get; set; }

        public readonly static string CreatureImages = "ImageBase/Creatures";



        static public string GetProgDataPath(string partPath)
        {
            return $@"{ProgData}{partPath}";
        }



        /// <summary>
        /// Oddziela (w trochę parszywy sposób) podaną ścieżkę roota od ścieżki bezwzględnej.
        /// </summary>
        /// <param name="directory">Ścieżka katalogu do oddzielenia (wyjście metody GetStoragePath/GetProgDataPath)</param>
        /// <param name="fullPath">Ścieżka bezwzględna</param>
        /// <returns>Zwraca ścieżkę względną względem podanego katalogu</returns>
        static public string GetPartPath(string directory, string fullPath)
        {
            if (!fullPath.Contains(directory))
                throw new Exception("Nie można uzyskać ścieżki częściowej. Ścieżki nie pokrywają się. (w GetPartPath)");

            return fullPath.Substring(directory.Length);
        }
        static public string GetPartPath(string fullPath)
        {
            return GetPartPath(ProgData, fullPath);
        }
    }
}
