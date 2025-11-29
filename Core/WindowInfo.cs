using System;
using System.Windows.Media.Imaging;

namespace Flip_Cards_W7
{
    public class WindowInfo
    {
        public IntPtr Handle { get; set; }
        public string Title { get; set; }
        public BitmapSource Thumbnail { get; set; }
    }
}
