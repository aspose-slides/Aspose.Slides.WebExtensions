using Aspose.Slides.Export.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Aspose.Slides.WebExtensions.Tests
{
    [TestClass]
    public class TestMultilevelBullets
    {
        private static string TemplatePath = null;
        private static string PresentationFilePath = null;
        private static string OutputPath = null;
        private static string RootDirectory = null;
        private static XmlDocument indexHtml;
        private static XmlNamespaceManager namespaceManager;

        static TestMultilevelBullets()
        {
            RootDirectory = Path.GetFullPath(".");
            PresentationFilePath = Path.Combine(RootDirectory, "TestData", "multilevel-bullets.pptx");
            TemplatePath = Path.Combine(RootDirectory, "templates", "single-page");
            OutputPath = Path.Combine(RootDirectory, "single-page-multilevel-bullets-animated-output");

            using (Presentation pres = new Presentation(PresentationFilePath))
            {
                WebDocumentOptions options = new WebDocumentOptions
                {
                    TemplateEngine = new RazorTemplateEngine(),
                    OutputSaver = new FileOutputSaver(),
                    AnimateTransitions = true
                };

                WebDocument document = pres.ToSinglePageWebDocument(options, TemplatePath, OutputPath);
                document.Save();
            }

            string indexHtmlPath = Path.Combine(OutputPath, "index.html");
            string indexHtmlContent = File.ReadAllText(indexHtmlPath);
            indexHtml = new XmlDocument();
            indexHtml.XmlResolver = null;
            indexHtml.LoadXml(indexHtmlContent);
            namespaceManager = new XmlNamespaceManager(indexHtml.NameTable);
            namespaceManager.AddNamespace("html", "http://www.w3.org/1999/xhtml");
        }
        [TestMethod]
        public void TestSvgDivPathsBlock()
        {
            XmlElement root = indexHtml.DocumentElement;
            XmlNode svg = root.SelectSingleNode("//html:div[@id='svgdivpaths']/html:svg", namespaceManager);

            Assert.IsNotNull(svg, "div#svgdivpaths block not found");
            string width = svg.Attributes["width"].Value;
            Assert.AreEqual("959.75", width, "div#svgdivpaths block should be 959.75 px wide");
            string height = svg.Attributes["height"].Value;
            Assert.AreEqual("540", height, "div#svgdivpaths block should be 540 px high");
            string viewbox = svg.Attributes["viewBox"].Value;
            Assert.AreEqual("0 0 959.75 540", viewbox, "div#svgdivpaths block viewBox attribute should be equal to '0 0 959.75 540'");
        }
        [TestMethod]
        public void TestAnimationJs()
        {
            string animationsJsPath = Path.Combine(OutputPath, "animation.js");
            string animationsJsContent = File.ReadAllText(animationsJsPath);
            Assert.IsTrue(animationsJsContent.Contains("var frameWidth = 959.75;"), "animation.js should contain string 'var frameWidth = 959.75;'");
            Assert.IsTrue(animationsJsContent.Contains("var frameHeight = 540;"), "animation.js should contain string 'var frameHeight = 540;'");
        }
        [TestMethod]
        public void TestPresCss()
        {
            string pressCssPath = Path.Combine(OutputPath, "pres.css");
            string presCssContent = File.ReadAllText(pressCssPath);
            Match bgMatch = System.Text.RegularExpressions.Regex.Match(presCssContent, "\\.bg[^{]*\\{[^}]+\\}");
            Assert.IsTrue(bgMatch.Success, ".bg not found in pres.css");
            string bgClassStyle = bgMatch.Value;
            Assert.IsTrue(bgClassStyle.Contains("width: 979.75px;"), ".bg in pres.css should have property width equals to 813.75px");
            Assert.IsTrue(bgClassStyle.Contains("height: 560px;"), ".bg in pres.css should have property height equals to 615.25px");

            Match slideMatch = System.Text.RegularExpressions.Regex.Match(presCssContent, "\\.slide\\b[^{]*\\{[^}]+\\}");
            Assert.IsTrue(slideMatch.Success, ".slide not found in pres.css");
            string slideClassStyle = slideMatch.Value;
            Assert.IsTrue(slideClassStyle.Contains("width: 964.75px;"), ".slide in pres.css should have property width equals to 798.75px");
            Assert.IsTrue(slideClassStyle.Contains("height: 545px;"), ".slide in pres.css should have property height equals to 600.25px");
        }
    }
}