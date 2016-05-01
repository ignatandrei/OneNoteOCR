using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OneNoteOCRDll;
using Image = System.Windows.Controls.Image;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace OCROneWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        BitmapImage FromImage(Bitmap b)
        {
            using (var ms = new MemoryStream())
            {
                b.Save(ms,ImageFormat.Png);
                ms.Position = 0;
                var bi=new BitmapImage();
                bi.BeginInit();
                bi.StreamSource =new MemoryStream(ms.ToArray());
                bi.EndInit();
                bi.CacheOption= BitmapCacheOption.OnLoad;
                return bi;

            }
        }

        private void TheWindow_Loaded(object sender, RoutedEventArgs e)
        {

          
            var b=new Bitmap("ignat.jpg");
            

            var o = new OneNoteOCR();
            var arr = o.OcrTexts("ignat.jpg");


            using (Graphics gr = Graphics.FromImage(b))
            {
                gr.PageUnit= GraphicsUnit.Point;
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                gr.DrawRectangles(Pens.Red, arr.Select(it => it.Rect).ToArray());

            }
            //var src = new BitmapImage();
            //src.BeginInit();
            //src.CacheOption = BitmapCacheOption.OnLoad;
            //src.UriSource = new Uri("ignat.jpg", UriKind.Relative);
            //src.EndInit();
            var src = FromImage(b) ;
            //var i = new Image();
            //i.Stretch = Stretch.Fill;
            image.Source = src;
            //image.Stretch= Stretch.UniformToFill;
            

            //int q = src.PixelHeight;        // Image loads here
            //Sp.Children.Add(i);

        }
    }
}
