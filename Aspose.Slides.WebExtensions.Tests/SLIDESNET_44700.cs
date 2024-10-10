using Aspose.Slides.Export.Web;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Aspose.Slides.WebExtensions.Tests
{
    [TestClass]
    public class SLIDESNET_44700
    {
        [TestMethod]
        public void Test_44700()
        {
            var RootDirectory = Path.GetFullPath("../../../");
            var testCommonPath = Path.Combine(RootDirectory, "TestData");
            var testDataPath = Path.Combine(testCommonPath, "SLIDESNET_44700");
            var testOutPath = Path.Combine(testCommonPath, "Out");
            var templatesPath = Path.Combine(RootDirectory, "../Aspose.Slides.WebExtensions/templates");

            var PresentationFilePath = Path.Combine(testDataPath, "SLIDESNET-44700.pptx");
            var EthalonPath =  Path.Combine(testDataPath, "html");
            var TemplatePath = Path.Combine(testOutPath, "templates");
            var OutputPath = Path.Combine(testOutPath, "SLIDESNET_44700");

            var sourcePath1 = Path.GetFullPath(Path.Combine(templatesPath, "single-page"));
            var sourcePath2 = Path.GetFullPath(Path.Combine(templatesPath, "common"));

            Directory.CreateDirectory(OutputPath);
            Directory.CreateDirectory(TemplatePath);

            foreach (string dirPath in Directory.GetDirectories(sourcePath1, "*", SearchOption.AllDirectories)) Directory.CreateDirectory(dirPath.Replace(sourcePath1, TemplatePath));
            foreach (string newPath in Directory.GetFiles(sourcePath1, "*.*", SearchOption.AllDirectories)) File.Copy(newPath, newPath.Replace(sourcePath1, TemplatePath), true);

            foreach (string dirPath in Directory.GetDirectories(sourcePath2, "*", SearchOption.AllDirectories)) Directory.CreateDirectory(dirPath.Replace(sourcePath2, TemplatePath));
            foreach (string newPath in Directory.GetFiles(sourcePath2, "*.*", SearchOption.AllDirectories)) File.Copy(newPath, newPath.Replace(sourcePath2, TemplatePath), true);

            using (Presentation pres = new Presentation(PresentationFilePath))
            {
                WebDocument document = pres.ToSinglePageWebDocument(
                    new WebDocumentOptions { EmbedImages = true },
                    TemplatePath,
                    OutputPath);
                document.Save();
            }

            TestUtils.CompareDir(EthalonPath, OutputPath, _ => Regex.Replace(_, "background-image: *url\\(\'[^\']+\'\\)", ""), true);
        }
    }
}