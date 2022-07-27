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
    }
}