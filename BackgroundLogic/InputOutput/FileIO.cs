using BackgroundLogic.Helpers;
using BackgroundLogic.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.InputOutput
{
    public static class FileIO
    {
        private static string _progDataPath;

        public static void LoadPath(string path)
        {
            _progDataPath = path;
        }

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
            return _progDataPath;
            //return ConfigurationManager.ConnectionStrings["ProgDataDirectory"].ConnectionString;
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

        /// <summary>
        /// Metoda formatuje dowolny obrazek do kwadratu o określonym rozmiarze i zapisuje go na dysku
        /// </summary>
        /// <param name="fullPath">Ścieżka bezwzględna dla pliku</param>
        /// <param name="fileStream">Obrazek do obróbki w formacie Stream (np. InputStream)</param>
        /// <param name="size">Długość boku. Domyślnie 500</param>
        /// <exception cref="Exception"> Wiele różnych błędów (nie jestem pewien jakie)</exception>
        public static void FormatAndSaveImg(string fullPath, Stream fileStream, int size = 500)
        {

            //przycinanie i skalowanie obazka do kwadratu 500 na 500
            Bitmap img = new Bitmap(fileStream);
            if (img.Width > img.Height)
            {
                img = new Bitmap(img, new Size(Convert.ToInt32(1.0 * img.Width / img.Height * size), size));
            }
            else
            {
                img = new Bitmap(img, new Size(size, Convert.ToInt32(1.0 * img.Height / img.Width * size)));
            }

            Rectangle cropField = new Rectangle((img.Width - size) / 2, (img.Height - size) / 2, size, size); ;
            img = img.Clone(cropField, img.PixelFormat);

            img.Save(fullPath, ImageFormat.Png);
        }

        public static void CleanupImages()
        {
            List<CreatureModel> creatures = CreatureIO.GetData();

            List<string> fileNames = new List<string>();
            foreach (CreatureModel creature in creatures)
            {
                if(creature.ImagePath != null)
                    fileNames.Add(creature.ImagePath.Split('/').Last());
            }
            DirectoryInfo creatureImagesDir = new DirectoryInfo(PathLookup.ProgData + PathLookup.CreatureImages);
            foreach (var file in creatureImagesDir.GetFiles())
            {
                if (!fileNames.Contains(file.Name))
                    DeleteFile(file.FullName);
            }
        }

    }
}
