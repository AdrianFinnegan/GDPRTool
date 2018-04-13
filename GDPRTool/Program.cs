using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GDPRTool
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Directory source not provided. E.g. C:\\MyDocuments");
                Console.WriteLine("Press any key to close...");
                Console.ReadLine();
                return;
            }

            var source = args[0];

            var directoryContents = Directory.GetFiles(source, "*.*")
                .Where(file => file.ToLower().EndsWith(".doc") || file.ToLower().EndsWith(".docx") || file.ToLower().EndsWith(".pdf"))
                .ToList();

            if (directoryContents.Count > 0)
            {
                Directory.CreateDirectory("./copy");
                UpdateContentTimeStamp(source, directoryContents);
            }
            
        }

        //Takes existing document and re-writes it to the Copy destination
        private static void UpdateContentTimeStamp(string source, List<string> directoryContents)
        {
            foreach (var file in directoryContents)
            {
                using (FileStream stream = File.OpenRead(file))
                {
                    var lastModified = File.GetLastWriteTime(file);
                    if (lastModified <= DateTime.Now.AddYears(-2))
                    {
                        string exeFolder = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                        exeFolder = exeFolder + "\\copy\\" + file.Substring(source.Length);
                        using (FileStream writeStream = File.OpenWrite(exeFolder))
                        {
                            BinaryReader reader = new BinaryReader(stream);
                            BinaryWriter writer = new BinaryWriter(writeStream);

                            // create a buffer to hold the bytes 
                            byte[] buffer = new Byte[1024];
                            int bytesRead;

                            // while the read method returns bytes
                            // keep writing them to the output stream
                            while ((bytesRead =
                                    stream.Read(buffer, 0, 1024)) > 0)
                            {
                                writeStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
            }
        }
    }
}
