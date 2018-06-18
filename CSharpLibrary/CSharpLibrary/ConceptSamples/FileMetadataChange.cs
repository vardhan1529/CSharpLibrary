using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    public class FileMetadataChange
    {
        public static void UpdateTitle(string path, string name)
        {
            var file = ShellFile.FromFilePath(path);

            // Read and Write:
            string[] oldAuthors = file.Properties.System.Author.Value;
            string oldTitle = file.Properties.System.Title.Value;

            //file.Properties.System.Author.Value = new string[] { "Author #1", "Author #2" };
            //file.Properties.System.Title.Value = name;

            // Alternate way to Write:

            ShellPropertyWriter propertyWriter = file.Properties.GetPropertyWriter();
            propertyWriter.WriteProperty(SystemProperties.System.Author, new string[] { "Author" });
            propertyWriter.Close();
        }
    }
}
