using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aspose.Slides.WebExtensions.Tests
{
    [TestClass]
    public class SLIDESNET_45507
    {
        [TestMethod]
        [TestCategory("GitHubActions")]
        public void Test_45507_Visual()
        {
            var rootDirectory = Path.GetFullPath("../../../");
            var presentationFilePath = Path.Combine(rootDirectory, "TestData", "SLIDESNET_45507", "SLIDESNET_45507.pptx");

            VisualRegressionTest
                .ForIssue("SLIDESNET_45507")
                .UsingPresentation(presentationFilePath)
                .UsingTemplateSet("single-page")
                .WithOutputName("SLIDESNET_45507")
                .WithWebDocumentOptions(options =>
                {
                    options.EmbedImages = true;
                })
                .WithViewport(1280, 720)
                .WithPixelTolerance(8000)
                .Open("index.html").Wait(800).Capture("01-text-alignment")
                .AssertMatchesBaseline();
        }
    }
}

