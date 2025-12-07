// Copyright (c) 2001-2020 Aspose Pty Ltd. All Rights Reserved.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Slides.Export.Web;

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
                Shape asShape = shape as Shape;
                using (MemoryStream ms = new MemoryStream())
                using (IImage image = GetShapeThumbnail(asShape))
                {
                    if (image == null) return "none";
                    image.Save(ms, ImageFormat.Png);
                    return "url('data:image/png;base64, " + Convert.ToBase64String(ms.ToArray()) + "')";
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
                    if (shape is OleObjectFrame && (shape as OleObjectFrame).SubstitutePictureFormat != null && (shape as OleObjectFrame).SubstitutePictureFormat.Picture != null)
                    {
                        imgSrcPath = model.Output.GetResourcePath((shape as OleObjectFrame).SubstitutePictureFormat.Picture.Image);
                    }
                    else
                    {
                        throw;
                    }
                }

                string result = ShapeHelper.ConvertPathToRelative(imgSrcPath, slidesPath);
                return "url(" + result + ");";
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
                foreach(var item in slide.Shapes) if (item is T) result.Add((T)item);
            }
            
            foreach (var slide in pres.LayoutSlides)
            {
                foreach (var item in slide.Shapes) if (item is T) result.Add((T)item);
            }
            
            foreach (var slide in pres.Masters)
            {
                foreach (var item in slide.Shapes) if (item is T) result.Add((T)item);
            }

            return result;
        }

        private static IImage GetShapeThumbnail(IShape shape)
        {
            AutoShape autoShape = shape as AutoShape;

            IImage thumbnail;
            if (autoShape != null && autoShape.TextFrame != null && !string.IsNullOrEmpty(autoShape.TextFrame.Text))
            {
                // Copy shape paragraphs -> remove text -> get shape image -> restore paragraphs. Export text as HTML markup in the template.
                List<Paragraph> paraColl = new List<Paragraph>();
                foreach (Paragraph para in autoShape.TextFrame.Paragraphs)
                    paraColl.Add(new Paragraph(para));

                try
                {
                    autoShape.TextFrame.Paragraphs.Clear();
                    thumbnail = autoShape.GetImage();
                }
                finally
                {
                    foreach (Paragraph para in paraColl)
                        autoShape.TextFrame.Paragraphs.Add(para);
                }
            }
            else if (shape is IConnector)
            {
                thumbnail = shape.GetImage(ShapeThumbnailBounds.Appearance, 1, 1);
            }
            else
            {
                thumbnail = shape.GetImage();
            }

            return thumbnail;
        }

        public static string GetSubstitutionMarkup(string templateMarkup, IShape shape, Point origin, string animationAttributes)
        {
            return null;
        }
        public static string GetPositionStyle(Shape shape, Point origin)
        {
            int left = (int)shape.X;
            int top = (int)shape.Y;
            int width = (int)shape.Width;
            int height = (int)shape.Height;

            if (shape is Connector && height == 0)
            {
                Connector cShape = (Connector)shape;
                if (!double.IsNaN(cShape.LineFormat.Width))
                {
                    height = (int)(shape as Connector).LineFormat.Width;
                }
            }
            return string.Format("left: {0}px; top: {1}px; width: {2}px; height: {3}px;", left, top, width, height);
        }
    }
}
