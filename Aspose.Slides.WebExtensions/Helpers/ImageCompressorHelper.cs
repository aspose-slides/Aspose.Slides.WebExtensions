// Copyright (c) 2001-2025 Aspose Pty Ltd. All Rights Reserved.

using Aspose.Slides.Export;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Aspose.Slides.WebExtensions.Helpers
{
    public static class ImageCompressorHelper
    {
        public static Dictionary<IPPImage, byte[]> CompressAll(
            IPresentation presentation,
            PicturesCompression picturesCompression)
        {
            if (picturesCompression == PicturesCompression.DocumentResolution)
                return new Dictionary<IPPImage, byte[]>();

            return CompressUsages(presentation, null, picturesCompression);
        }

        internal static byte[] Compress(
            IPresentation presentation,
            IPPImage image,
            PicturesCompression picturesCompression)
        {
            if (picturesCompression == PicturesCompression.DocumentResolution)
                return null;

            Dictionary<IPPImage, byte[]> result = CompressUsages(
                presentation, image, picturesCompression);
            byte[] compressedImage;
            return result.TryGetValue(image, out compressedImage)
                ? compressedImage
                : null;
        }

        private static Dictionary<IPPImage, byte[]> CompressUsages(
            IPresentation presentation,
            IPPImage targetImage,
            PicturesCompression picturesCompression)
        {
            Dictionary<IPPImage, byte[]> result = new Dictionary<IPPImage, byte[]>();
            Dictionary<IPPImage, long> areas = new Dictionary<IPPImage, long>();

            foreach (ISlide slide in presentation.Slides)
                CompressBaseSlideUsages(slide, presentation.SlideSize.Size.Width,
                    presentation.SlideSize.Size.Height, targetImage, picturesCompression, result, areas);

            foreach (ILayoutSlide layoutSlide in presentation.LayoutSlides)
                CompressBaseSlideUsages(layoutSlide, presentation.SlideSize.Size.Width,
                    presentation.SlideSize.Size.Height, targetImage, picturesCompression, result, areas);

            foreach (IMasterSlide masterSlide in presentation.Masters)
                CompressBaseSlideUsages(masterSlide, presentation.SlideSize.Size.Width,
                    presentation.SlideSize.Size.Height, targetImage, picturesCompression, result, areas);

            return result;
        }

        private static void UpdateResult(
            IPPImage sourceImage,
            byte[] compressedData,
            Dictionary<IPPImage, byte[]> result,
            Dictionary<IPPImage, long> areas)
        {
            if (compressedData == null)
                return;

            using (MemoryStream stream = new MemoryStream(compressedData))
            using (IImage image = Images.FromStream(stream))
            {
                long area = (long)image.Width * image.Height;
                long currentArea;
                if (!areas.TryGetValue(sourceImage, out currentArea) || area > currentArea)
                {
                    areas[sourceImage] = area;
                    result[sourceImage] = compressedData;
                }
            }
        }

        private static void CompressBaseSlideUsages(
            IBaseSlide slide,
            float slideWidth,
            float slideHeight,
            IPPImage targetImage,
            PicturesCompression picturesCompression,
            Dictionary<IPPImage, byte[]> result,
            Dictionary<IPPImage, long> areas)
        {
            CompressShapeImageUsages(slide.Shapes, targetImage, picturesCompression, result, areas);

            IFillFormat backgroundFill = slide.Background.FillFormat;
            if (backgroundFill.FillType == FillType.Picture && backgroundFill.PictureFillFormat.Picture != null)
            {
                IPPImage image = backgroundFill.PictureFillFormat.Picture.Image;
                if (image != null && (targetImage == null || targetImage == image))
                    UpdateResult(
                        image,
                        RenderCompressedImage(image, slideWidth, slideHeight, picturesCompression),
                        result,
                        areas);
            }
        }

        private static void CompressShapeImageUsages(
            IShapeCollection shapes,
            IPPImage targetImage,
            PicturesCompression picturesCompression,
            Dictionary<IPPImage, byte[]> result,
            Dictionary<IPPImage, long> areas)
        {
            foreach (IShape shape in shapes)
            {
                IPictureFrame pictureFrame = shape as IPictureFrame;
                if (pictureFrame != null && pictureFrame.PictureFormat.Picture != null)
                {
                    IPPImage image = pictureFrame.PictureFormat.Picture.Image;
                    CompressShapeImage(image, shape, targetImage, picturesCompression, result, areas);
                }

                if (shape.FillFormat.FillType == FillType.Picture && shape.FillFormat.PictureFillFormat.Picture != null)
                {
                    IPPImage image = shape.FillFormat.PictureFillFormat.Picture.Image;
                    CompressShapeImage(image, shape, targetImage, picturesCompression, result, areas);
                }

                IAutoShape autoShape = shape as IAutoShape;
                if (autoShape != null && autoShape.TextFrame != null)
                {
                    foreach (IParagraph paragraph in autoShape.TextFrame.Paragraphs)
                    {
                        IBulletFormatEffectiveData bullet = paragraph.ParagraphFormat.GetEffective().Bullet;
                        if (bullet.Type == BulletType.Picture &&
                            bullet.FillFormat.PictureFillFormat.Picture != null)
                        {
                            IPPImage image = bullet.FillFormat.PictureFillFormat.Picture.Image;
                            CompressShapeImage(image, shape, targetImage, picturesCompression, result, areas);
                        }
                    }
                }

                IGroupShape groupShape = shape as IGroupShape;
                if (groupShape != null)
                    CompressShapeImageUsages(groupShape.Shapes, targetImage, picturesCompression, result, areas);
            }
        }

        private static void CompressShapeImage(
            IPPImage image,
            IShape shape,
            IPPImage targetImage,
            PicturesCompression picturesCompression,
            Dictionary<IPPImage, byte[]> result,
            Dictionary<IPPImage, long> areas)
        {
            if (image == null || (targetImage != null && targetImage != image))
                return;

            UpdateResult(
                image,
                RenderCompressedImage(image, shape, picturesCompression),
                result,
                areas);
        }

        private static byte[] RenderCompressedImage(
            IPPImage image,
            IShape shape,
            PicturesCompression picturesCompression)
        {
            using (Presentation presentation = new Presentation())
            using (MemoryStream htmlStream = new MemoryStream())
            {
                presentation.Slides[0].Shapes.AddClone(shape);
                return RenderCompressedImage(
                    presentation, htmlStream, image, picturesCompression);
            }
        }

        private static byte[] RenderCompressedImage(
            IPPImage image,
            float width,
            float height,
            PicturesCompression picturesCompression)
        {
            using (Presentation presentation = new Presentation())
            using (MemoryStream htmlStream = new MemoryStream())
            {
                IPPImage backgroundImage = presentation.Images.AddImage(image.BinaryData);
                presentation.SlideSize.SetSize(width, height, SlideSizeScaleType.DoNotScale);
                ISlide slide = presentation.Slides[0];
                slide.Background.Type = BackgroundType.OwnBackground;
                slide.Background.FillFormat.FillType = FillType.Picture;
                slide.Background.FillFormat.PictureFillFormat.PictureFillMode = PictureFillMode.Stretch;
                slide.Background.FillFormat.PictureFillFormat.Picture.Image = backgroundImage;
                return RenderCompressedImage(
                    presentation, htmlStream, image, picturesCompression);
            }
        }

        private static byte[] RenderCompressedImage(
            Presentation presentation,
            MemoryStream htmlStream,
            IPPImage sourceImage,
            PicturesCompression picturesCompression)
        {
            presentation.Save(htmlStream, SaveFormat.Html, new HtmlOptions
            {
                PicturesCompression = picturesCompression,
                DeletePicturesCroppedAreas = true
            });
            return FindMatchingImage(htmlStream.ToArray(), sourceImage);
        }

        private static byte[] FindMatchingImage(byte[] htmlData, IPPImage sourceImage)
        {
            string html = Encoding.UTF8.GetString(htmlData);
            MatchCollection matches = Regex.Matches(html, "data:image/(?:png|jpeg);base64,\\s*([^\\\"']+)");
            byte[] bestData = null;
            double bestRatioDifference = double.MaxValue;
            long bestArea = -1;
            double sourceRatio = sourceImage.Width / (double)sourceImage.Height;

            foreach (Match match in matches)
            {
                byte[] data = Convert.FromBase64String(match.Groups[1].Value);
                using (MemoryStream stream = new MemoryStream(data))
                using (IImage image = Images.FromStream(stream))
                {
                    double ratioDifference = Math.Abs(image.Width / (double)image.Height - sourceRatio);
                    long area = (long)image.Width * image.Height;
                    if (ratioDifference < bestRatioDifference ||
                        Math.Abs(ratioDifference - bestRatioDifference) < 0.0001 && area > bestArea)
                    {
                        bestData = data;
                        bestRatioDifference = ratioDifference;
                        bestArea = area;
                    }
                }
            }

            return bestData == null ? null : ConvertToPng(bestData);
        }

        private static byte[] ConvertToPng(byte[] imageData)
        {
            using (MemoryStream source = new MemoryStream(imageData))
            using (IImage image = Images.FromStream(source))
            using (MemoryStream target = new MemoryStream())
            {
                image.Save(target, ImageFormat.Png);
                return target.ToArray();
            }
        }

    }
}
