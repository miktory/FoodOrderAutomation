using System;
using System.Collections.Generic;
using System.Text;

namespace Eat
{
    public static class ScalingInfo
    {
        private static double _defaultWidth = 1440;
        private static double _defaultHeight = 3120;
        private static double _defaultDensity = 2;
        public static double DefaultWidth { get => _defaultWidth; }
        public static double DefaultHeight { get => _defaultHeight; }
        public static double DefaulDensity { get => _defaultDensity; }
    }
}
