// Copyright (c) 2001-2021 Aspose Pty Ltd. All Rights Reserved.

using System.Drawing;

namespace Aspose.Slides.WebExtensions.Helpers
{
    public static class ColorHelper
    {
        public static string GetRrbaColorString(Color color)
        {
            return string.Format("rgba({0}, {1}, {2}, {3})", color.R, color.G, color.B, NumberHelper.ToCssNumber(color.A / 255f));
        }
        public static Color DensifyColor(Color color)
        {
            return Color.FromArgb((byte)(color.R / 2.5), (byte)(color.G / 2.5), (byte)(color.B / 2.5));
        }
    }
}