// Copyright (c) 2001-2020 Aspose Pty Ltd. All Rights Reserved.

using Aspose.Slides.Export.Web;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Aspose.Slides.Util;

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
                result = "data:image/png;base64, " + Convert.ToBase64String(image.BinaryData);
            }

            return result;
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
                    using (Image initialImage = System.Drawing.Bitmap.FromStream(tiffData))
                    {
                        initialImage.Save(convertedFilePath, ImageFormat.Png);
                    }
                }
            }
        }
        public static Bitmap MetafileToBitmap(IPPImage image)
        {
            Metafile metafile = (Metafile)image.SystemImage;
            int h = metafile.Height;
            int w = metafile.Width;
            Bitmap bitmap = new Bitmap(w, h);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.None;
                g.DrawImage(metafile, 0, 0, w, h);
            }

            return bitmap;
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
                (int)pictureFrame.X + origin.X,
                (int)pictureFrame.Y + origin.Y,
                (int)pictureFrame.Width,
                (int)pictureFrame.Height,
                transform);
            return positionStyle;
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