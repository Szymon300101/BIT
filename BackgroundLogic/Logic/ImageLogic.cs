using BackgroundLogic.Helpers.Interfaces;
using BackgroundLogic.InputOutput;
using BackgroundLogic.Logic.Interfaces;
using BackgroundLogic.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.Logic
{
    internal class ImageLogic : IImageLogic
    {
        readonly IPathMenager _pathLookup;

        public ImageLogic(IPathMenager pathMenager)
        {
            _pathLookup = pathMenager;
        }

        public void CleanupImages()
        {
            List<CreatureModel> creatures = CreatureIO.GetData();

            List<string> fileNames = new List<string>();
            foreach (CreatureModel creature in creatures)
            {
                if (creature.ImagePath != null)
                    fileNames.Add(creature.ImagePath.Split('/').Last());
            }
            DirectoryInfo creatureImagesDir = new DirectoryInfo(_pathLookup.ProgData + _pathLookup.CreatureImages);
            foreach (var file in creatureImagesDir.GetFiles())
            {
                if (!fileNames.Contains(file.Name))
                    FileIO.DeleteFile(file.FullName);
            }
        }

        public void FormatAndSaveImg(string fullPath, Stream fileStream, int size = 500)
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

        public string GetBase64StringForImage(string fullPath)
        {
            using (Image loadedImage = Image.FromFile(fullPath))
            {
                return parseImageToBase64(loadedImage);
            }
        }

        private static string parseImageToBase64(Image img)
        {
            using (MemoryStream imageData = new MemoryStream())
            {
                img.Save(imageData, ImageFormat.Png);
                return Convert.ToBase64String(imageData.ToArray());
            }
        }

        public string HandleBadImagePaths(string fullPath)
        {
            bool isNull = (fullPath == null);
            if (isNull || !System.IO.File.Exists(fullPath))
                return _pathLookup.GetProgDataPath(_pathLookup.NoImage);
            else
                return fullPath;
        }
    }
}
