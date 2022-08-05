// Copyright (c) 2001-2020 Aspose Pty Ltd. All Rights Reserved.

using Aspose.Slides.Export.Web;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Aspose.Slides.WebExtensions.Helpers
{
    public static class TextHelper
    {
        public static string GetTextFrameColumnsStyle(ITextFrameFormatEffectiveData format)
        {
            string result = "";
            if (format.ColumnCount > 1)
            {
                var spacing = format.ColumnSpacing;
                result = string.Format("column-count: {0}; column-gap: {1}px;", format.ColumnCount, double.IsNaN(spacing) ? 0 : spacing);
            }

            return result;
        }

        public static string GetVerticalAlignmentStyle(TextAnchorType textAnchorType)
        {
            switch (textAnchorType)
            {
                case TextAnchorType.Top:
                    return "vertical-align: top;";
                case TextAnchorType.Bottom:
                    return "vertical-align: bottom;";
                default:
                    return "vertical-align: center;";
            }
        }

        public static string GetVerticalAlignmentStyle(TextFrame textFrame, AutoShape parentShape)
        {
            // workaround with padding for div/p case

            string paddingTop = "";
            var textFrameFormatEffective = textFrame.TextFrameFormat.GetEffective();

            float paraHeights = 0;
            foreach (var para in textFrame.Paragraphs)
                paraHeights += para.GetRect().Height;

            switch (textFrame.TextFrameFormat.GetEffective().AnchoringType)
            {
                case TextAnchorType.Bottom:
                    paddingTop = string.Format("padding-top: {0}px;", parentShape.Height - paraHeights - textFrameFormatEffective.MarginTop - textFrameFormatEffective.MarginBottom);
                    break;
                case TextAnchorType.Center:
                    paddingTop = string.Format("padding-top: {0}px;", (parentShape.Height - paraHeights - textFrameFormatEffective.MarginTop - textFrameFormatEffective.MarginBottom) / 2);
                    break;
            }

            return paddingTop;
        }

        public static string GetTextPositioningStyle(TextFrame textFrame, AutoShape parentShape)
        {
            var textFrameFormatEffective = textFrame.TextFrameFormat.GetEffective();

            float paraHeights = 0;
            float maxParaWidth = float.MinValue;
            foreach (var para in textFrame.Paragraphs)
            {
                var paraRect = para.GetRect();
                paraHeights += paraRect.Height;
                if (paraRect.Width > maxParaWidth)
                    maxParaWidth = paraRect.Width;
            }

            bool verticalText = textFrameFormatEffective.TextVerticalType == TextVerticalType.Vertical
                                || textFrameFormatEffective.TextVerticalType == TextVerticalType.WordArtVertical
                                || textFrameFormatEffective.TextVerticalType == TextVerticalType.Vertical270;

            double paddingTop = 0;
            double paddingLeft = 0;
            string writingModeStyle = "";

            if (!verticalText)
            {
                switch (textFrameFormatEffective.AnchoringType)
                {
                    case TextAnchorType.Bottom:
                        paddingTop = parentShape.Height - paraHeights - textFrameFormatEffective.MarginTop - textFrameFormatEffective.MarginBottom;
                        break;
                    case TextAnchorType.Center:
                        paddingTop = (parentShape.Height - paraHeights - textFrameFormatEffective.MarginTop - textFrameFormatEffective.MarginBottom) / 2;
                        break;
                }
                if ((int)parentShape.Rotation != 0)
                {
                    writingModeStyle += String.Format("transform: rotate({0}deg);", (int)parentShape.Rotation);
                }
            }
            else
            {
                if (textFrameFormatEffective.TextVerticalType == TextVerticalType.WordArtVertical)
                    writingModeStyle = "width: 1px; word-wrap: break-word; white-space: pre-wrap;";
                else
                    writingModeStyle = "writing-mode: vertical-rl;";

                if ((textFrameFormatEffective.AnchoringType == TextAnchorType.Top && (textFrameFormatEffective.TextVerticalType == TextVerticalType.Vertical || textFrameFormatEffective.TextVerticalType == TextVerticalType.WordArtVertical))
                    || (textFrameFormatEffective.AnchoringType == TextAnchorType.Bottom && textFrameFormatEffective.TextVerticalType == TextVerticalType.Vertical270))
                    paddingLeft = parentShape.Width - paraHeights - textFrameFormatEffective.MarginLeft - textFrameFormatEffective.MarginRight;

                if (textFrameFormatEffective.AnchoringType == TextAnchorType.Center)
                    if (textFrameFormatEffective.TextVerticalType == TextVerticalType.Horizontal || textFrame.Paragraphs.Count == 1)
                        paddingLeft = (parentShape.Width - paraHeights - textFrameFormatEffective.MarginLeft - textFrameFormatEffective.MarginRight) / 2;
                    else
                        paddingLeft = parentShape.Width - paraHeights - textFrameFormatEffective.MarginLeft - textFrameFormatEffective.MarginRight;

                if (textFrameFormatEffective.TextVerticalType == TextVerticalType.Vertical270)
                    writingModeStyle += "transform: rotate(180deg);";
            }

            string paddingTopStyle = string.Format("padding-top: {0}px;", (int)paddingTop);
            string paddingLeftStyle = string.Format("padding-left: {0}px;", (int)paddingLeft);

            return string.Join(" ", paddingTopStyle, paddingLeftStyle, writingModeStyle);
        }

        public static string GetHorizontalAlignmentStyle(TextAlignment textAlignment)
        {
            switch (textAlignment)
            {
                case TextAlignment.Center:
                case TextAlignment.Distributed:
                case TextAlignment.Justify:
                case TextAlignment.JustifyLow:
                    return "text-align: center;";
                case TextAlignment.Right:
                    return "text-align: right;";
                default:
                    return "text-align: left;";
            }
        }

        public static string GetLineSpacingStyle(IParagraphFormatEffectiveData format, float fontHeight)
        {
            string result = "";
            if (format.SpaceWithin < 0)
                result = string.Format("line-height: {0}px;", -(int)format.SpaceWithin);
            else
                result = string.Format("line-height: {0}px;", (int)((8f/7f)*(fontHeight) * format.SpaceWithin / 100f));

            return result;
        }

        public static string GetTextStyle<T>(IPortionFormatEffectiveData format, ITextFrameFormatEffectiveData textFrameFormat, bool isTableContent, TemplateContext<T> model)
        {
            float fontHeight = format.FontHeight;

            string fontFillStyle = FillHelper.GetFillStyle(format.FillFormat, model);
            if (fontFillStyle.StartsWith("background-color: "))
            {
                // fix for solid fill
                fontFillStyle = fontFillStyle.Replace("background-color: ", "color: ");
            }
            else
            {
                // additional css for non-solid fills
                fontFillStyle += " -webkit-text-fill-color: transparent; -webkit-background-clip: text;";
            }

            string escapedTextStyle = "";
            if (format.Escapement != 0)
            {
                escapedTextStyle = string.Format("position: relative; top: {0}px;", -fontHeight * format.Escapement / 100);
                fontHeight *= 0.67f;
            }

            string textDecorationStyle = "";
            if (format.StrikethroughType != TextStrikethroughType.None)
            {
                string underlineStyle = "";
                switch (format.StrikethroughType)
                {
                    case TextStrikethroughType.Double:
                        underlineStyle = "double";
                        break;
                    default:
                        underlineStyle = "solid";
                        break;
                }

                textDecorationStyle = string.Format("text-decoration: line-through {0};", underlineStyle);
            }
            
            if (format.FontUnderline != TextUnderlineType.None)
            {
                string underlineStyle = "";
                switch (format.FontUnderline)
                {
                    case TextUnderlineType.Dotted:
                    case TextUnderlineType.HeavyDotted:
                        underlineStyle = "dotted";
                        break;
                    case TextUnderlineType.Dashed:
                    case TextUnderlineType.DotDash:
                    case TextUnderlineType.DotDotDash:
                    case TextUnderlineType.LongDashed:
                    case TextUnderlineType.HeavyDashed:
                    case TextUnderlineType.HeavyDotDash:
                    case TextUnderlineType.HeavyDotDotDash:
                    case TextUnderlineType.HeavyLongDashed:
                        underlineStyle = "dashed";
                        break;
                    case TextUnderlineType.Wavy:
                    case TextUnderlineType.DoubleWavy:
                    case TextUnderlineType.HeavyWavy:
                        underlineStyle = "wavy";
                        break;
                    case TextUnderlineType.Double:
                        underlineStyle = "double";
                        break;
                    default:
                        underlineStyle = "solid";
                        break;
                }
                
                textDecorationStyle = string.Format("text-decoration: underline {0} {1};", underlineStyle, ColorHelper.GetRrbaColorString(format.UnderlineFillFormat.SolidFillColor));
            }

            var shadowFix = (textFrameFormat.TextVerticalType == TextVerticalType.Vertical || textFrameFormat.TextVerticalType == TextVerticalType.Vertical270 ? -1 : 1)
                                    *(isTableContent ? 0.5f : 1);

            string outerShadowStyle = "";
            if (format.EffectFormat.OuterShadowEffect != null)
                outerShadowStyle = string.Format("text-shadow: {0}px {1}px {2}px {3};",
                                                    shadowFix * format.EffectFormat.OuterShadowEffect.Distance * Math.Cos((Math.PI / 180) * format.EffectFormat.OuterShadowEffect.Direction),
                                                    shadowFix * format.EffectFormat.OuterShadowEffect.Distance * Math.Sin((Math.PI / 180) * format.EffectFormat.OuterShadowEffect.Direction),
                                                    format.EffectFormat.OuterShadowEffect.BlurRadius,
                                                    ColorHelper.GetRrbaColorString(format.EffectFormat.OuterShadowEffect.ShadowColor));

            string strokeStyle = "";
            if (format.LineFormat.FillFormat.FillType == FillType.Solid)
                strokeStyle = string.Format("-webkit-text-stroke: {0}px {1};", format.LineFormat.Width, ColorHelper.GetRrbaColorString(format.LineFormat.FillFormat.SolidFillColor));
            else if (format.LineFormat.FillFormat.FillType == FillType.Gradient)
                strokeStyle = "";

            string spacingStyle = "";
            if (format.Spacing != 0)
                spacingStyle = string.Format("letter-spacing: {0}px;", format.Spacing);

            string fontBoldItalicStyle = GetTextFontItalicStyle(format);
            string fontFamilyStyle = string.Format("font-family: {0};", format.LatinFont);
            string fontHeightStyle = string.Format("font-size: {0}px;", fontHeight);
            string fontCapStyle = "";
            if (format.TextCapType == TextCapType.All)
                fontCapStyle = "text-transform: uppercase;";
            else if (format.TextCapType == TextCapType.Small)
                fontCapStyle = "font-variant: small-caps;";

            return string.Join(" ",
                            fontBoldItalicStyle,
                            fontFamilyStyle,
                            fontHeightStyle,
                            fontFillStyle,
                            escapedTextStyle,
                            textDecorationStyle,
                            outerShadowStyle,
                            strokeStyle,
                            spacingStyle,
                            fontCapStyle);

        }

        public static string GetTextStyle<T>(IParagraphFormatEffectiveData format, TemplateContext<T> model)
        {
            string alignment = TextHelper.GetHorizontalAlignmentStyle(format.Alignment);
            float fontHeight = (model.Object as Paragraph).Portions[0].PortionFormat.GetEffective().FontHeight;
            string lineSpacingStyle = TextHelper.GetLineSpacingStyle(format, fontHeight);

            return string.Join(" ", alignment, lineSpacingStyle);
        }

        public static string GetTextBulletStyle<T>(IParagraphFormatEffectiveData format, ITextFrameFormatEffectiveData textFrameFormat, bool isTableContent, TemplateContext<T> model)
        {
            var firstPortionFormatEffective = (model.Object as Paragraph).Portions[0].PortionFormat.GetEffective();

            string fontBoldItalicStyle = GetTextFontItalicStyle(firstPortionFormatEffective);

            string fontFill = "";
            switch (format.Bullet.Type)
            {
                case BulletType.Symbol:
                case BulletType.Numbered:
                    if (format.Bullet.IsBulletHardColor)
                    {
                        fontFill = string.Format("color: {0};", ColorHelper.GetRrbaColorString(format.Bullet.FillFormat.SolidFillColor));
                    }
                    else
                    {
                        fontFill = FillHelper.GetFillStyle(firstPortionFormatEffective.FillFormat, model);
                        if (fontFill.StartsWith("background-color: "))
                        {
                            // fix for solid fill
                            fontFill = fontFill.Replace("background-color: ", "color: ");
                        }
                        else
                        {
                            // additional css for non-solid fills
                            fontFill += " -webkit-text-fill-color: transparent; -webkit-background-clip: text;";
                        }
                    }
                    break;
                case BulletType.Picture:
                    // picture bullet
                    break;
            }

            IFontData bulletFont = firstPortionFormatEffective.LatinFont;
            if (format.Bullet.IsBulletHardFont && format.Bullet.Type != BulletType.Numbered)
                bulletFont = format.Bullet.Font;

            var shadowFix = (textFrameFormat.TextVerticalType == TextVerticalType.Vertical || textFrameFormat.TextVerticalType == TextVerticalType.Vertical270 ? -1 : 1) 
                             * (isTableContent ? 0.5f : 1);

            string outerShadowStyle = "";
            if (firstPortionFormatEffective.EffectFormat.OuterShadowEffect != null)
                outerShadowStyle = string.Format("text-shadow: {0}px {1}px {2}px {3};",
                                                    shadowFix * firstPortionFormatEffective.EffectFormat.OuterShadowEffect.Distance * Math.Cos((Math.PI / 180) * firstPortionFormatEffective.EffectFormat.OuterShadowEffect.Direction),
                                                    shadowFix * firstPortionFormatEffective.EffectFormat.OuterShadowEffect.Distance * Math.Sin((Math.PI / 180) * firstPortionFormatEffective.EffectFormat.OuterShadowEffect.Direction),
                                                    firstPortionFormatEffective.EffectFormat.OuterShadowEffect.BlurRadius,
                                                    ColorHelper.GetRrbaColorString(firstPortionFormatEffective.EffectFormat.OuterShadowEffect.ShadowColor));

            string fontHeightStyle = string.Format("font-size: {0}px;", format.Bullet.Height * firstPortionFormatEffective.FontHeight / 100);
            string fontFamilyStyle = string.Format("font-family: {0};", bulletFont);

            return string.Join(" ", fontBoldItalicStyle, fontFill, outerShadowStyle, fontHeightStyle, fontFamilyStyle);
        }

        public static string GetPictureBulletStyle<T>(IParagraphFormatEffectiveData format, TemplateContext<T> model)
        {
            return string.Format("width: {0}px; height: {0}px;", GetPictureBulletSize(format, model));
        }

        public static float GetPictureBulletSize<T>(IParagraphFormatEffectiveData format, TemplateContext<T> model)
        {
            var firstPortionFormatEffective = (model.Object as Paragraph).Portions[0].PortionFormat.GetEffective();
            return format.Bullet.Height * firstPortionFormatEffective.FontHeight / 100 * 0.75f;
        }

        public static double[] GetParagraphMargins<T>(IParagraphFormatEffectiveData format, TextFrame parentTextFrame, SizeF parentContainerSize, TemplateContext<T> model)
        {
            Paragraph contextObject = model.Object as Paragraph;
            var textFrameFormat = parentTextFrame.TextFrameFormat.GetEffective();

            var firstPortionFormatEffective = (model.Object as Paragraph).Portions[0].PortionFormat.GetEffective();

            // left, top, right, bottom
            double[] margins = new double[] { format.MarginLeft + textFrameFormat.MarginLeft,
                                              0,
                                              format.MarginRight + textFrameFormat.MarginRight,
                                              0 };

            // margins adjustment for vertical text (...)
            switch (format.Alignment)
            {
                case TextAlignment.Center:
                case TextAlignment.Distributed:
                case TextAlignment.Justify:
                case TextAlignment.JustifyLow:
                    if (textFrameFormat.TextVerticalType == TextVerticalType.Vertical)
                        margins[1] += (parentContainerSize.Height - contextObject.GetRect().Width) / 2;
                    else if (textFrameFormat.TextVerticalType == TextVerticalType.WordArtVertical)
                        margins[1] = -((parentContainerSize.Height - contextObject.GetRect().Width) / 2);
                    else if (textFrameFormat.TextVerticalType == TextVerticalType.Vertical270)
                        margins[3] += (parentContainerSize.Height - contextObject.GetRect().Width) / 2;
                    break;
                case TextAlignment.Right:
                    if (textFrameFormat.TextVerticalType == TextVerticalType.Vertical)
                        margins[1] += parentContainerSize.Height - contextObject.GetRect().Width;
                    break;
                case TextAlignment.Left:
                    if (textFrameFormat.TextVerticalType == TextVerticalType.Vertical270)
                        margins[3] = parentContainerSize.Height - contextObject.GetRect().Width;
                    else if (textFrameFormat.TextVerticalType == TextVerticalType.WordArtVertical)
                        margins[1] = -(parentContainerSize.Height - contextObject.GetRect().Width);
                    break;
            }

            switch (textFrameFormat.AnchoringType)
            {
                case TextAnchorType.Center:
                case TextAnchorType.Distributed:
                case TextAnchorType.Justified:
                    if (textFrameFormat.TextVerticalType == TextVerticalType.Vertical270)
                        margins[2] = (parentContainerSize.Width - contextObject.GetRect().Height) / 2;
                    break;
                case TextAnchorType.Bottom:
                    if (textFrameFormat.TextVerticalType == TextVerticalType.Vertical270)
                        margins[2] = parentContainerSize.Width - contextObject.GetRect().Height;
                    break;
            }

            if ((textFrameFormat.TextVerticalType == TextVerticalType.Vertical || textFrameFormat.TextVerticalType == TextVerticalType.Vertical270) && parentTextFrame.Paragraphs.Count > 1)
            {
                margins[0] = 0;
                margins[2] = 0;
            }

            if (textFrameFormat.TextVerticalType == TextVerticalType.Horizontal)
            {
                double lineSpacingAdjustment = 0;
                if (firstPortionFormatEffective.FontHeight > 25)
                    lineSpacingAdjustment = -firstPortionFormatEffective.FontHeight / 10;

                margins[1] = lineSpacingAdjustment;
                margins[3] = lineSpacingAdjustment;
            }

            
            return margins;
        }

        private static string GetTextFontItalicStyle(IPortionFormatEffectiveData format)
        {
            string fontWeight = format.FontBold ? "font-weight: bold;" : "";
            string fontStyle = format.FontItalic ? "font-style: italic;" : "";

            return string.Join(" ", fontWeight, fontStyle);
        }

        private static bool HasParagraphDrawnBullet(Paragraph paragraph)
        {
            return paragraph.ParagraphFormat.GetEffective().Bullet.Type > BulletType.None && (paragraph.Text.Length > 0 || paragraph.Text.Contains("\\n"));
        }
    }
}