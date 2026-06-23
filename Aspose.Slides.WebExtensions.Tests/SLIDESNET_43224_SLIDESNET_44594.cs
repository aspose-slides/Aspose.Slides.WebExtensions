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
    public class SLIDESNET_43224_SLIDESNET_44594
    {
        private enum ImageComposition
        {
            PictureFrame,
            GroupedPictureFrame,
            ShapeFill,
            SlideBackground,
            MasterBackground
        }
        private static readonly PicturesCompression[] Resolutions =
        {
            PicturesCompression.Dpi72,
            PicturesCompression.Dpi96,
            PicturesCompression.Dpi150,
            PicturesCompression.Dpi220,
            PicturesCompression.Dpi330,
            PicturesCompression.DocumentResolution
        };
        private Dictionary<ImageComposition, string> paths = new Dictionary<ImageComposition, string>()
        {
            { ImageComposition.PictureFrame, "SLIDESNET-43224-picture-frame.pptx" },
            { ImageComposition.GroupedPictureFrame, "SLIDESNET-43224-grouped-picture-frame.pptx" },
            { ImageComposition.ShapeFill, "SLIDESNET-43224-shape-fill.pptx" },
            { ImageComposition.SlideBackground, "SLIDESNET-43224-slide-background.pptx" },
            { ImageComposition.MasterBackground, "SLIDESNET-43224-master-background.pptx" }
        };

        [TestMethod]
        public void PicturesCompressionMatchesHtml4ForHtml5()
        {
            foreach (ImageComposition composition in Enum.GetValues(typeof(ImageComposition)))
            {
                foreach (PicturesCompression resolution in Resolutions)
                {
                    using (Presentation presentation = new Presentation(GetTestDataPath(paths[composition])))
                    {
                        IPPImage sourceImage = presentation.Images[0];
                        int sourceImageCount = presentation.Images.Count;
                        byte[] expectedData = GetHtml4ImageData(presentation, resolution);

                        Dictionary<IPPImage, byte[]> compressedImages = ImageCompressorHelper.CompressAll(presentation, resolution);
                        byte[] actualData = compressedImages.ContainsKey(sourceImage) ? compressedImages[sourceImage] : sourceImage.BinaryData;

                        AssertImageSize(expectedData, actualData, string.Format("Composition: {0}; resolution: {1}", composition, resolution));
                        Assert.AreEqual(sourceImageCount, presentation.Images.Count, "Compression must not modify the source presentation.");
                    }
                }
            }

            VerifyHtml5Output(true, PicturesCompression.Dpi72);  VerifyHtml5Output(false, PicturesCompression.Dpi72);
            VerifyHtml5Output(true, PicturesCompression.Dpi150); VerifyHtml5Output(false, PicturesCompression.Dpi150);
            VerifyHtml5Output(true, PicturesCompression.Dpi330); VerifyHtml5Output(false, PicturesCompression.Dpi330);
        }

        [TestMethod]
        public void MixedRasterImageUsagesUseCompression()
        {
            var resolution = PicturesCompression.Dpi72;
            using (Presentation presentation = new Presentation(GetTestDataPath("SLIDESNET-43224-mixed.pptx")))
            {
                int sourceImageCount = presentation.Images.Count;
                Dictionary<IPPImage, byte[]> compressedImages =
                    ImageCompressorHelper.CompressAll(presentation, resolution);

                Assert.AreEqual(1, sourceImageCount, "The mixed raster presentation must contain one image.");
                Assert.AreEqual(sourceImageCount, presentation.Images.Count, "Compression must not modify the source presentation.");
                Assert.IsTrue(compressedImages.ContainsKey(presentation.Images[0]), "The raster image used as frame, group shape and background must be compressed.");

                AssertCompressedImageIsSmaller(presentation.Images[0].BinaryData, compressedImages[presentation.Images[0]]);

                foreach (bool embedImages in new[] { false, true })
                    VerifyHtml5Output(presentation, embedImages, resolution);
            }
        }

        private void VerifyHtml5Output(bool embedImages, PicturesCompression resolution)
        {
            string rootDirectory = Path.GetFullPath("../../../");
            string testDirectory = Path.Combine(rootDirectory, "TestData", "Out", "SLIDESNET_43224_SLIDESNET_44594",
                Guid.NewGuid().ToString("N"));
            string templatePath = Path.Combine(testDirectory, "templates");
            string outputPath = Path.Combine(testDirectory, "html");

            Directory.CreateDirectory(outputPath);
            Directory.CreateDirectory(templatePath);
            CopyTemplates(Path.GetFullPath(Path.Combine(rootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "single-page")), templatePath);
            CopyTemplates(Path.GetFullPath(Path.Combine(rootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "common")), templatePath);

            try
            {
                using (Presentation presentation = new Presentation(GetTestDataPath(paths[ImageComposition.PictureFrame])) )
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

        private static void VerifyHtml5Output(
            Presentation presentation,
            bool embedImages,
            PicturesCompression resolution)
        {
            string rootDirectory = Path.GetFullPath("../../../");
            string testDirectory = Path.Combine(rootDirectory, "TestData", "Out", "SLIDESNET_43224_SLIDESNET_44594",
                Guid.NewGuid().ToString("N"));
            string templatePath = Path.Combine(testDirectory, "templates");
            string outputPath = Path.Combine(testDirectory, "html");

            Directory.CreateDirectory(outputPath);
            Directory.CreateDirectory(templatePath);
            CopyTemplates(Path.GetFullPath(Path.Combine(rootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "single-page")), templatePath);
            CopyTemplates(Path.GetFullPath(Path.Combine(rootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "common")), templatePath);

            try
            {
                WebDocument document = presentation.ToSinglePageWebDocument(
                    new WebDocumentOptions { EmbedImages = embedImages },
                    templatePath,
                    outputPath,
                    resolution);
                document.Save();

                if (embedImages)
                {
                    string html = File.ReadAllText(Path.Combine(outputPath, "index.html"));
                    Assert.IsTrue(Regex.Matches(html, "data:image/png;base64,\\s*[^\\\"']+").Count >= 1,
                        "Embedded HTML5 output must contain compressed raster image.");
                }
                else
                {
                    Assert.IsTrue(File.Exists(Path.Combine(outputPath, "images", "image0.png")),
                        "Linked HTML5 output must contain compressed raster image.");
                }
            }
            finally
            {
                Directory.Delete(testDirectory, true);
            }
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

        private static string GetTestDataPath(string fileName)
        {
            string rootDirectory = Path.GetFullPath("../../../");
            return Path.Combine(rootDirectory, "TestData", "SLIDESNET_43224", fileName);
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

        private static void AssertCompressedImageIsSmaller(byte[] sourceData, byte[] compressedData)
        {
            using (MemoryStream sourceStream = new MemoryStream(sourceData))
            using (MemoryStream compressedStream = new MemoryStream(compressedData))
            using (IImage sourceImage = Images.FromStream(sourceStream))
            using (IImage compressedImage = Images.FromStream(compressedStream))
            {
                Assert.IsTrue(compressedImage.Width < sourceImage.Width || compressedImage.Height < sourceImage.Height,
                    "Dpi72 must visibly reduce the raster image dimensions.");
            }
        }

        private static void CopyTemplates(string sourcePath, string destinationPath)
        {
            foreach (string directory in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(directory.Replace(sourcePath, destinationPath));

            foreach (string file in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                File.Copy(file, file.Replace(sourcePath, destinationPath), true);
        }
    }
}
