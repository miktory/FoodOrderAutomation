using System;
using System.Collections.Generic;
using System.Text;
using Xamarin;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eat.EditUser
{
    public class EditUserPopUp
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.9; }
        public static double Width { get => DeviceInfo.WidthScaling*0.9; }
        public static double RowSpacing { get => Height * 0.02; }
        public static float CornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.01); }
        public static Thickness Padding { get => new Thickness(Width * 0.05, Height * 0.02, Width * 0.05, Height * 0.02); }
        public static double TopFrameWidth { get => Width * 0.2; }
        public static double TopFrameHeight { get => Height * 0.01; }
        public static Thickness TopFrameMargin { get => new Thickness(TopFrameWidth * 0.1, TopFrameHeight * 0.1, TopFrameWidth * 0.1, TopFrameHeight * 0.1); }
        public static float TopFrameCornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.03); }
        public static Thickness EntryMargin { get => new Thickness(0, Height * 0.05, 0, 0); }
        public static double UploadFrameHeight { get => Height * 0.15; }
        public static double UploadFrameWidth { get => Width * 0.5; }
        public static Thickness UploadFrameMargin { get => new Thickness(0, Height * 0.055, 0, 0); }
        public static double TitleFontSize { get => Height * 0.04; }
        public static double EntryFontSize { get => TitleFontSize * 0.65; }
        public static double SaveButtonHeight { get => Height * 0.08; }
        public static double SaveButtonWidth { get => Width * 0.3; }
        public static double SaveButtonFontSize { get => Height * 0.04; }
        public static Thickness SaveButtonPadding { get => new Thickness(SaveButtonWidth * 0.05, SaveButtonHeight * 0.02, SaveButtonWidth * 0.05, SaveButtonHeight * 0.02); }
        public static double DeleteButtonHeight { get => Height * 0.08; }
        public static double DeleteButtonWidth { get => DeleteButtonHeight; }
    }


}
