using Aspose.Slides.Export.Web;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aspose.Slides.WebExtensions.Tests
{
    [TestClass]
    public class SLIDESNET_43877
    {
        [TestMethod]
        public void Test_Demo_Transitions()
        {
            var RootDirectory = Path.GetFullPath("../../../");
            var PresentationFilePath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43877", "demo-transitions.pptx");
            var EthalonPath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43877", "demo-transitions");
            var TemplatePath = Path.Combine(RootDirectory, "TestData", "Out", "templates");
            var OutputPath = Path.Combine(RootDirectory, "TestData", "Out", "SLIDESNET_43877", "demo-transitions");

            var sourcePath1 = Path.GetFullPath( Path.Combine(RootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "single-page"));
            var sourcePath2 = Path.GetFullPath( Path.Combine(RootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "common"));

            Directory.CreateDirectory(OutputPath);
            Directory.CreateDirectory(TemplatePath);

            foreach (string dirPath in Directory.GetDirectories(sourcePath1, "*", SearchOption.AllDirectories)) Directory.CreateDirectory(dirPath.Replace(sourcePath1, TemplatePath));
            foreach (string newPath in Directory.GetFiles(sourcePath1, "*.*", SearchOption.AllDirectories)) File.Copy(newPath, newPath.Replace(sourcePath1, TemplatePath), true);

            foreach (string dirPath in Directory.GetDirectories(sourcePath2, "*", SearchOption.AllDirectories)) Directory.CreateDirectory(dirPath.Replace(sourcePath2, TemplatePath));
            foreach (string newPath in Directory.GetFiles(sourcePath2, "*.*", SearchOption.AllDirectories)) File.Copy(newPath, newPath.Replace(sourcePath2, TemplatePath), true);

            using (Presentation pres = new Presentation(PresentationFilePath))
            {
                WebDocumentOptions options = new WebDocumentOptions
                {
                    EmbedImages = true
                };
                WebDocument document = pres.ToSinglePageWebDocument(options, TemplatePath, OutputPath);
                document.Save();
            }
            TestUtils.CompareDir(EthalonPath, OutputPath, _ =>
            {
                _ = Regex.Replace(_, "<div class=\"shape\" id=\"slide-280-shape-39\" [^>]+>", "");
                _ = Regex.Replace(_, "<div class=\"shape\" id=\"slide-2147483662-shape-7\" [^>]+>", "");
                _ = Regex.Replace(_, "<div class=\"shape\" id=\"slide-280-shape-38\" [^>]+>", "");
                _ = Regex.Replace(_, "<div class=\"shape\" id=\"slide-2147483660-shape-1\" [^>]+>", "");
                return _;
            });
        }


        [TestMethod]
        public void Test_Html5issues()
        {
            var RootDirectory = Path.GetFullPath("../../../");
            var PresentationFilePath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43877", "html5issues.pptx");
            var EthalonPath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43877", "html5issues");
            var TemplatePath = Path.Combine(RootDirectory, "TestData", "Out", "templates");
            var OutputPath = Path.Combine(RootDirectory, "TestData", "Out", "SLIDESNET_43877", "html5issues");

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
                WebDocumentOptions options = new WebDocumentOptions
                {
                    EmbedImages = true
                };
                WebDocument document = pres.ToSinglePageWebDocument(options, TemplatePath, OutputPath);
                document.Save();
            }

            TestUtils.CompareDir(EthalonPath, OutputPath, _ =>
            {
                string content = _;
                content = Regex.Replace(content, "<div class=\"shape\" id=\"slide-277-shape-0\" [^>]+>", "");
                content = Regex.Replace(content, "<div class=\"shape\" id=\"slide-2147483662-shape-19\" [^>]+>", "");
                content = Regex.Replace(content, "<div class=\"shape\" id=\"slide-2147309457-shape-37\" [^>]+>", "");
                content = Regex.Replace(content, "<div class=\"shape\" id=\"slide-2147309457-shape-38\" [^>]+>", "");
                content = Regex.Replace(content, "<div class=\"shape\" id=\"slide-2147309457-shape-51\" [^>]+>", "");
                content = Regex.Replace(content, "<div class=\"shape\" id=\"slide-2147309457-shape-53\" [^>]+>", "");
                return content;
            });
        }


        [TestMethod]
        public void Test_Multilevel_Bullets()
        {
            var RootDirectory = Path.GetFullPath("../../../");
            var PresentationFilePath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43877", "multilevel-bullets.pptx");
            var EthalonPath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43877", "multilevel-bullets");
            var TemplatePath = Path.Combine(RootDirectory, "TestData", "Out", "templates");
            var OutputPath = Path.Combine(RootDirectory, "TestData", "Out", "SLIDESNET_43877", "multilevel-bullets");

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
                WebDocumentOptions options = new WebDocumentOptions
                {
                    EmbedImages = true
                };
                WebDocument document = pres.ToSinglePageWebDocument(options, TemplatePath, OutputPath);
                document.Save();
            }

            TestUtils.CompareDir(EthalonPath, OutputPath, _ => Regex.Replace(_, "<div class=\"shape\" id=\"slide-2147493457-shape-0\" [^>]+>", ""));
        }


        [TestMethod]
        public void Test_Artificial()
        {
            var RootDirectory = Path.GetFullPath("../../../");
            var PresentationFilePath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43877", "artificial.pptx");
            var EthalonPath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43877", "artificial");
            var TemplatePath = Path.Combine(RootDirectory, "TestData", "Out", "templates");
            var OutputPath = Path.Combine(RootDirectory, "TestData", "Out", "SLIDESNET_43877", "artificial");

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
                WebDocumentOptions options = new WebDocumentOptions
                {
                    EmbedImages = true
                };
                WebDocument document = pres.ToSinglePageWebDocument(options, TemplatePath, OutputPath);
                document.Save();
            }

            TestUtils.CompareDir(EthalonPath, OutputPath, _ => Regex.Replace(_, "background-image: url\\([^)]+\\)", "")) ;
        }


        [TestMethod]
        public void Test_SamplePPT1()
        {
            var RootDirectory = Path.GetFullPath("../../../");
            var PresentationFilePath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43877", "SamplePPT1.ppt");
            var EthalonPath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43877", "SamplePPT1");
            var TemplatePath = Path.Combine(RootDirectory, "TestData", "Out", "templates");
            var OutputPath = Path.Combine(RootDirectory, "TestData", "Out", "SLIDESNET_43877", "SamplePPT1");

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
                WebDocumentOptions options = new WebDocumentOptions
                {
                    EmbedImages = true
                };
                WebDocument document = pres.ToSinglePageWebDocument(options, TemplatePath, OutputPath);
                document.Save();
            }

            TestUtils.CompareDir(EthalonPath, OutputPath, _ => 
            {
                _ = Regex.Replace(_, "<div class=\"shape\" id=\"slide-257-shape-6\" [^>]+>", "");
                _ = Regex.Replace(_, "<div class=\"shape\" id=\"slide-257-shape-8\" [^>]+>", "");
                return _;
            });
        }


        [TestMethod]
        public void Test_Table()
        {
            var RootDirectory = Path.GetFullPath("../../../");
            var PresentationFilePath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43877", "table.pptx");
            var EthalonPath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_43877", "table");
            var TemplatePath = Path.Combine(RootDirectory, "TestData", "Out", "templates");
            var OutputPath = Path.Combine(RootDirectory, "TestData", "Out", "SLIDESNET_43877", "table");

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
                WebDocumentOptions options = new WebDocumentOptions
                {
                    EmbedImages = true
                };
                WebDocument document = pres.ToSinglePageWebDocument(options, TemplatePath, OutputPath);
                document.Save();
            }

            TestUtils.CompareDir(EthalonPath, OutputPath, _ => _);
        }
    }
}