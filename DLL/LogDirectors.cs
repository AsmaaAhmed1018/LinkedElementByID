using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Net.Security;

namespace DLL
{
    public static class LogDirectors
    {
        public static string FullDirctory = GetDirc(); 
        #region Images
        private static string imagesfilepath = @"\Images\in studio digital - white horizontal tagline Croped.png";
        public static string Imagesfilepath { get => FullDirctory + imagesfilepath; set => imagesfilepath = value; }
        #endregion
        #region Main
        private static string largeIconPath = @"\Icons\Link.png";
        public static string LargeIconPath { get => FullDirctory + largeIconPath; set => largeIconPath = value; }
        private static string mianIconPath = @"\Icons\Link.png";
        public static string MianIconPath { get => FullDirctory + mianIconPath; set => mianIconPath = value; }
        #endregion
        private static string GetDirc()
        {
           return new DirectoryInfo(Assembly.GetExecutingAssembly().Location).Parent.FullName;
        }
    }
}
