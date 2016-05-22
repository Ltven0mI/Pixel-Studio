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

        public Color TabActiveColor = Color.FromArgb(255, 52, 127, 179);
        public Color TabFocusedColor = Color.FromArgb(255, 61, 148, 209);

        public Color TabFocusedCloseFocusedColor = Color.FromArgb(255, 80, 168, 229);
        public Color TabFocusedCloseActiveColor = Color.FromArgb(255, 37, 83, 115);

        public Color TabActiveCloseFocusedColor = Color.FromArgb(255, 61, 148, 209);
        public Color TabActiveCloseActiveColor = Color.FromArgb(255, 43, 108, 153);

        public Color ProjectButtonFocusedColor = Color.FromArgb(255, 52, 52, 53);
        public Color ProjectButtonActiveColor = Color.FromArgb(255, 52, 127, 179);
    }
}
