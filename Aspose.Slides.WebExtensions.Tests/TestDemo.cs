using Aspose.Slides;
using Aspose.Slides.Export.Web;
using Aspose.Slides.WebExtensions;
using NUnit.Framework;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Aspose.Slides.WebExtensions.Tests
{
    [TestFixture]
    public class TestDemo
    {
        private string TemplatePath = null;
        private string PresentationFilePath = null;
        private string OutputPath = null;
        private string RootDirectory = null;
        private XmlDocument indexHtml;
        private XmlNamespaceManager namespaceManager;

        [OneTimeSetUp]
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

            string indexHtmlPath = Path.Combine(OutputPath, "index.html");
            string indexHtmlContent = File.ReadAllText(indexHtmlPath);
            indexHtmlContent = FixVideoTag(indexHtmlContent);
            indexHtml = new XmlDocument();
            indexHtml.XmlResolver = null;
            indexHtml.LoadXml(indexHtmlContent);
            namespaceManager = new XmlNamespaceManager(indexHtml.NameTable);
            namespaceManager.AddNamespace("html", "http://www.w3.org/1999/xhtml");
        }
        static string FixVideoTag(string input)
        {
            input = Regex.Replace(input, "(<video[^>]+) controls>", "$1 controls=\"\">");
            input = Regex.Replace(input, "(<source[^>]+)>", "$1 />");
            return input;
        }

        [Test]
        public void TestHeadlineConnector()
        {
            XmlElement root = indexHtml.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("//html:div[@id='slide-2147483660-shape-1']", namespaceManager);

            Assert.AreEqual(9, nodes.Count);
            foreach(XmlNode node in nodes)
            {
                string style = node.Attributes["style"].Value;
                Assert.IsTrue(style.Contains("height: 2px"), "Headline connector's div block should be 2px high");
            }
        }
    }
}