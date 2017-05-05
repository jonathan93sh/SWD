using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Viewer
{

    interface IViewer
    {
        void setRes(int x, int y);
        void setDataArray(uint[] array2D);
        void update();
    }

    class imageViewer : IViewer
    {
        private Image _wpfView;
        private int _size_x, _size_y;
        private uint[] _array2D;

        
        private WriteableBitmap _bitmap;

        public WriteableBitmap bitmap { 
            get { return _bitmap; }
            set
            {
                _bitmap = bitmap;
            }
        }

        public imageViewer(Image wpfView)
        {
            _wpfView = wpfView;
            _size_x = 0;
            _size_y = 0;
        }

        public void setRes(int x, int y)
        {
            _size_x = x;
            _size_y = y;
            _bitmap = new WriteableBitmap(_size_x, _size_y, 96, 96, PixelFormats.BlackWhite, null);
        }

        public void setDataArray(uint[] array2D)
        {
            _array2D = array2D;
        }

        public void update()
        {
            _bitmap.WritePixels(new Int32Rect(0, 0, _size_x, _size_y), _array2D, _size_x * 4, 0);
            _wpfView.Source = _bitmap;
        }

    }
}
