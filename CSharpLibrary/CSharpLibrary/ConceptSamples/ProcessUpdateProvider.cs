using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    class ProcessUpdateProvider
    {
        public static void ProcessRequest()
        {
            Console.WriteLine("This transters the files from source to destination");
            Console.Write("Enter the source file path: ");
            var path = Console.ReadLine();
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Invalid file path");
                return;
            }

            var parentPath = Directory.GetParent(path).FullName;
            //Create destination folder
            var destinationPath = parentPath + "\\Destination";
            if (Directory.Exists(destinationPath))
            {
                var count = 1;
                while (Directory.Exists(destinationPath + count))
                {
                    count++;
                }

                destinationPath += count;
            }

            Directory.CreateDirectory(destinationPath);
            Console.WriteLine(DateTime.Now + " Destination Folder " + destinationPath + "has been created");

            foreach (var r in CopyFiles(path, destinationPath))
            {
                Console.WriteLine(r.Message);

                if (!r.Success)
                {
                    Console.WriteLine("Error Message: " + r.ErrorMessage);
                }
            }
        }

        private static IEnumerable<FileTransferInfo> CopyFiles(string path, string destinationPath)
        {
            foreach (var filePath in Directory.GetFiles(path))
            {
                Console.WriteLine(string.Format("{0} Copying File: {1}", DateTime.Now, filePath));
                var transferInfo = new FileTransferInfo() { Success = true };
                try
                {
                    var fileName = filePath.Split('\\').Last();

                    //Using filecopy
                    File.Copy(filePath, destinationPath + "\\" + fileName);

                    //var f = File.Create(destinationPath + "\\" + fileName);
                    //Using StreamReader
                    //using (var fileReader = new StreamReader(filePath))
                    //{
                    //    var b = Encoding.ASCII.GetBytes(fileReader.ReadToEnd());
                    //    f.Write(b, 0, b.Length);
                    //    f.Close();
                    //}

                    //Using extension methods
                    //var fileBytes = File.ReadAllBytes(filePath);
                    //f.Write(fileBytes, 0, fileBytes.Length);

                    transferInfo.Message = string.Format("{0} Copied File: {1}", DateTime.Now, filePath);
                }
                catch (Exception ex)
                {
                    //Adding a small delay to see process
                    System.Threading.Thread.Sleep(1000);
                    transferInfo.Success = false;
                    transferInfo.ErrorMessage = ex.Message;
                    transferInfo.Message = string.Format("{0} Copy Failed File: {1}", DateTime.Now, filePath);
                }

                yield return transferInfo;
            }
        }

        private class FileTransferInfo
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
            public string Message { get; set; }
        }
    }
}
