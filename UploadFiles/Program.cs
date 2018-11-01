using System;

namespace UploadFiles
{
    public class Program
    {
        static void Main(string[] args)
        {
            // https://i.imgur.com/dN17tXy.png
            var imgur = new ImgurUpload("bdae1d60e1130e7");
            var result = imgur.PostFile(@"E:\Media\image.png").Result;
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
