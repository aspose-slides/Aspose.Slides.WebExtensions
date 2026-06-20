using Aspose.Slides.Export;
using Aspose.Slides.Export.Web;
using Aspose.Slides.WebExtensions.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Aspose.Slides.WebExtensions.Tests
{
    [TestClass]
    public class SLIDESNET_44594
    {
        private static readonly PicturesCompression[] Resolutions =
        {
            PicturesCompression.Dpi72,
            PicturesCompression.Dpi96,
            PicturesCompression.Dpi150,
            PicturesCompression.Dpi220,
            PicturesCompression.Dpi330,
            PicturesCompression.DocumentResolution
        };

        [TestMethod]
        public void CompressionMatchesHtml4ForDpiAndComposition()
        {
            foreach (ImageComposition composition in Enum.GetValues(typeof(ImageComposition)))
            {
                foreach (PicturesCompression resolution in Resolutions)
                {
                    using (Presentation presentation = CreatePresentation(composition))
                    {
                        IPPImage sourceImage = presentation.Images[0];
                        int sourceImageCount = presentation.Images.Count;
                        byte[] expectedData = GetHtml4ImageData(presentation, resolution);

                        Dictionary<IPPImage, byte[]> compressedImages =
                            ImageCompressorHelper.CompressAll(presentation, resolution);
                        byte[] actualData = compressedImages.ContainsKey(sourceImage)
                            ? compressedImages[sourceImage]
                            : sourceImage.BinaryData;

                        AssertImageSize(expectedData, actualData,
                            string.Format("Composition: {0}; resolution: {1}", composition, resolution));
                        Assert.AreEqual(sourceImageCount, presentation.Images.Count,
                            "Compression must not modify the source presentation.");
                    }
                }
            }
        }

        [TestMethod]
        public void Html5UsesCompressedImagesInEmbeddedAndLinkedModes()
        {
            foreach (bool embedImages in new[] { false, true })
            {
                foreach (PicturesCompression resolution in new[]
                {
                    PicturesCompression.Dpi72,
                    PicturesCompression.Dpi150,
                    PicturesCompression.Dpi330
                })
                {
                    VerifyHtml5Output(embedImages, resolution);
                }
            }
        }

        private static void VerifyHtml5Output(bool embedImages, PicturesCompression resolution)
        {
            string rootDirectory = Path.GetFullPath("../../../");
            string testDirectory = Path.Combine(rootDirectory, "TestData", "Out", "SLIDESNET_44594",
                Guid.NewGuid().ToString("N"));
            string templatePath = Path.Combine(testDirectory, "templates");
            string outputPath = Path.Combine(testDirectory, "html");

            Directory.CreateDirectory(outputPath);
            Directory.CreateDirectory(templatePath);
            CopyTemplates(Path.GetFullPath(Path.Combine(rootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "single-page")), templatePath);
            CopyTemplates(Path.GetFullPath(Path.Combine(rootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "common")), templatePath);

            try
            {
                using (Presentation presentation = CreatePresentation(ImageComposition.PictureFrame))
                {
                    byte[] expectedData = GetHtml4ImageData(presentation, resolution);
                    WebDocument document = presentation.ToSinglePageWebDocument(
                        new WebDocumentOptions { EmbedImages = embedImages },
                        templatePath,
                        outputPath,
                        resolution);
                    document.Save();

                    byte[] actualData = embedImages
                        ? GetFirstEmbeddedImageData(File.ReadAllText(Path.Combine(outputPath, "index.html")))
                        : File.ReadAllBytes(Path.Combine(outputPath, "images", "image0.png"));

                    AssertImageSize(expectedData, actualData,
                        string.Format("EmbedImages: {0}; resolution: {1}", embedImages, resolution));
                }
            }
            finally
            {
                Directory.Delete(testDirectory, true);
            }
        }

        private static Presentation CreatePresentation(ImageComposition composition)
        {
            string rootDirectory = Path.GetFullPath("../../../");
            string sourcePath = Path.Combine(rootDirectory, "TestData", "SLIDESNET_43224", "SLIDESNET-43224.pptx");
            byte[] imageData;
            using (Presentation source = new Presentation(sourcePath))
                imageData = source.Images[0].BinaryData;

            Presentation presentation = new Presentation();
            IPPImage image = presentation.Images.AddImage(imageData);
            ISlide slide = presentation.Slides[0];

            switch (composition)
            {
                case ImageComposition.PictureFrame:
                    slide.Shapes.AddPictureFrame(ShapeType.Rectangle, 20, 20, 400, 225, image);
                    break;

                case ImageComposition.GroupedPictureFrame:
                    IGroupShape outerGroup = slide.Shapes.AddGroupShape();
                    IGroupShape innerGroup = outerGroup.Shapes.AddGroupShape();
                    innerGroup.Shapes.AddPictureFrame(ShapeType.Rectangle, 20, 20, 400, 225, image);
                    break;

                case ImageComposition.ShapeFill:
                    IAutoShape shape = slide.Shapes.AddAutoShape(ShapeType.Rectangle, 20, 20, 400, 225);
                    SetPictureFill(shape.FillFormat, image);
                    break;

                case ImageComposition.SlideBackground:
                    slide.Background.Type = BackgroundType.OwnBackground;
                    SetPictureFill(slide.Background.FillFormat, image);
                    break;

                case ImageComposition.MasterBackground:
                    IMasterSlide master = presentation.Masters[0];
                    master.Background.Type = BackgroundType.OwnBackground;
                    SetPictureFill(master.Background.FillFormat, image);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("composition");
            }

            return presentation;
        }

        private static void SetPictureFill(IFillFormat fillFormat, IPPImage image)
        {
            fillFormat.FillType = FillType.Picture;
            fillFormat.PictureFillFormat.PictureFillMode = PictureFillMode.Stretch;
            fillFormat.PictureFillFormat.Picture.Image = image;
        }

        private static byte[] GetHtml4ImageData(Presentation presentation, PicturesCompression resolution)
        {
            using (MemoryStream htmlStream = new MemoryStream())
            {
                presentation.Save(htmlStream, SaveFormat.Html,
                    new HtmlOptions { PicturesCompression = resolution });
                return GetFirstEmbeddedImageData(System.Text.Encoding.UTF8.GetString(htmlStream.ToArray()));
            }
        }

        private static byte[] GetFirstEmbeddedImageData(string html)
        {
            Match match = Regex.Match(html, "data:image/(?:png|jpeg);base64,\\s*([^\\\"']+)");
            Assert.IsTrue(match.Success, "The exported HTML does not contain an embedded raster image.");
            return Convert.FromBase64String(match.Groups[1].Value);
        }

        private static void AssertImageSize(byte[] expectedData, byte[] actualData, string message)
        {
            using (MemoryStream expectedStream = new MemoryStream(expectedData))
            using (MemoryStream actualStream = new MemoryStream(actualData))
            using (IImage expectedImage = Images.FromStream(expectedStream))
            using (IImage actualImage = Images.FromStream(actualStream))
            {
                Assert.AreEqual(expectedImage.Width, actualImage.Width, message);
                Assert.AreEqual(expectedImage.Height, actualImage.Height, message);
            }
        }

        private static void CopyTemplates(string sourcePath, string destinationPath)
        {
            foreach (string directory in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(directory.Replace(sourcePath, destinationPath));

            foreach (string file in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                File.Copy(file, file.Replace(sourcePath, destinationPath), true);
        }

        private enum ImageComposition
        {
            PictureFrame,
            GroupedPictureFrame,
            ShapeFill,
            SlideBackground,
            MasterBackground
        }
    }
}
