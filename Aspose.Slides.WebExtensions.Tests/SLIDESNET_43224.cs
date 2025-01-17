using Aspose.Slides.Export.Web;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aspose.Slides.Export;
using System.Drawing;

namespace Aspose.Slides.WebExtensions.Tests
{
    [TestClass]
    public class SLIDESNET_43224
    {
        [TestMethod]
        public void Test_43224()
        {
            var RootDirectory = Path.GetFullPath("../../../");
            var PresentationFilePath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43224", "SLIDESNET-43224.pptx");
            var EthalonPath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43224", "html");
            var TemplatePath = Path.Combine(RootDirectory, "TestData", "Out", "templates");
            var OutputPath = Path.Combine(RootDirectory, "TestData", "Out", "SLIDESNET_43224");

            var sourcePath1 = Path.GetFullPath(Path.Combine(RootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "single-page"));
            var sourcePath2 = Path.GetFullPath(Path.Combine(RootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "common"));

            Directory.CreateDirectory(OutputPath);
            Directory.CreateDirectory(TemplatePath);

            foreach (string dirPath in Directory.GetDirectories(sourcePath1, "*", SearchOption.AllDirectories)) Directory.CreateDirectory(dirPath.Replace(sourcePath1, TemplatePath));
            foreach (string newPath in Directory.GetFiles(sourcePath1, "*.*", SearchOption.AllDirectories)) File.Copy(newPath, newPath.Replace(sourcePath1, TemplatePath), true);

            foreach (string dirPath in Directory.GetDirectories(sourcePath2, "*", SearchOption.AllDirectories)) Directory.CreateDirectory(dirPath.Replace(sourcePath2, TemplatePath));
            foreach (string newPath in Directory.GetFiles(sourcePath2, "*.*", SearchOption.AllDirectories)) File.Copy(newPath, newPath.Replace(sourcePath2, TemplatePath), true);

            string testImagePath = Path.Combine(OutputPath, "images/image0red1.png");

            using (Presentation pres = new Presentation(PresentationFilePath))
            {
                WebDocument document = pres.ToSinglePageWebDocument(
                    new WebDocumentOptions { EmbedImages = false },
                    TemplatePath,
                    OutputPath);
                document.Global.Put("picturesCompression", PicturesCompression.Dpi72);
                document.Save();

                using (Bitmap testImage = new Bitmap(testImagePath))
                {
                    Assert.AreEqual(608, testImage.Width);
                    Assert.AreEqual(342, testImage.Height);
                }

                document = pres.ToSinglePageWebDocument(
                    new WebDocumentOptions { EmbedImages = false },
                    TemplatePath,
                    OutputPath);
                document.Global.Put("picturesCompression", PicturesCompression.Dpi96);
                document.Save();

                using (Bitmap testImage = new Bitmap(testImagePath))
                {
                    Assert.AreEqual(811, testImage.Width);
                    Assert.AreEqual(456, testImage.Height);
                }

                document = pres.ToSinglePageWebDocument(
                    new WebDocumentOptions { EmbedImages = false },
                    TemplatePath,
                    OutputPath);
                document.Global.Put("picturesCompression", PicturesCompression.Dpi96);
                document.Save();

                using (Bitmap testImage = new Bitmap(testImagePath))
                {
                    Assert.AreEqual(811, testImage.Width);
                    Assert.AreEqual(456, testImage.Height);
                }

                document = pres.ToSinglePageWebDocument(
                    new WebDocumentOptions { EmbedImages = false },
                    TemplatePath,
                    OutputPath);
                document.Global.Put("picturesCompression", PicturesCompression.Dpi150);
                document.Save();

                using (Bitmap testImage = new Bitmap(testImagePath))
                {
                    Assert.AreEqual(1268, testImage.Width);
                    Assert.AreEqual(713, testImage.Height);
                }

                document = pres.ToSinglePageWebDocument(
                    new WebDocumentOptions { EmbedImages = false },
                    TemplatePath,
                    OutputPath);
                document.Global.Put("picturesCompression", PicturesCompression.Dpi220);
                document.Save();

                using (Bitmap testImage = new Bitmap(testImagePath))
                {
                    Assert.AreEqual(1400, testImage.Width);
                    Assert.AreEqual(788, testImage.Height);
                }

                document = pres.ToSinglePageWebDocument(
                    new WebDocumentOptions { EmbedImages = false },
                    TemplatePath,
                    OutputPath);
                document.Global.Put("picturesCompression", PicturesCompression.Dpi330);
                document.Save();

                using (Bitmap testImage = new Bitmap(testImagePath))
                {
                    Assert.AreEqual(1400, testImage.Width);
                    Assert.AreEqual(788, testImage.Height);
                }

                document = pres.ToSinglePageWebDocument(
                    new WebDocumentOptions { EmbedImages = false },
                    TemplatePath,
                    OutputPath);
                document.Global.Put("picturesCompression", PicturesCompression.DocumentResolution);
                document.Save();

                using (Bitmap testImage = new Bitmap(testImagePath))
                {
                    Assert.AreEqual(1400, testImage.Width);
                    Assert.AreEqual(788, testImage.Height);
                }
            }
        }
    }
}