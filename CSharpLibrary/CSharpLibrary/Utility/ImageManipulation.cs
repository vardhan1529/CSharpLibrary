using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CSharpLibrary.Utility
{
    class ImageManipulation
    {
        public static string GetBase64EncodedString(string path, string format)
        {
            var data = File.ReadAllBytes(path);
            var img = Image.FromFile(path);
            img.Save(@"D:\test.jpg");
            return null;
        }
    }
}
