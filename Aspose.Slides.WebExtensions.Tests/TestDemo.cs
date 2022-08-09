using Aspose.Slides;
using Aspose.Slides.Export.Web;
using Aspose.Slides.WebExtensions;
using NUnit.Framework;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Aspose.Slides.WebExtensions.Tests
{
    public class TestDemo
    {
        private string TemplatePath = null;
        private string PresentationFilePath = null;
        private string OutputPath = null;
        private string RootDirectory = null;

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
        static string FixVideoTag(string input)
        {
            input = Regex.Replace(input, "(<video[^>]+) controls>", "$1 controls=\"\">");
            input = Regex.Replace(input, "<source[^>]+>", "$1 />");
            return input;
        }

        [Test]
        public void TestHeadlineConnector()
        {
            string indexHtmlPath = Path.Combine(OutputPath, "index.html");
            Assert.IsTrue(File.Exists(indexHtmlPath));
            string indexHtmlContent = File.ReadAllText(indexHtmlPath);
            indexHtmlContent = FixVideoTag(indexHtmlContent);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(indexHtmlContent);
            var mgr = new XmlNamespaceManager(doc.NameTable);
            mgr.AddNamespace("html", "http://www.w3.org/1999/xhtml");
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("//html:div[@id='slide-2147483660-shape-1']", mgr);

            Assert.AreEqual(9, nodes.Count);
            foreach(XmlNode node in nodes)
            {
                string style = node.Attributes["style"].Value;
                Assert.IsTrue(style.Contains("height: 2px"), "");
            }
        }
    }
}