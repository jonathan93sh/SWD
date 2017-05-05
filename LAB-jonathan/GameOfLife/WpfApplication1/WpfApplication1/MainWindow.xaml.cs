using System;
using System.Collections.Generic;
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



namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        Viewer.imageViewer view;
        public MainWindow()
        {

            view = new Viewer.imageViewer(GOLview);

            InitializeComponent();

            Task golTask = new Task(() =>
            {
                

                GameOfLife.GameOfLife gol = new GameOfLife.GameOfLife(1000, -1, view);

                gol.RunParLoop();
            });

            golTask.Start();
            

            /*WriteableBitmap bitmap = new WriteableBitmap(1000, 1000, 96, 96, PixelFormats.BlackWhite, null);

            uint[] pixels = new uint[1000*1000];

            for(int x = 0; x < 1000; x++)
            {
                for(int y = 0; y < 1000; y++)
                {
                    int i = (1000 * y) + x;
                    pixels[i] = (i % 10 == 0)? 1u : 0u;
                }
            }
            bitmap.WritePixels(new Int32Rect(0,0,1000,1000), pixels, 1000*4,0);
            this.GOLview.Source = bitmap;*/
        }

    }
}
