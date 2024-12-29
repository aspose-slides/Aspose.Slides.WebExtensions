using Aspose.Slides.Export.Web;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Aspose.Slides.WebExtensions.Tests
{
    [TestClass]
    public class SLIDESNET_44797

    {
        [TestMethod]
        public void Test_44797()
        {
            var RootDirectory = Path.GetFullPath("../../../");
            var PresentationFilePath = Path.Combine(RootDirectory, "TestData", "SLIDESNET_44797", "SLIDESNET-44797.pptx");
            var TemplatePath = Path.Combine(RootDirectory, "TestData", "Out", "templates");
            var OutputPath = Path.Combine(RootDirectory, "TestData", "Out", "SLIDESNET_44797");

            var sourcePath1 = Path.GetFullPath(Path.Combine(RootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "single-page"));
            var sourcePath2 = Path.GetFullPath(Path.Combine(RootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "common"));

            Directory.CreateDirectory(OutputPath);
            Directory.CreateDirectory(TemplatePath);

            foreach (string dirPath in Directory.GetDirectories(sourcePath1, "*", SearchOption.AllDirectories)) Directory.CreateDirectory(dirPath.Replace(sourcePath1, TemplatePath));
            foreach (string newPath in Directory.GetFiles(sourcePath1, "*.*", SearchOption.AllDirectories)) File.Copy(newPath, newPath.Replace(sourcePath1, TemplatePath), true);

            foreach (string dirPath in Directory.GetDirectories(sourcePath2, "*", SearchOption.AllDirectories)) Directory.CreateDirectory(dirPath.Replace(sourcePath2, TemplatePath));
            foreach (string newPath in Directory.GetFiles(sourcePath2, "*.*", SearchOption.AllDirectories)) File.Copy(newPath, newPath.Replace(sourcePath2, TemplatePath), true);

            try
            {
                using (var pres = new Presentation(PresentationFilePath))
                {
                    // Read the layouts and create from each layout a slide
                    for (int i = 0; i < pres.LayoutSlides.Count; i++)
                    {
                        // Add an empty slide to the presentation
                        pres.Slides.AddEmptySlide(pres.LayoutSlides[i]);
                    }
                    WebDocument document = pres.ToSinglePageWebDocument(
                        new WebDocumentOptions { },
                        TemplatePath,
                        OutputPath);
                    document.Save();
                }
            }
            catch(NullReferenceException ex)
            {
                Assert.Fail();
            }
        }
    }
}