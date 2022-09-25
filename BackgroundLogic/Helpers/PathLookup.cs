using BackgroundLogic.Helpers.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.Helpers
{
    public class PathLookup : IPathMenager
    {
        public string ProgData { get; private set; }

        public string CreatureImages { get { return "ImageBase/Creatures"; } }
        public string NoImage { get { return "noImage.png"; } }


        private static string progDataPathLoader;
        public PathLookup()
        {
            if (progDataPathLoader == null)
                throw new Exception("Nie załadowano ścieżki 'ProgData' przed zainicjalizowniem klasy 'PathLookup'");
            this.ProgData = progDataPathLoader;
        }
        public static void StaticSetProgData(string value)
        {
            progDataPathLoader = value;
        }

        public string GetProgDataPath(string partPath)
        {
            return $@"{ProgData}{partPath}";
        }

        /// <summary>
        /// Oddziela (w trochę parszywy sposób) podaną ścieżkę roota od ścieżki bezwzględnej.
        /// </summary>
        /// <param name="directory">Ścieżka katalogu do oddzielenia</param>
        /// <returns>Zwraca ścieżkę względną względem podanego katalogu</returns>
        public string GetPartPath(string directory, string fullPath)
        {
            if (!fullPath.Contains(directory))
                throw new Exception("Nie można uzyskać ścieżki częściowej. Ścieżki nie pokrywają się. (w GetPartPath)");

            return fullPath.Substring(directory.Length);
        }
        public string GetPartPath(string fullPath)
        {
            return GetPartPath(ProgData, fullPath);
        }
    }
}
