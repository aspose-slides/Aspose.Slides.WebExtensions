﻿// Copyright (c) 2001-2020 Aspose Pty Ltd. All Rights Reserved.

using Aspose.Slides.Export.Web;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Slides.Util;
using System.Collections.Generic;

namespace Aspose.Slides.WebExtensions.Helpers
{
    public static class ImageHelper
    {
        public static string GetImageURL<T>(IPPImage image, TemplateContext<T> model)
        {
            string result = "";
            if (!model.Global.Get<bool>("embedImages"))
            {
                var imgSrcPath = model.Output.GetResourcePath(image);
                var slidesPath = model.Global.Get<string>("slidesPath");

                result = ShapeHelper.ConvertPathToRelative(imgSrcPath, slidesPath);
            }
            else
            {
                byte[] dataSource = image.BinaryData;
                if (image.ContentType == "image/x-emf" || image.ContentType == "image/x-wmf")
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Image.Save(ms, ImageFormat.Png);
                        ms.Flush();
                        dataSource = ms.ToArray();
                    }
                }
                else if (image.ContentType == "image/tiff")
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Image.Save(ms, ImageFormat.Png);
                        ms.Flush();
                        dataSource = ms.ToArray();
                    }
                }

                result = "data:image/png;base64, " + Convert.ToBase64String(dataSource);
            }

            return result;
        }
        public static string Crop<T>(IPPImage image, TemplateContext<T> model, float cropLeft, float cropTop, float cropRight, float cropBottom)
        {
            if (cropLeft != 0f || cropTop != 0f || cropRight != 0f || cropBottom != 0f)
            {
                Image originImage = Image.FromStream(new MemoryStream(image.BinaryData));

                Rectangle srcRect = new Rectangle(
                            (int)(originImage.Width * cropLeft / 100),
                            (int)(originImage.Height * cropTop / 100),
                            (int)(originImage.Width * (100 - cropLeft - cropRight) / 100),
                            (int)(originImage.Height * (100 - cropTop - cropBottom) / 100));
                Rectangle destRect = new Rectangle(0, 0, srcRect.Width, srcRect.Height);

                Bitmap resultBmp = new Bitmap(destRect.Width, destRect.Height);
                using (Graphics g = Graphics.FromImage(resultBmp))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.DrawImage(originImage, destRect, srcRect, GraphicsUnit.Pixel);
                    g.Flush();
                }

                var slidesPath = model.Global.Get<string>("slidesPath");
                string convertedFileName = GetImageURL<T>(image, model).Replace(".png", string.Format("cr{0}.png", (model.Object as PictureFrame).UniqueId));
                string convertedFilePath = Path.Combine(slidesPath, convertedFileName);
                string imagesPath = Path.GetDirectoryName(convertedFilePath);
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                resultBmp.Save(convertedFilePath, System.Drawing.Imaging.ImageFormat.Png);
                return convertedFileName;
            }
            return GetImageURL<T>(image, model);
        }
        public static string CropEmbed(string imgSrc, float cropLeft, float cropTop, float cropRight, float cropBottom)
        {
            const string b64prefix = "data:image/png;base64, ";
            if (cropLeft != 0f || cropTop != 0f || cropRight != 0f || cropBottom != 0f)
            {
                if (imgSrc.StartsWith(b64prefix))
                {
                    byte[] imgData = Convert.FromBase64String(imgSrc.Substring(b64prefix.Length));
                    Image originImage = Image.FromStream(new MemoryStream(imgData));

                    Rectangle srcRect = new Rectangle(
                                (int)(originImage.Width * cropLeft / 100),
                                (int)(originImage.Height * cropTop / 100),
                                (int)(originImage.Width * (100 - cropLeft - cropRight) / 100),
                                (int)(originImage.Height * (100 - cropTop - cropBottom) / 100));
                    Rectangle destRect = new Rectangle(0, 0, srcRect.Width, srcRect.Height);

                    Bitmap resultBmp = new Bitmap(destRect.Width, destRect.Height);
                    using (Graphics g = Graphics.FromImage(resultBmp))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.DrawImage(originImage, destRect, srcRect, GraphicsUnit.Pixel);
                        g.Flush();
                    }
                    MemoryStream resultStream = new MemoryStream();
                    resultBmp.Save(resultStream, System.Drawing.Imaging.ImageFormat.Png);
                    return String.Format("{1}{0}", Convert.ToBase64String(resultStream.ToArray()), b64prefix);
                }
                throw new NotImplementedException();
            }
            return imgSrc;
        }
        public static void ConvertTiffToPng<T>(IPPImage image, TemplateContext<T> model)
        {
            if (image.ContentType == "image/tiff")
            {
                var slidesPath = model.Global.Get<string>("slidesPath");
                string convertedFileName = GetImageURL<T>(image, model) + ".png";
                string convertedFilePath = Path.Combine(slidesPath, convertedFileName);
                string imagesPath = Path.GetDirectoryName(convertedFilePath);
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }
                using (MemoryStream tiffData = new MemoryStream(image.BinaryData))
                {
                    using (Image initialImage = Image.FromStream(tiffData))
                    {
                        initialImage.Save(convertedFilePath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
        }
        public static IImage GetShapeFillImage(Shape shape, IFillFormatEffectiveData format)
        {
            AutoShape aShape = shape as AutoShape;
            if (aShape != null)
            {
                List<List<string>> store = new List<List<string>>();
                foreach (var par in aShape.TextFrame.Paragraphs)
                {
                    store.Add(new List<string>());
                    foreach (var por in par.Portions)
                    {
                        store[store.Count - 1].Add(por.Text);
                        por.Text = "";
                    }
                }

                IImage r = aShape.GetImage();

                for (int i = 0; i < store.Count; i++)
                {
                    for (int j = 0; j < store[i].Count; j++)
                    {
                        aShape.TextFrame.Paragraphs[i].Portions[j].Text = store[i][j];
                    }
                }

                return r;
            }
            return null;
        }
        
        public static string GetImagePositioningStyle(PictureFrame pictureFrame, Point origin)
        {
            var transform = "";
            if ((int)pictureFrame.Rotation != 0)
            {
                transform += string.Format(" rotate({0}deg)", (int)pictureFrame.Rotation);
            }
            if (pictureFrame.Frame.FlipH == NullableBool.True)
            {
                transform += " scaleX(-1)";
            }
            if (pictureFrame.Frame.FlipV == NullableBool.True)
            {
                transform += " scaleY(-1)";
            }
            if (!string.IsNullOrEmpty(transform)) transform = string.Format("transform:{0};", transform);

            var positionStyle = string.Format("left: {0}px; top: {1}px; width: {2}px; height: {3}px;{4}",
                (int)pictureFrame.X,// + origin.X,
                (int)pictureFrame.Y,// + origin.Y,
                (int)pictureFrame.Width,
                (int)pictureFrame.Height,
                transform);
            return positionStyle;
        }
        public static string GetImageShadowStyle(PictureFrame pictureFrame)
        {
            var outerShadow = pictureFrame.EffectFormat.OuterShadowEffect;
            if (outerShadow != null)
            {
                var outerShadowEffective = outerShadow.GetEffective();
                var xOffset = Math.Cos(2 * Math.PI * outerShadowEffective.Direction / 360) * outerShadowEffective.Distance;
                var yOffset = Math.Sin(2 * Math.PI * outerShadowEffective.Direction / 360) * outerShadowEffective.Distance;
                var blur = outerShadowEffective.BlurRadius;
                var spread = 0;
                var color = ColorHelper.GetRrbaColorString( outerShadowEffective.ShadowColor);
                return string.Format(
                    "box-shadow: {0}px {1}px {2}px {3}px {4};", 
                    NumberHelper.ToCssNumber(xOffset, 1, true), 
                    NumberHelper.ToCssNumber(yOffset, 1, true),  
                    NumberHelper.ToCssNumber(blur, 1, true),   
                    spread, 
                    color);
            }
            return "";
        }
        public static string CreateSvgFilter(PictureFrame pictureFrame, string id)
        {
            string svgFilter = "";
            foreach(var effect in pictureFrame.PictureFormat.Picture.ImageTransform)
            {
                if (effect.GetType() == typeof(Effects.ColorChange))
                {
                    Effects.ColorChange colorChange = (Effects.ColorChange)effect;
                    string funcR = "", funcG = "", funcB = "";
                    for (int i = 0; i< 256; i++)
                    {
                        funcR += (i == colorChange.FromColor.R) ? "1 " : "0 ";
                        funcG += (i == colorChange.FromColor.G) ? "1 " : "0 ";
                        funcB += (i == colorChange.FromColor.B) ? "1 " : "0 ";
                    }
                    funcR = funcR.Trim();
                    funcG = funcG.Trim();
                    funcB = funcB.Trim();
                    string floodColor = string.Format("#{0:X2}{1:X2}{2:X2}", colorChange.ToColor.R, colorChange.ToColor.G, colorChange.ToColor.B);

                    svgFilter = string.Format(
                        "<svg width=\"{3}\" height=\"{4}\" viewBox=\"0 0 {3} {4}\">" +
                            "<defs>" +
                                "<filter id=\"{5}\" color-interpolation-filters=\"sRGB\">" +
                                    "<feComponentTransfer>" +
                                        "<feFuncR type=\"discrete\" tableValues=\"{0}\"></feFuncR>" +
                                        "<feFuncG type=\"discrete\" tableValues=\"{1}\"></feFuncG>" +
                                        "<feFuncB type=\"discrete\" tableValues=\"{2}\"></feFuncB>" +
                                    "</feComponentTransfer>" +
                                    "<feColorMatrix type=\"matrix\" values=\"1 0 0 0 0 0 1 0 0 0 0 0 1 0 0 1 1 1 1 -3\" result=\"selectedColor\"></feColorMatrix>" +
                                    "<feComposite operator=\"out\" in=\"SourceGraphic\" result=\"notSelectedColor\"></feComposite>" +
                                    "<feFlood flood-color=\"{6}\" flood-opacity=\"{7}\"></feFlood>" +
                                    "<feComposite operator=\"in\" in2=\"selectedColor\"></feComposite>" +
                                    "<feComposite operator=\"over\" in2=\"notSelectedColor\"></feComposite>" +
                                "</filter>" +
                            "</defs>" +
                        "</svg>", funcR, funcG, funcB, (int)pictureFrame.Width, (int)pictureFrame.Height, id, floodColor,  ((float)colorChange.ToColor.Color.A/255f).ToString("0.0", System.Globalization.CultureInfo.InvariantCulture));
                }
            }
            return svgFilter;
        }
    }
}