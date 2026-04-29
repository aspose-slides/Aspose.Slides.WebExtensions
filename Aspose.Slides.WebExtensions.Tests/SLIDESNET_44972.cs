using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aspose.Slides.WebExtensions.Tests
{
    [TestClass]
    public class SLIDESNET_44972
    {
        [TestMethod]
        public void Test_44972_Visual()
        {
            var rootDirectory = Path.GetFullPath("../../../");
            var presentationFilePath = Path.Combine(rootDirectory, "TestData", "SLIDESNET_44972", "SLIDESNET_44972.pptx");

            VisualRegressionTest
                .ForIssue("SLIDESNET_44972")
                .UsingPresentation(presentationFilePath)
                .UsingTemplateSet("single-page")
                .WithOutputName("SLIDESNET_44972")
                .WithWebDocumentOptions(options =>
                {
                    options.AnimateShapes = true;
                    options.AnimateTransitions = true;
                    options.EmbedImages = true;
                })
                .WithViewport(1280, 720)
                .Open("index.html").Wait(1200).Capture("01-slide-1-initial")
                .Click("#NextSlide").Wait(1200).Capture("02-slide-1-after-first-click")
                .Click("#NextSlide").Wait(1200).Capture("03-slide-1-after-second-click")
                .AssertMatchesBaseline();
        }
    }
}
