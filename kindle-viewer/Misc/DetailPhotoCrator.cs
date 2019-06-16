using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace kindle_viewer.Misc
{
    class DetailPhotoCrator
    {
        static int width = 1920;
        static int height = 1080;

        private CanvasRenderTarget canvas;
        private CanvasDrawingSession ds;

        DetailPhotoCrator()
        {
            // https://stackoverflow.com/questions/46326260/how-to-draw-text-on-an-image-and-save-it-using-uwp-for-windows-10
            // https://edi.wang/post/2017/3/2/windows-10-uwp-save-inkcanvas-to-picture
            // https://stackoverflow.com/questions/11876302/drawing-image-to-canvas-in-metro-app
            CanvasDevice device = CanvasDevice.GetSharedDevice();
            CanvasRenderTarget renderTarget = new CanvasRenderTarget(device, DetailPhotoCrator.width, DetailPhotoCrator.height, 96);

            ds = canvas.CreateDrawingSession();
            canvas = renderTarget;
        }

        private string getBackgroundUrl()
        {
            return "https://kindle.annatarhe.com/coffee-d3ec79a0efd30ac2704aa2f26e72cb28.jpg";
        }

        private DetailPhotoCrator setBackground()
        {
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(this.getBackgroundUrl(), UriKind.RelativeOrAbsolute));
            // .Background = ib;

            return this;

        }
    }
}
