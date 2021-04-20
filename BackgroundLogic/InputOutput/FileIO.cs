using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.InputOutput
{
    public static class FileIO
    {
        /// <summary>
        /// Łączy ścieżkę roota ProgData (z Web.config) złączoną z podaną ścieżką względną
        /// </summary>
        /// <param name="partPath">Ścieżka względna (względem ProgData)</param>
        /// <returns>Zwraca ścieżkę bezwzględną</returns>
        static public string GetProgDataPath(string partPath)
        {
            return $@"{GetProgDataPath()}{partPath}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Zwraca ścieżkę ProgData z Web.config</returns>
        static public string GetProgDataPath()
        {
            return ConfigurationManager.ConnectionStrings["ProgDataDirectory"].ConnectionString;
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

        /// <summary>
        /// Odczytuje cały tekst z pliku .txt
        /// </summary>
        /// <param name="fullPath">Bezwzględna ścieżka pliku</param>
        /// <returns>Zwraca tekst</returns>
        public static string ReadTxt(string fullPath)
        {
            string text;
            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    text = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"ReadTxt method error for file: {fullPath}", e);
            }

            return text;
        }

        /// <summary>
        /// Wpisuje tekst do pliku .txt (zastępuje)
        /// </summary>
        /// <param name="fullPath">Bezwzględna ścieżka pliku</param>
        /// <param name="text">Tekst</param>
        /// <exception cref="Exception">Wyjątki zapisu do pliku</exception>
        public static void WriteText(string fullPath, string text)
        {
            System.IO.File.WriteAllText(fullPath, text);
        }


        public static bool DeleteFile(string fullPath)
        {
            if (!File.Exists(fullPath)) return false;

            File.Delete(fullPath);

            return true;
        }

    }
}
