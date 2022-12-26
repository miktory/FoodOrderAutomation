using System;
using System.Collections.Generic;
using System.Text;
using Xamarin;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eat.EditCategory
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
        public static Thickness Margin { get => new Thickness(Width * 0.05, 0, Width * 0.05, 0); }
        public static Thickness Padding { get => new Thickness(Width * 0.05, Height * 0.05, Width * 0.05, Height * 0.05); }
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
    public class AddButton
    {
        public static double Width { get => DeviceInfo.WidthScaling * 0.19; }
        public static double Height { get => DeviceInfo.HeightScaling * 0.06; }
        public static int CornerRadius { get => (int)(DeviceInfo.HeightScaling * 0.01); }
        public static Thickness Margin { get => new Thickness(Width * 0.05, Height * 0.35, Width * 0.05, Height * 0.35); }
        public static double FontSize { get => Height * 0.5; }
    }
    public class EditPanelPopUp
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.78; }
        public static double Width { get => DeviceInfo.WidthScaling; }
        public static double RowSpacing { get => Height * 0.02; }
        public static float CornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.03); }
        public static Thickness Padding { get => new Thickness(Width * 0.05, Height * 0.02, Width * 0.05, Height * 0.02); }
        public static double TopFrameWidth { get => Width * 0.2; }
        public static double TopFrameHeight { get => Height * 0.01; }
        public static Thickness TopFrameMargin { get => new Thickness(TopFrameWidth * 0.1, TopFrameHeight * 0.1, TopFrameWidth * 0.1, TopFrameHeight * 0.1); }
        public static float TopFrameCornerRadius { get => (float)(DeviceInfo.HeightScaling * 0.03); }
        public static Thickness CostMargin { get => new Thickness(0, Height * 0.1, 0, 0); }
        public static double NameFontSize { get => Height * 0.04; }
        public static double DescriptionFontSize { get => NameFontSize * 0.65; }
        public static double CostFontSize { get => NameFontSize * 0.8; }
        public static double SaveButtonHeight { get => Height * 0.08; }
        public static double SaveButtonFontSize { get => Height * 0.04; }
        public static double DeleteButtonHeight { get => Height* 0.08; }
        public static double DeleteButtonWidth { get => DeleteButtonHeight; }

}
    public class CreateDishPopUp
    {
        public static double Height { get => DeviceInfo.HeightScaling * 0.6; }
        public static double Width { get => DeviceInfo.WidthScaling*0.9; }
        public static double RowSpacing { get => Height * 0.03; }
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
        public static double EntryFontSize { get => TitleFontSize * 0.7; }
        public static double SaveButtonHeight { get => Height * 0.08; }
        public static double SaveButtonWidth { get => Width * 0.3; }
        public static double SaveButtonFontSize { get => Height * 0.045; }
    }


}
