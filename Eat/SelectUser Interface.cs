using System;
using System.Collections.Generic;
using System.Text;
using Xamarin;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eat.SelectUser
{
    public class TopBar
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.11; }
        public static double Width { get => DeviceInfo.WidthScaling; }
        public static double BackArrowHeight { get => Height*0.16; }
        public static double FontSize { get => Height * 0.25; }
    }
    public class SearchBar
    { 
        public static double Height { get => DeviceInfo.HeightScaling * 0.08; }
        public static double Width { get => DeviceInfo.WidthScaling; }
        public static float CornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.01); }
        public static Thickness Margin { get => new Thickness(Width * 0.05, Height * 0.22, Width * 0.05, Height * 0.22); }
        public static double FontSize { get => Height * 0.18; }
    }

    public class SelectionPanel
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.58; }
        public static double Width { get => DeviceInfo.WidthScaling; }
        public static float CornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.01); }
        public static Thickness Margin { get => new Thickness(Width * 0.05, 0, Width * 0.05, Height* 0.05); }
        public static Thickness Padding { get => new Thickness(Width * 0.05, Height * 0.05, Width * 0.05, Height * 0.05); }
        public static Thickness CollectionViewPadding { get => new Thickness(Width * 0.03, Height * 0.03, Width * 0.03, 0); }
        public static double DishNameFontSize { get => DishFrameHeight * 0.14; }
        public static double DishDescriptionFontSize { get => DishNameFontSize*0.7; }
        public static double DishFrameHeight { get => Height *0.3; }
        public static double DishFrameWidth { get => DishFrameHeight; }
        public static Thickness DishImageMargin { get => new Thickness(DishFrameWidth * 0.1, DishFrameHeight * 0.1, DishFrameWidth * 0.1, DishFrameHeight * 0.1); }
        public static float DishFrameCornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.01); }
        public static double DishStatusHeight { get => DishFrameHeight * 0.05; }
        public static double DishStatusWidth { get => DishStatusHeight; }
        public static Thickness DishStatusMargin { get => new Thickness(DishFrameWidth * 0.02, DishFrameHeight * 0.02, DishFrameWidth * 0.02, DishFrameHeight * 0.02); }
        public static float DishStatusCornerRadius { get => (float)(DeviceInfo.HeightScaling * 100); }      
    }
    public class ShortUserPanel
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.09; }
        public static double Width { get => DeviceInfo.WidthScaling * 0.9; }
        public static float CornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.03); }
        public static float PhotoCornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.3 * 0.5); }
        public static double NameFontSize { get => Height * 0.3; }
        public static double GradeFontSize { get => Height * 0.10; }
        public static Thickness GridPadding { get => new Thickness(DeviceInfo.WidthScaling * 0.03, DeviceInfo.HeightScaling * 0.01, DeviceInfo.WidthScaling * 0.03, DeviceInfo.HeightScaling * 0.01); }
    }
    public class AddButton
    {
        public static double Width { get => DeviceInfo.WidthScaling * 0.19; }
        public static double Height { get => DeviceInfo.HeightScaling * 0.06; }
        public static int CornerRadius { get => (int)(DeviceInfo.HeightScaling * 0.01); }
        public static Thickness Margin { get => new Thickness(Width * 0.05, 0, Width * 0.05, Height * 0.35); }
        public static double FontSize { get => Height * 0.5; }
    }


}
