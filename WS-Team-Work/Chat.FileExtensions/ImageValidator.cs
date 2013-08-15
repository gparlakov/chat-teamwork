﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.FileExtensions
{
    public static class ImageValidator
    {
        static string[] allowedImgageFormat = new string[] {
                "jpg","jpeg", "img", "gif", "ico", "JPEG", "JFIF", "Exif", 
                "TIFF", "RAW", "GIF", "BMP", "PNG", "PPM", "PGM", "PBM", 
                "PNM", "PFM", "PAM", "WEBP"};

        public static bool CheckImageFormat(string file)
        {
            string[] gettingTheFormat = file.Split(new char[] { '.' });
            string fileFormat = gettingTheFormat[gettingTheFormat.Length - 1];


            foreach (var format in allowedImgageFormat)
            {
                if (format == fileFormat)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
