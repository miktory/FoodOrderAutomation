using System;
using System.Collections.Generic;
using System.Text;
using Xamarin;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eat.ServiceMenu
{
    public class TopBar
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.11; }
        public static double Width { get => DeviceInfo.WidthScaling; }
        public static double BackArrowHeight { get => Height*0.16; }
        public static double FontSize { get => Height * 0.25; }
    }
    public class SelectionPanel
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.7; }
        public static double Width { get => DeviceInfo.WidthScaling; }
        public static double FontSize { get => Height * 0.03; }
        public static Thickness FramePadding { get => new Thickness(0, Height * 0.05, 0, Height * 0.05); }
        public static Thickness GridPadding { get => new Thickness(0, 0, 0, Height * 0.03); }
    }   

}
