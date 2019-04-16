using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Wrong parameters");
                PrintHelp();
                return;
            }
            var extension = args[0].ToLower();
            var program = new Program();

            var count =  program.CountDirLines(System.IO.Directory.GetCurrentDirectory(), extension);
            program.PrintResult(count);

        }

        private void PrintResult(int count)
        {
            Console.WriteLine(@"Meaningful lines in current directory : {0}", count);
            Console.ReadLine();
        }

        private int CountDirLines(string directory, string extension)
        {
            var count = 0;

            var dirInfo = new System.IO.DirectoryInfo(directory);
            var subDir = dirInfo.GetDirectories();

            for (var i = 0; i < subDir.Length; i++)
            { 
                count += this.CountDirLines(subDir[i].FullName, extension);
            }

            var files = dirInfo.GetFiles(); 

            for (var i = 0; i < files.Length; i++)
            {
                count += this.CountFileLines(files[i], extension);
            }

            return count;
        }

        private int CountFileLines(System.IO.FileInfo file, string extension)
        {
            var count = 0;
            if (file.Extension.ToLower().Equals(extension))
            {
                var lines = System.IO.File.ReadAllLines(file.FullName);

                foreach (var line in lines)
                {
                    if (!String.IsNullOrWhiteSpace(line) && !line.Trim().StartsWith("//"))
                    {
                        ++count;
                    }
                }
            }
            return count;
        }
        static void PrintHelp()
        {
            Console.WriteLine("program.exe extension");
            Console.WriteLine("extension \tfile extension");
        }
    }
}
