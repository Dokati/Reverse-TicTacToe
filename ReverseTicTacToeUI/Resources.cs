using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReverseTicTacToeUI
{
    public class Resources
    {
        private static readonly string sr_BoardBackgroundImage = "BoardBackground.jpeg";
        private static readonly string sr_CellBackgroundImage = "CellBackground.jpeg";
        private static readonly string sr_CellBackgroundImageHovering = "CellBackgroundHovering.jpeg";
        private static readonly string sr_OIconImage = "OIcon.jpeg";
        private static readonly string sr_XIconImage = "XIcon.jpeg";
        private static readonly string sr_TitleIcon = "titleicon.ico";
        private static readonly string sr_ResourcesFolderPath;

        static Resources()
        {
            sr_ResourcesFolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Resources");
        }

        public static string TitleIcon
        {
            get
            {
                return sr_TitleIcon;
            }
        }
        public static string BoardBackground
        {
            get
            {
                return sr_BoardBackgroundImage;
            }
        }
        public static string CellBackgroundHovering
        {
            get
            {
                return sr_CellBackgroundImageHovering;
            }
        }

        public static string CellBackground
        {
            get
            {
                return sr_CellBackgroundImage;
            }
        }

        public static string OIcon
        {
            get
            {
                return sr_OIconImage;
            }
        }

        public static string XIcon
        {
            get
            {
                return sr_XIconImage;
            }
        }

        public static string ResourcesFolderPath
        {
            get
            {
                return sr_ResourcesFolderPath;
            }
        }
    }
}
