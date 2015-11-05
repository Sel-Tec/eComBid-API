using System;
using System.IO;

namespace eComBid.API.Utility
{
    public enum ImageType
    {
        jpg, png
    }

    public class ImageProcessor
    {
        public static string Upload(Byte[] binaryImage, string filename, string fileType, int? id, Type type)
        {
            if (binaryImage != null && binaryImage.Length > 0 && !string.IsNullOrEmpty(filename))
            {
                string path = GetImageSavePath(id, type);
                File.WriteAllBytes(path, binaryImage);
                return path;
            }
            return "";
        }

        public static string Upload(Byte[] binaryImage, ImageType imageType, string filename, string savePath)
        {
            if (binaryImage != null && binaryImage.Length > 0 && !string.IsNullOrEmpty(filename))
            {
                string imageFolder = ConfigMember.ImageFolder;
                string fullyQualifiedFilename = imageFolder + savePath + "/" + filename;
                FileInfo file = new System.IO.FileInfo(fullyQualifiedFilename);
                file.Directory.Create();
                File.WriteAllBytes(fullyQualifiedFilename, binaryImage);
                return fullyQualifiedFilename;
            }
            return "";
        }

        public static string GetImageSavePath(int? id, Type type)
        {
            //TODO: find better logic of implementation
            string imageFolder = ConfigMember.ImageFolder;
            string imagePath = imageFolder + "/";
            //if(type == typeof(User))
            //{
            //    imagePath += (id.ToString() + "/");
            //}
            //else if(type == typeof(Pet))
            //{
            //    Pet p = new Pet(id.Value);
            //    imagePath += (p.UserId.ToString() + "/" + p.Id.ToString() + "/");
            //}
            //else
            //{
            //    throw new NotImplementedException();
            //}

            return imagePath;
        }
    }
}
