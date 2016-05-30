using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Drawing;

namespace Pixel_Studio
{
    public class Theme
    {
        public Color BackColor = Color.FromArgb(255, 34, 34, 36);
        public Color ForeColor = Color.White;

        public Color CanvasBackColor = Color.FromArgb(255, 23, 23, 24);


        // Project View Colors //
        public Color TabActiveColor = Color.FromArgb(255, 52, 127, 179);
        public Color TabFocusedColor = Color.FromArgb(255, 61, 148, 209);

        public Color TabFocusedCloseFocusedColor = Color.FromArgb(255, 80, 168, 229);
        public Color TabFocusedCloseActiveColor = Color.FromArgb(255, 37, 83, 115);

        public Color TabActiveCloseFocusedColor = Color.FromArgb(255, 61, 148, 209);
        public Color TabActiveCloseActiveColor = Color.FromArgb(255, 43, 108, 153);

        public Color ProjectButtonFocusedColor = Color.FromArgb(255, 52, 52, 53);
        public Color ProjectButtonActiveColor = Color.FromArgb(255, 52, 127, 179);


        // Layer View Colors //
        public Color LayerButtonIdleTop = Color.FromArgb(255, 36, 36, 38);
        public Color LayerButtonIdleBot = Color.FromArgb(255, 39, 39, 41);
        public Color LayerButtonIdleAlt = Color.FromArgb(255, 28, 28, 30);

        public Color LayerButtonFocusedTop = Color.FromArgb(255, 46, 46, 48);
        public Color LayerButtonFocusedBot = Color.FromArgb(255, 41, 41, 42);
        public Color LayerButtonFocusedAlt = Color.FromArgb(255, 34, 34, 36);

        public Color LayerButtonActiveTop = Color.FromArgb(255, 23, 23, 24);
        public Color LayerButtonActiveBot = Color.FromArgb(255, 18, 18, 18);
        public Color LayerButtonActiveAlt = Color.FromArgb(255, 62, 123, 152);
    }
}
