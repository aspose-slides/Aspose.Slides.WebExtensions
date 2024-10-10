// Copyright (c) 2001-2020 Aspose Pty Ltd. All Rights Reserved.

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Aspose.Slides.WebExtensions.Helpers
{
    internal class PatternGenerator
    {
        static PatternGenerator()
        {
            m_coords = new int[54][];
            m_coords[(int)PatternStyle.Percent05] = new int[] { 0, 0, 4, 4 };
            m_coords[(int)PatternStyle.Percent10] = new int[] { 0, 0, 4, 2, 0, 4, 4, 6 };
            m_coords[(int)PatternStyle.Percent20] = new int[] { 0, 0, 2, 2, 4, 4, 6, 6, 4, 0, 6, 2, 0, 4, 2, 6 };
            m_coords[(int)PatternStyle.Percent25] = new int[] { 0, 0, 4, 0, 2, 1, 6, 1, 0, 2, 4, 2, 2, 3, 6, 3, 0, 4, 4, 4, 2, 5, 6, 5, 0, 6, 4, 6, 2, 7, 6, 7 };
            m_coords[(int)PatternStyle.Percent30] = new int[] { 0, 0, 2, 0, 4, 0, 6, 0, 1, 1, 5, 1, 0, 2, 2, 2, 4, 2, 6, 2, 3, 3, 7, 3, 0, 4, 2, 4, 4, 4, 6, 4, 1, 5, 5, 5, 0, 6, 2, 6, 4, 6, 6, 6, 3, 7, 7, 7 };
            m_coords[(int)PatternStyle.Percent40] = new int[] { 0, 0, 2, 0, 4, 0, 6, 0, 1, 1, 3, 1, 5, 1, 7, 1, 0, 2, 2, 2, 4, 2, 6, 2, 1, 3, 3, 3, 7, 3, 0, 4, 2, 4, 4, 4, 6, 4, 1, 5, 3, 5, 5, 5, 7, 5, 0, 6, 2, 6, 4, 6, 6, 6, 3, 7, 5, 7, 7, 7 };
            m_coords[(int)PatternStyle.Percent50] = new int[] { 0, 0, 2, 0, 4, 0, 6, 0, 1, 1, 3, 1, 5, 1, 7, 1, 0, 2, 2, 2, 4, 2, 6, 2, 1, 3, 3, 3, 5, 3, 7, 3, 0, 4, 2, 4, 4, 4, 6, 4, 1, 5, 3, 5, 5, 5, 7, 5, 0, 6, 2, 6, 4, 6, 6, 6, 1, 7, 3, 7, 5, 7, 7, 7 };
            m_coords[(int)PatternStyle.Percent60] = new int[] { 0, 0, 1, 0, 2, 0, 4, 0, 5, 0, 6, 0, 1, 1, 3, 1, 5, 1, 7, 1, 0, 2, 2, 2, 3, 2, 4, 2, 6, 2, 7, 2, 1, 3, 3, 3, 5, 3, 7, 3, 0, 4, 1, 4, 2, 4, 4, 4, 5, 4, 6, 4, 1, 5, 3, 5, 5, 5, 7, 5, 0, 6, 2, 6, 3, 6, 4, 6, 6, 6, 7, 6, 1, 7, 3, 7, 5, 7, 7, 7 };
            m_coords[(int)PatternStyle.Percent70] = new int[] { 1, 0, 2, 0, 3, 0, 5, 0, 6, 0, 7, 0, 0, 1, 1, 1, 3, 1, 4, 1, 5, 1, 7, 1, 1, 2, 2, 2, 3, 2, 5, 2, 6, 2, 7, 2, 0, 3, 1, 3, 3, 3, 4, 3, 5, 3, 7, 3, 1, 4, 2, 4, 3, 4, 5, 4, 6, 4, 7, 4, 0, 5, 1, 5, 3, 5, 4, 5, 5, 5, 7, 5, 1, 6, 2, 6, 3, 6, 5, 6, 6, 6, 7, 6, 0, 7, 1, 7, 3, 7, 4, 7, 5, 7, 7, 7 };
            m_coords[(int)PatternStyle.Percent75] = new int[] { 1, 0, 2, 0, 3, 0, 5, 0, 6, 0, 7, 0, 0, 1, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 1, 0, 2, 1, 2, 3, 2, 4, 2, 5, 2, 7, 2, 0, 3, 1, 3, 2, 3, 3, 3, 4, 3, 5, 3, 6, 3, 7, 3, 1, 4, 2, 4, 3, 4, 5, 4, 6, 4, 7, 4, 0, 5, 1, 5, 2, 5, 3, 5, 4, 5, 5, 5, 6, 5, 7, 5, 0, 6, 1, 6, 3, 6, 4, 6, 5, 6, 7, 6, 0, 7, 1, 7, 2, 7, 3, 7, 4, 7, 5, 7, 6, 7, 7, 7 };
            m_coords[(int)PatternStyle.Percent80] = new int[] { 0, 0, 1, 0, 2, 0, 4, 0, 5, 0, 6, 0, 7, 0, 0, 1, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 1, 0, 2, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 0, 3, 1, 3, 2, 3, 3, 3, 4, 3, 5, 3, 6, 3, 7, 3, 0, 4, 1, 4, 2, 4, 4, 4, 5, 4, 6, 4, 7, 4, 0, 5, 1, 5, 2, 5, 3, 5, 4, 5, 5, 5, 6, 5, 7, 5, 0, 6, 1, 6, 2, 6, 3, 6, 4, 6, 5, 6, 6, 6, 0, 7, 1, 7, 2, 7, 3, 7, 4, 7, 5, 7, 6, 7, 7, 7 };
            m_coords[(int)PatternStyle.Percent90] = new int[] { 0, 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7, 0, 0, 1, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 1, 0, 2, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 0, 3, 1, 3, 2, 3, 3, 3, 5, 3, 6, 3, 7, 3, 0, 4, 1, 4, 2, 4, 3, 4, 4, 4, 5, 4, 6, 4, 7, 4, 0, 5, 1, 5, 2, 5, 3, 5, 4, 5, 5, 5, 6, 5, 7, 5, 0, 6, 1, 6, 2, 6, 3, 6, 4, 6, 5, 6, 6, 6, 7, 6, 1, 7, 2, 7, 3, 7, 4, 7, 5, 7, 6, 7, 7, 7 };
            m_coords[(int)PatternStyle.LightDownwardDiagonal] = new int[] { 0, 0, 4, 0, 1, 1, 5, 1, 2, 2, 6, 2, 3, 3, 7, 3, 0, 4, 4, 4, 1, 5, 5, 5, 2, 6, 6, 6, 3, 7, 7, 7 };
            m_coords[(int)PatternStyle.LightUpwardDiagonal] = new int[] { 3, 0, 7, 0, 2, 1, 6, 1, 1, 2, 5, 2, 0, 3, 4, 3, 3, 4, 7, 4, 2, 5, 6, 5, 1, 6, 5, 6, 0, 7, 4, 7 };
            m_coords[(int)PatternStyle.DarkDownwardDiagonal] = new int[] { 0, 0, 1, 0, 4, 0, 5, 0, 1, 1, 2, 1, 5, 1, 6, 1, 2, 2, 3, 2, 6, 2, 7, 2, 0, 3, 3, 3, 4, 3, 7, 3, 0, 4, 1, 4, 4, 4, 5, 4, 1, 5, 2, 5, 5, 5, 6, 5, 2, 6, 3, 6, 6, 6, 7, 6, 0, 7, 3, 7, 4, 7, 7, 7 };
            m_coords[(int)PatternStyle.DarkUpwardDiagonal] = new int[] { 2, 0, 3, 0, 6, 0, 7, 0, 1, 1, 2, 1, 5, 1, 6, 1, 0, 2, 1, 2, 4, 2, 5, 2, 0, 3, 3, 3, 4, 3, 7, 3, 2, 4, 3, 4, 6, 4, 7, 4, 1, 5, 2, 5, 5, 5, 6, 5, 0, 6, 1, 6, 4, 6, 5, 6, 0, 7, 3, 7, 4, 7, 7, 7 };
            m_coords[(int)PatternStyle.WideDownwardDiagonal] = new int[] { 0, 0, 0, 1, 1, 0, 1, 1, 1, 2, 2, 1, 2, 2, 2, 3, 3, 2, 3, 3, 3, 4, 4, 3, 4, 4, 4, 5, 5, 4, 5, 5, 5, 6, 6, 5, 6, 6, 6, 7, 7, 6, 7, 7, 7, 0, 0, 7 };
            m_coords[(int)PatternStyle.WideUpwardDiagonal] = new int[] { 7, 0, 7, 1, 6, 0, 6, 1, 6, 2, 5, 1, 5, 2, 5, 3, 4, 2, 4, 3, 4, 4, 3, 3, 3, 4, 3, 5, 2, 4, 2, 5, 2, 6, 1, 5, 1, 6, 1, 7, 0, 6, 0, 7, 0, 0, 7, 7 };
            m_coords[(int)PatternStyle.LightVertical] = new int[] { 0, 0, 4, 0, 0, 1, 4, 1, 0, 2, 4, 2, 0, 3, 4, 3, 0, 4, 4, 4, 0, 5, 4, 5, 0, 6, 4, 6, 0, 7, 4, 7 };
            m_coords[(int)PatternStyle.LightHorizontal] = new int[] { 0, 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7, 0, 0, 4, 1, 4, 2, 4, 3, 4, 4, 4, 5, 4, 6, 4, 7, 4 };
            m_coords[(int)PatternStyle.NarrowVertical] = new int[] { 0, 0, 2, 0, 4, 0, 6, 0, 0, 1, 2, 1, 4, 1, 6, 1, 0, 2, 2, 2, 4, 2, 6, 2, 0, 3, 2, 3, 4, 3, 6, 3, 0, 4, 2, 4, 4, 4, 6, 4, 0, 5, 2, 5, 4, 5, 6, 5, 0, 6, 2, 6, 4, 6, 6, 6, 0, 7, 2, 7, 4, 7, 6, 7 };
            m_coords[(int)PatternStyle.NarrowHorizontal] = new int[] { 0, 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7, 0, 0, 2, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 0, 4, 1, 4, 2, 4, 3, 4, 4, 4, 5, 4, 6, 4, 7, 4, 0, 6, 1, 6, 2, 6, 3, 6, 4, 6, 5, 6, 6, 6, 7, 6 };
            m_coords[(int)PatternStyle.DarkVertical] = new int[] { 0, 0, 1, 0, 4, 0, 5, 0, 0, 1, 1, 1, 4, 1, 5, 1, 0, 2, 1, 2, 4, 2, 5, 2, 0, 3, 1, 3, 4, 3, 5, 3, 0, 4, 1, 4, 4, 4, 5, 4, 0, 5, 1, 5, 4, 5, 5, 5, 0, 6, 1, 6, 4, 6, 5, 6, 0, 7, 1, 7, 4, 7, 5, 7 };
            m_coords[(int)PatternStyle.DarkHorizontal] = new int[] { 0, 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7, 0, 0, 1, 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 1, 0, 4, 1, 4, 2, 4, 3, 4, 4, 4, 5, 4, 6, 4, 7, 4, 0, 5, 1, 5, 2, 5, 3, 5, 4, 5, 5, 5, 6, 5, 7, 5 };
            m_coords[(int)PatternStyle.DashedDownwardDiagonal] = new int[] { 0, 2, 4, 2, 1, 3, 5, 3, 2, 4, 6, 4, 3, 5, 7, 5 };
            m_coords[(int)PatternStyle.DashedUpwardDiagonal] = new int[] { 3, 2, 7, 2, 2, 3, 6, 3, 1, 4, 5, 4, 0, 5, 4, 5 };
            m_coords[(int)PatternStyle.DashedHorizontal] = new int[] { 0, 0, 1, 0, 2, 0, 3, 0, 4, 5, 5, 5, 6, 5, 7, 5 };
            m_coords[(int)PatternStyle.DashedVertical] = new int[] { 0, 0, 0, 1, 0, 2, 0, 3, 0, 4, 4, 5, 4, 6, 4, 7 };
            m_coords[(int)PatternStyle.SmallConfetti] = new int[] { 0, 0, 4, 1, 1, 2, 6, 3, 3, 4, 7, 5, 2, 6, 5, 7 };
            m_coords[(int)PatternStyle.LargeConfetti] = new int[] { 0, 0, 2, 0, 3, 0, 7, 0, 2, 1, 3, 1, 6, 2, 7, 2, 3, 3, 4, 3, 6, 3, 7, 3, 0, 4, 1, 4, 3, 4, 4, 4, 0, 5, 1, 5, 4, 6, 5, 6, 0, 7, 4, 7, 5, 7, 7, 7 };
            m_coords[(int)PatternStyle.Zigzag] = new int[] { 0, 0, 7, 0, 1, 1, 6, 1, 2, 2, 5, 2, 3, 3, 4, 3, 0, 4, 7, 4, 1, 5, 6, 5, 2, 6, 5, 6, 3, 7, 4, 7 };
            m_coords[(int)PatternStyle.Wave] = new int[] { 4, 1, 4, 1, 2, 2, 5, 2, 7, 2, 0, 3, 1, 3, 4, 5, 4, 5, 2, 6, 5, 6, 7, 6, 0, 7, 1, 7 };
            m_coords[(int)PatternStyle.DiagonalBrick] = new int[] { 7, 0, 6, 1, 5, 2, 4, 3, 3, 4, 4, 4, 2, 5, 5, 5, 1, 6, 6, 6, 0, 7, 7, 7 };
            m_coords[(int)PatternStyle.HorizontalBrick] = new int[] { 0, 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7, 0, 0, 1, 0, 2, 0, 3, 0, 4, 1, 4, 2, 4, 3, 4, 4, 4, 5, 4, 6, 4, 7, 4, 4, 5, 4, 6, 4, 7 };
            m_coords[(int)PatternStyle.Weave] = new int[] { 0, 0, 4, 0, 1, 1, 3, 1, 5, 1, 2, 2, 6, 2, 1, 3, 5, 3, 7, 3, 0, 4, 4, 4, 3, 4, 5, 5, 2, 6, 6, 6, 1, 7, 3, 7, 7, 7 };
            m_coords[(int)PatternStyle.Plaid] = new int[] { 0, 0, 2, 0, 4, 0, 6, 0, 1, 1, 3, 1, 5, 1, 7, 1, 0, 2, 2, 2, 4, 2, 6, 2, 1, 3, 3, 3, 5, 3, 7, 3, 0, 4, 1, 4, 2, 4, 3, 4, 0, 5, 1, 5, 2, 5, 3, 5, 0, 6, 1, 6, 2, 6, 3, 6, 0, 7, 1, 7, 2, 7, 3, 7 };
            m_coords[(int)PatternStyle.Divot] = new int[] { 3, 1, 4, 2, 3, 3, 0, 5, 7, 6, 0, 7 };
            m_coords[(int)PatternStyle.DottedGrid] = new int[] { 0, 0, 2, 0, 4, 0, 6, 0, 0, 2, 0, 4, 0, 6 };
            m_coords[(int)PatternStyle.DottedDiamond] = new int[] { 0, 0, 2, 2, 6, 2, 4, 4, 2, 6, 6, 6 };
            m_coords[(int)PatternStyle.Shingle] = new int[] { 6, 0, 7, 0, 0, 1, 5, 1, 1, 2, 4, 2, 2, 3, 3, 3, 4, 4, 5, 4, 6, 5, 7, 6, 7, 7 };
            m_coords[(int)PatternStyle.Trellis] = new int[] { 0, 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7, 0, 1, 1, 2, 1, 5, 1, 6, 1, 0, 2, 1, 2, 2, 2, 3, 2, 4, 2, 5, 2, 6, 2, 7, 2, 0, 3, 3, 3, 4, 3, 7, 3, 0, 4, 1, 4, 2, 4, 3, 4, 4, 4, 5, 4, 6, 4, 7, 4, 1, 5, 2, 5, 5, 5, 6, 5, 0, 6, 1, 6, 2, 6, 3, 6, 4, 6, 5, 6, 6, 6, 7, 6, 0, 7, 3, 7, 4, 7, 7, 7 };
            m_coords[(int)PatternStyle.Sphere] = new int[] { 1, 0, 2, 0, 3, 0, 5, 0, 6, 0, 7, 0, 0, 1, 4, 1, 7, 1, 0, 2, 4, 2, 5, 2, 6, 2, 7, 2, 0, 3, 4, 3, 5, 3, 6, 3, 7, 3, 1, 4, 2, 4, 3, 4, 5, 4, 6, 4, 7, 4, 0, 5, 3, 5, 4, 5, 0, 6, 1, 6, 2, 6, 3, 6, 4, 6, 0, 7, 1, 7, 2, 7, 3, 7, 4, 7 };
            m_coords[(int)PatternStyle.SmallGrid] = new int[] { 0, 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7, 0, 0, 1, 4, 1, 0, 2, 4, 2, 0, 3, 4, 3, 0, 4, 1, 4, 2, 4, 3, 4, 4, 4, 5, 4, 6, 4, 7, 4, 0, 5, 4, 5, 0, 6, 4, 6, 0, 7, 4, 7 };
            m_coords[(int)PatternStyle.LargeGrid] = new int[] { 0, 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7, 0, 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 6, 0, 7 };
            m_coords[(int)PatternStyle.SmallCheckerBoard] = new int[] { 0, 0, 3, 0, 4, 0, 7, 0, 1, 1, 2, 1, 5, 1, 6, 1, 1, 2, 2, 2, 5, 2, 6, 2, 0, 3, 3, 3, 4, 3, 7, 3, 0, 4, 3, 4, 4, 4, 7, 4, 1, 5, 2, 5, 5, 5, 6, 5, 1, 6, 2, 6, 5, 6, 6, 6, 0, 7, 3, 7, 4, 7, 7, 7 };
            m_coords[(int)PatternStyle.LargeCheckerBoard] = new int[] { 0, 0, 1, 0, 2, 0, 3, 0, 0, 1, 1, 1, 2, 1, 3, 1, 0, 2, 1, 2, 2, 2, 3, 2, 0, 3, 1, 3, 2, 3, 3, 3, 4, 4, 5, 4, 6, 4, 7, 4, 4, 5, 5, 5, 6, 5, 7, 5, 4, 6, 5, 6, 6, 6, 7, 6, 4, 7, 5, 7, 6, 7, 7, 7 };
            m_coords[(int)PatternStyle.OutlinedDiamond] = new int[] { 0, 0, 6, 0, 1, 1, 5, 1, 2, 2, 4, 2, 3, 3, 3, 4, 5, 4, 2, 5, 5, 5, 0, 6, 6, 6, 7, 7 };
            m_coords[(int)PatternStyle.SolidDiamond] = new int[] { 3, 0, 2, 1, 3, 1, 4, 1, 1, 2, 3, 2, 4, 2, 5, 2, 0, 3, 1, 3, 2, 3, 3, 3, 4, 3, 5, 3, 6, 3, 1, 4, 2, 4, 3, 4, 4, 4, 5, 4, 2, 5, 3, 5, 4, 5, 3, 6 };
        }
        
        public static string GetPatternImage(PatternStyle pattern, Color foreColor, Color backgroundColor)
        {
            string result = "";

            using (MemoryStream ms = new MemoryStream())
            using (Bitmap bitmap = new Bitmap(8, 8))
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.Clear(backgroundColor);

                var patternCoords = m_coords[(int)pattern];
                for (int i = 0; i < patternCoords.Length; i += 2)
                    bitmap.SetPixel(patternCoords[i], patternCoords[i + 1], foreColor);

                
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;
                result = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
            }

            return result;
        }

        private static int[][] m_coords;
    }
}