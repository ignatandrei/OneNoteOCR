using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneNoteOCRDll;

namespace OneNoteTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var ocr = new OneNoteOCR();
            try
            {
                ocr.Verify();
            }
            catch (Exception)
            {
                Console.WriteLine("you do not have OneNote 15 ");
                return;
            }
            if (args.Length == 0)
            {
                Console.WriteLine("please add argument = path to the image file");
                return;
            }
            var ocrText = ocr.RecognizeImage(args[0]);
            Console.WriteLine(ocrText);
        }
    }
}
