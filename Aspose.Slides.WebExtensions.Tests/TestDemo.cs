using Aspose.Slides;
using Aspose.Slides.Export.Web;
using Aspose.Slides.WebExtensions;
using NUnit.Framework;
using System.IO;

namespace Aspose.Slides.WebExtensions.Tests
{
    public class TestDemo
    {
        private string TemplatePath = null;
        private string PresentationFilePath = null;
        private string OutputPath = null;
        private string RootDirectory = null;

        const string HEADLINE_CONNECTOR_SNIPPET1 = 
            "<div " +
                "class=\"shape\" " +
                "id=\"slide-2147483662-shape-7\" " +
                "style=\"" +
                    "left: 57px; " +
                    "top: 1214px; " +
                    "width: 864px; " +
                    "height: 2px; " +
                    "background-image: url(images/connector1.png); " +
                    "background-size: 100% 100%; " +
                    "transform: rotate(0deg);\" >";

        const string HEADLINE_CONNECTOR_SNIPPET2 = 
            "<div " +
                "class=\"shape\" " +
                "id=\"slide-2147483660-shape-1\" " +
                "style=\"" +
                    "left: 57px; " +
                    "top: 1214px; " +
                    "width: 864px; " +
                    "height: 2px; " +
                    "background-image: url(images/connector2.png); " +
                    "background-size: 100% 100%; " +
                    "transform: rotate(0deg);\" >";

        [SetUp]
        public void Setup()
        {
            RootDirectory = TestContext.CurrentContext.TestDirectory;
            PresentationFilePath = Path.Combine(RootDirectory, "TestData", "demo.pptx");
            TemplatePath = Path.Combine(RootDirectory, "templates", "single-page");
            OutputPath = Path.Combine(RootDirectory, "single-page-demo-output");

            using (Presentation pres = new Presentation(PresentationFilePath))
            {
                WebDocument document = pres.ToSinglePageWebDocument(TemplatePath, OutputPath);
                document.Save();
            }
        }

        [Test]
        public void TestHeadlineConnector()
        {
            string indexHtmlPath = Path.Combine(OutputPath, "index.html");
            Assert.IsTrue(File.Exists(indexHtmlPath));
            string indexHtmlContent = File.ReadAllText(indexHtmlPath);
            Assert.IsTrue(indexHtmlContent.Contains(HEADLINE_CONNECTOR_SNIPPET1));
            Assert.IsTrue(indexHtmlContent.Contains(HEADLINE_CONNECTOR_SNIPPET2));
        }
    }
}