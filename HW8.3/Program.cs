using System;
using System.IO;

namespace HW8._3
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к папке");
            string DirName = Console.ReadLine();

            Console.WriteLine($"Исходный размер папки {DirName} - {FolderSize(DirName)} байт.");

            DirDelet(DirName);

            Console.WriteLine($"Текущий размер папки {DirName} - {FolderSize(DirName)} байт.");
        }

        static long FolderSize(string dirName)
        {
            long folderSize = 0;

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirName);

                if (dirInfo.Exists)
                {
                    FileInfo[] files = dirInfo.GetFiles();

                    foreach (var item in files)
                    {
                        folderSize += item.Length;
                    }

                    DirectoryInfo[] subfolders = dirInfo.GetDirectories();

                    foreach (var item in subfolders)
                    {
                        folderSize += FolderSize(item.FullName);
                    }
                }

                else
                {
                    Console.WriteLine("Указан неверный путь к директории");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return folderSize;
        }

        static void DirDelet(string dirName)
        {
            long deletedSize = 0;

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirName);

                if (dirInfo.Exists)
                {

                    DirectoryInfo[] folderNames = dirInfo.GetDirectories();

                    foreach (var item in folderNames)
                    {
                        DirDelet(item.FullName);

                        if (DateTime.Now - item.LastAccessTime > TimeSpan.FromMinutes(30))
                        {
                            deletedSize += FolderSize(item.FullName);
                            item.Delete(true);
                        }
                    }

                    FileInfo[] fileNames = dirInfo.GetFiles();

                    foreach (var item in fileNames)
                    {
                        if (DateTime.Now - item.LastAccessTime > TimeSpan.FromMinutes(30))
                        {
                            deletedSize += item.Length;
                            item.Delete();
                        }
                    }


                    Console.WriteLine($"Очистка {dirInfo.FullName} от файлов и папок освобождено - {deletedSize} байт");
                }

                else
                {
                    Console.WriteLine($"По указанному адресу директория отсутствует");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
