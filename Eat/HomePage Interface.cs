using System;
using System.Collections.Generic;
using System.Text;
using Xamarin;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eat.HomePage
{
    public class UserPanel
    {
        public static double UserBarHeight { get => DeviceInfo.HeightScaling * 0.23; }
        public static double UserBarWidth { get => DeviceInfo.WidthScaling * 0.9; }
        public static float UserBarCornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.03); }
        public static float UserBarPhotoCornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.3 * 0.5); }
        public static double NameFontSize { get => UserBarHeight * 0.12; }
        public static double DescriptionFontSize { get => UserBarHeight * 0.08; }
        public static double BalanceFontSize { get => UserBarHeight * 0.2; }
        public static Thickness BalanceMargin { get => new Thickness(0, 0, UserBarWidth * 0.05, 0); }
        public static Thickness GradeMargin { get => new Thickness(0, UserBarHeight * 0.15, 0, 0); }
        public static Thickness UserBarGridPadding { get => new Thickness(DeviceInfo.WidthScaling * 0.03, DeviceInfo.HeightScaling * 0.01, DeviceInfo.WidthScaling * 0.03, DeviceInfo.HeightScaling * 0.01); }
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
    public class CollectionViewPanel
    {
        public static Thickness GridPadding { get => new Thickness(0, DeviceInfo.HeightScaling * 0.01, 0, DeviceInfo.HeightScaling * 0.01); }
    }
    public class NotificationsBar
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.06; }
        public static double Width { get => DeviceInfo.WidthScaling * 0.48; }
        public static float CornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.03); }
        public static double FontSize { get => Height * 0.33; }
        public static Thickness RoleBarPadding { get => new Thickness(Width * 0.05, 0, Width * 0.05, 0); }
        public static Thickness GridPadding { get => new Thickness(Width * 0.05, Height * 0.01, Width * 0.05, Height * 0.01); }
        public static Thickness BellPadding { get => new Thickness(Height * 0.1); }

    }
    public class RoleBar
    {
        public static Thickness Padding { get => new Thickness(DeviceInfo.HeightScaling*0.22*0.1, 1, DeviceInfo.HeightScaling * 0.22 * 0.1, 1); }
        public static double FontSize { get => DeviceInfo.HeightScaling * 0.22 * 0.09; }

    }
    public class MenuSelectionBar
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.08; }
        public static double Width { get => DeviceInfo.WidthScaling * 0.9 * 0.64; }
        public static float CornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.01); }
        public static Thickness Padding { get => new Thickness(Width * 0.03, 0, Width * 0.03, 0); }
        public static double FontSizeTitle { get => Height*0.33; }
        public static double FontSizeMenuName { get => Height * 0.37*0.6; }
    }
    public class DataSelectionBar
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.08; }
        public static double Width { get => DeviceInfo.WidthScaling * 0.9 * 0.1; }
        public static float CornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.01); }
        public static Thickness Padding { get => new Thickness(Width * 0.03, 0, Width * 0.03, 0); }
        public static double FontSize { get => Height * 0.28; }
        public static double FontSizeMenuName { get => Height * 0.37 * 0.6; }

    }
    public class EatCategoriesPanel
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.30; }
        public static double Width { get => DeviceInfo.WidthScaling * 0.9; }
        public static float CornerRadiusNonCircle { get => (float)(DeviceInfo.HeightScaling * 0.01); }
        public static float CornerRadiusCircle { get => (float)(DeviceInfo.HeightScaling * 0.05); }
        public static Thickness Padding { get => new Thickness(Width * 0.12, 0, Width * 0.12, 0); }
        public static Thickness BarMargin { get => new Thickness(0, Height*0.03, 0, Height*0.03); }
        public static Thickness ArrowMargin { get => new Thickness(Height * 0.03); }
        public static double FontSize { get => Height * 0.1; }
        public static double FontSizeDescription { get => FontSize*0.5; }
        public static double CIrcleFramesSizeRequests { get => Height*0.12; }
        // 10,12,15,12
        // public static Thickness BarMargin { get => new Thickness(Width * 0.05, Height*0.03, Width * 0.05, Height*0.03); }

    }
    public class PayButton
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.08; }
        public static double Width { get => DeviceInfo.WidthScaling * 0.7; }
        public static Thickness Margin { get => new Thickness(0, DeviceInfo.HeightScaling * 0.005, 0, DeviceInfo.HeightScaling * 0.005); }
        public static double FontSize { get => DeviceInfo.HeightScaling * 0.03; }
        public static int CornerRadius { get => (int)(DeviceInfo.HeightScaling * 100); }
    }
    public class ServiceButton
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.08; }
        public static double Width { get => DeviceInfo.HeightScaling * 0.08; }
        public static Thickness ImageMargin { get => new Thickness(Height * 0.15, Height * 0.15, Height * 0.15, Height * 0.15); }
        public static double FontSize { get => DeviceInfo.HeightScaling * 0.03; }
        public static float CornerRadius { get => (int)(DeviceInfo.HeightScaling * 100); }
    }

}
