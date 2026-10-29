// Copyright (c) 2001-2025 Aspose Pty Ltd. All Rights Reserved.
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
            toPath = NormalizePath(toPath);
            fromPath = NormalizePath(fromPath);

            bool toIsRooted = IsRootedPath(toPath);
            bool fromIsRooted = IsRootedPath(fromPath);

            if (!toIsRooted)
                toPath = "/" + toPath;

            if (!fromIsRooted)
                fromPath = "/" + fromPath;

            if (!fromPath.EndsWith("/"))
                fromPath += "/";

            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);

            return Uri.UnescapeDataString(relativeUri.ToString());
        }

        private static string NormalizePath(string path)
        {
            return path.Replace('\\', '/');
        }

        private static bool IsRootedPath(string path)
        {
            if (path.StartsWith("/"))
                return true;

            // Windows drive path: C:/...
            return path.Length >= 3 &&
                   Char.IsLetter(path[0]) &&
                   path[1] == ':' &&
                   path[2] == '/';
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
                    thumbnail = ShouldUseAppearanceBounds(autoShape)
                        ? autoShape.GetImage(ShapeThumbnailBounds.Appearance, 1, 1)
                        : autoShape.GetImage();
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

        public static string GetShapeImagePositionStyle(Shape shape)
        {
            Rectangle imageBounds = GetShapeAppearanceBounds(shape);
            if (imageBounds == Rectangle.Empty)
                imageBounds = Rectangle.Round(new RectangleF(shape.X, shape.Y, shape.Width, shape.Height));

            return string.Format(
                "position: absolute; z-index: 0; left: {0}px; top: {1}px; width: {2}px; height: {3}px;",
                (int)Math.Floor(imageBounds.Left - shape.X),
                (int)Math.Floor(imageBounds.Top - shape.Y),
                imageBounds.Width,
                imageBounds.Height);
        }

        public static bool ShouldUseAppearanceBounds(Shape shape)
        {
            AutoShape autoShape = shape as AutoShape;
            if (autoShape == null)
                return false;

            switch (autoShape.ShapeType)
            {
                case ShapeType.CalloutWedgeRectangle:
                case ShapeType.CalloutWedgeRoundRectangle:
                case ShapeType.CalloutWedgeEllipse:
                case ShapeType.CalloutCloud:
                    return true;
                default:
                    return false;
            }
        }

        public static RectangleF GetCalloutTextBounds(Shape shape)
        {
            AutoShape autoShape = shape as AutoShape;
            if (autoShape == null)
                return new RectangleF(0, 0, shape.Width, shape.Height);

            switch (autoShape.ShapeType)
            {
                case ShapeType.CalloutWedgeEllipse:
                    const float horizontalInsetRatio = 0.146f;
                    float horizontalInset = shape.Width * horizontalInsetRatio;
                    return new RectangleF(horizontalInset, 0, shape.Width - 2 * horizontalInset, shape.Height);
                default:
                    return new RectangleF(0, 0, shape.Width, shape.Height);
            }
        }

        private static Rectangle GetShapeAppearanceBounds(Shape shape)
        {
            AutoShape autoShape = shape as AutoShape;
            if (autoShape != null && autoShape.TextFrame != null && !string.IsNullOrEmpty(autoShape.TextFrame.Text))
            {
                List<Paragraph> paragraphs = new List<Paragraph>();
                foreach (Paragraph paragraph in autoShape.TextFrame.Paragraphs)
                    paragraphs.Add(new Paragraph(paragraph));

                try
                {
                    autoShape.TextFrame.Paragraphs.Clear();
                    return GetShapeAppearanceBoundsWithoutText(autoShape);
                }
                finally
                {
                    foreach (Paragraph paragraph in paragraphs)
                        autoShape.TextFrame.Paragraphs.Add(paragraph);
                }
            }

            return GetShapeAppearanceBoundsWithoutText(shape);
        }

        private static Rectangle GetShapeAppearanceBoundsWithoutText(Shape shape)
        {
            using (IImage image = shape.GetImage(ShapeThumbnailBounds.Slide, 1, 1))
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, Aspose.Slides.ImageFormat.Png);
                stream.Position = 0;

                using (Bitmap bitmap = new Bitmap(stream))
                {
                    int left = bitmap.Width;
                    int top = bitmap.Height;
                    int right = -1;
                    int bottom = -1;

                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            if (bitmap.GetPixel(x, y).A == 0)
                                continue;

                            left = Math.Min(left, x);
                            top = Math.Min(top, y);
                            right = Math.Max(right, x);
                            bottom = Math.Max(bottom, y);
                        }
                    }

                    return right < left || bottom < top
                        ? Rectangle.Empty
                        : Rectangle.FromLTRB(left, top, right + 1, bottom + 1);
                }
            }
        }

    }
}
