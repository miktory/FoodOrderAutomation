using System;
using System.Collections.Generic;
using System.Text;
using Xamarin;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eat.SelectGrade
{
    public class SelectGradePopUp
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.4; }
        public static double Width { get => DeviceInfo.WidthScaling*0.9; }
        public static double RowSpacing { get => Height * 0.02; }
        public static float CornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.01); }
        public static Thickness GridPadding { get => new Thickness(Width * 0.1, Height * 0.05, Width * 0.1, Height * 0.05); }
        public static double GridRowSpacing { get => Height * 0.05; }
        public static double TitleFontSize { get => Height * 0.07; }
        public static double ButtonsFontSize { get => Height * 0.04; }

    }
    public class CollectionFrame
    {
        public static double Height { get => SelectGradePopUp.Height * 0.65; }
        public static double Width { get => DeviceInfo.WidthScaling * 0.8; }
        public static double RowSpacing { get => Height * 0.02; }
        public static Thickness GridPadding { get => new Thickness(0, Height * 0.05, 0, Height * 0.05); }
        public static Thickness RoleNameMargin { get => new Thickness(Width * 0.05, 0, 0, 0); }
        public static double RoleNameFontSize { get => Height * 0.1; }
        public static float CornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.01); }

    }
    public class SelectionCircle
    {
        public static double Height { get => CollectionFrame.Height * 0.08; }
        public static double Width { get => Height; }
        public static Thickness InternalCircleMargin { get => new Thickness(Width * 0.2); }
        public static float CornerRadius { get => (float)(DeviceInfo.HeightScaling * 100); }

    }



}
