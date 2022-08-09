// Copyright (c) 2001-2020 Aspose Pty Ltd. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Slides.Export.Web;
using System.Linq;

namespace Aspose.Slides.WebExtensions.Helpers
{
    public static class ShapeHelper
    {
        public static string GetShapeAsImageURL<T>(T shape, TemplateContext<T> model)
        {
            if (!(shape is Shape))
            {
                throw new InvalidOperationException("Object of Shape class expected");
            }

            if (model.Global.Get<bool>("embedImages"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Bitmap image = (shape as Shape).GetThumbnail();
                    image.Save(ms, ImageFormat.Png);
                    return "'data:image/png;base64, " + Convert.ToBase64String(ms.ToArray()) + "'";
                }
            }
            else
            {
                var imgSrcPath = "";
                var slidesPath = model.Global.Get<string>("slidesPath");

                try
                {
                    imgSrcPath = model.Output.GetResourcePath(shape as Shape);
                }
                catch (ArgumentException)
                {
                    if (shape is OleObjectFrame frame)
                    {
                        imgSrcPath = model.Output.GetResourcePath(frame.SubstitutePictureFormat?.Picture?.Image);
                    }
                    else
                    {
                        throw;
                    }
                }

                string result = ShapeHelper.ConvertPathToRelative(imgSrcPath, slidesPath);
                return result;
            }
        }

        public static string ConvertPathToRelative(string toPath, string fromPath)
        {
            // fixing paths with no root by adding fake root drive letter
            if (!Path.IsPathRooted(toPath))
                toPath = @"C:\" + toPath;
            if (!Path.IsPathRooted(fromPath))
                fromPath = @"C:\" + fromPath;
            if (!fromPath.EndsWith("\\"))
                fromPath += "\\";

            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string result = Uri.UnescapeDataString(relativeUri.ToString()).Replace('\\', '/');
            return result;
        }

        public static List<T> GetListOfShapes<T>(IPresentation pres)
        {
            List<T> result = new List<T>();

            foreach (var slide in pres.Slides)
            {
                result.AddRange(slide.Shapes.OfType<T>());
            }
            
            foreach (var slide in pres.LayoutSlides)
            {
                result.AddRange(slide.Shapes.OfType<T>());
            }
            
            foreach (var slide in pres.Masters)
            {
                result.AddRange(slide.Shapes.OfType<T>());
            }

            return result;
        }

        public static string GetSubstitutionMarkup(string templateMarkup, IShape shape, Point origin, string animationAttributes)
        {
            return null;
        }
        public static string GetPositionStyle(Shape shape, Point origin)
        {
            int left = (int)shape.X + origin.X;
            int top = (int)shape.Y + origin.Y;
            int width = (int)shape.Width;
            int height = (int)shape.Height;

            if (shape is Connector connector && height == 0)
            {
                height = (int)connector.LineFormat.Width;
            }
            return string.Format("left: {0}px; top: {1}px; width: {2}px; height: {3}px;", left, top, width, height);
        }
    }
}
