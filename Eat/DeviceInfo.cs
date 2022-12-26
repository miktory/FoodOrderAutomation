using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin;
using System.Globalization;

namespace Eat
{
    public static class DeviceInfo
    {
        private static DisplayInfo _mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
        // Width (in pixels)
        private static double _width = _mainDisplayInfo.Width;
        // Height (in pixels)
        private static double _height = _mainDisplayInfo.Height;
        private static double _density = _mainDisplayInfo.Density;
        private static double _widthScaling = DeviceInfo.Width / DeviceInfo.Density;
        private static double _heightScaling = DeviceInfo.Height / DeviceInfo.Density;
        public static double Width { get => _width; }
        public static double Height { get => _height; }
        public static double Density { get => _density; }
        public static double WidthScaling { get => _widthScaling; }
        public static double HeightScaling { get => _heightScaling; }


        public static object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * (double)parameter;

        }
    }
}
