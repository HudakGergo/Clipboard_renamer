using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;



namespace Clipboard_renamer
{
    internal class Program
    {
        [STAThread]
        /* I made this short script that solves the issue where you cannot paste multiple screenshots in an AI chat because they all have the same name.  */
        static void Main(string[] args)
        {
            //Discarding previousy created images from Temp folder to prevent storage pile up
            string[] oldFiles = Directory.GetFiles(Path.GetTempPath(), "img_*.png");
            foreach (var item in oldFiles)
            {
                try
                {
                    File.Delete(item);
                }
                catch
                {

                }
            }

        re:
            Console.WriteLine("Checking clipboard...");

            if (Clipboard.ContainsImage())
            {
                
                Console.WriteLine("Image found!");
                var image = Clipboard.GetImage();
                string tempPath = Path.GetTempPath();
                string fileName = $"img_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                string fullPath = Path.Combine(tempPath, fileName);

                
                image.Save(fullPath,ImageFormat.Png);
                Console.WriteLine($"Image saved to: {fullPath}");

                
                StringCollection filePaths = new StringCollection();
                filePaths.Add(fullPath);

                
                Clipboard.SetFileDropList(filePaths);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("File copied to clipboard!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No image found in clipboard!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("Type 'Exit' to close, press enter to repeat!");
            string response = Console.ReadLine();
            if (response.ToLower() != "exit")
            {
                Console.Clear();
                goto re;
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
