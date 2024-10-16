using Aspose.Slides.Export.Web;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aspose.Slides.WebExtensions.Tests
{
    [TestClass]
    public class SLIDESNET_44725

    {
        [TestMethod]
        public void Test_44725()
        {
            var RootDirectory = Path.GetFullPath("../../../");
            var PresentationFilePath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_44725", "SLIDESNET-44725.pptx");
            var EthalonPath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_44725", "html");
            var TemplatePath = Path.Combine(RootDirectory, "TestData", "Out", "templates");
            var OutputPath = Path.Combine(RootDirectory, "TestData", "Out", "SLIDESNET_44725");

            var sourcePath1 = Path.GetFullPath(Path.Combine(RootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "single-page"));
            var sourcePath2 = Path.GetFullPath(Path.Combine(RootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "common"));

            Directory.CreateDirectory(OutputPath);
            Directory.CreateDirectory(TemplatePath);

            foreach (string dirPath in Directory.GetDirectories(sourcePath1, "*", SearchOption.AllDirectories)) Directory.CreateDirectory(dirPath.Replace(sourcePath1, TemplatePath));
            foreach (string newPath in Directory.GetFiles(sourcePath1, "*.*", SearchOption.AllDirectories)) File.Copy(newPath, newPath.Replace(sourcePath1, TemplatePath), true);

            foreach (string dirPath in Directory.GetDirectories(sourcePath2, "*", SearchOption.AllDirectories)) Directory.CreateDirectory(dirPath.Replace(sourcePath2, TemplatePath));
            foreach (string newPath in Directory.GetFiles(sourcePath2, "*.*", SearchOption.AllDirectories)) File.Copy(newPath, newPath.Replace(sourcePath2, TemplatePath), true);

            using (Presentation pres = new Presentation(PresentationFilePath))
            {
                WebDocument document = pres.ToSinglePageWebDocument(
                    new WebDocumentOptions
                    {
                        EmbedImages = false
                    },
                    TemplatePath,
                    OutputPath);
                document.Save();
            }

            TestUtils.CompareDir(EthalonPath, OutputPath, _ => _ /*Regex.Replace(_, "<div class=\"shape\" id=\"slide-2147483725-shape-0\" [^>]+>", "")*/, true);
        }
    }
}