using System;
using System.Collections.Generic;
using System.Text;
using Xamarin;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eat.LoginPage
{
    public class Logo
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.43; }
    }
    public class LogPassPanel
    {
        public static Thickness GridPadding { get => new Thickness(DeviceInfo.WidthScaling * 0.03, DeviceInfo.HeightScaling * 0.005, DeviceInfo.WidthScaling * 0.03, DeviceInfo.HeightScaling * 0.005); }
        public static double Height { get => DeviceInfo.HeightScaling *0.18; }
        public static float CornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.01); }
        public static double FontSize { get => Height * 0.10; }
        
    }
    public class LoginButton
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.18; }
        public static double FontSize { get => Height * 0.15; }
        public static float CornerRadius { get => (float)(DeviceInfo.HeightScaling * 100); }
    }

}
