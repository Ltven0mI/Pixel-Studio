using Pixel_Studio.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Studio
{
    public static class ThemeManager
    {
        public static Theme ActiveTheme { get; private set; } = new Theme();
        //private static List<string> Themes;


        public static void InitThemes()
        {

        }

        //public static string[] GetThemes()
        //{
        //    string appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //    string themeFolder = Path.Combine("/" + Resources.DataFolder + "/" + Resources.ThemeFolder);

        //    if (Directory.Exists(themeFolder))
        //    {
        //        string[] filePaths = Directory.GetFiles(themeFolder);
        //        string[] fileNames = new string[filePaths.Length];
        //        for (int i = 0; i < filePaths.Length; i++)
        //        {
        //            fileNames[i] = Path.GetFileNameWithoutExtension(filePaths[i]);
        //            System.Diagnostics.Debug.WriteLine(fileNames[i]);
        //        }
        //        return fileNames;
        //    }
        //    return null;
        //}
    }
}
