using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aspose.Slides.WebExtensions.Tests
{
    [TestClass]
    public class SLIDESNET_44630
    {
        [TestMethod]
        public void Test_SamplePPT1_AnimationAwareSlideNavigation_Visual()
        {
            string env_suff =
#if NET472
            "-net472";
#else
            "";
#endif
            var rootDirectory = Path.GetFullPath("../../../");
            var presentationFilePath = Path.Combine(rootDirectory, "TestData", "SLIDESNET_43877", "SamplePPT1.ppt");

            VisualRegressionTest
                .ForIssue("SLIDESNET_44630")
                .UsingPresentation(presentationFilePath)
                .UsingTemplateSet("single-page")
                .WithOutputName("SamplePPT1-animation-aware-slide-navigation")
                .WithWebDocumentOptions(options =>
                {
                    options.AnimateShapes = true;
                    options.AnimateTransitions = true;
                    options.EmbedImages = true;
                })
                .WithViewport(1280, 720)
                //forward
                .Open("index.html").Wait(1200).Capture("01-slide-1-empty-n")
                .Click("#NextSlide").Wait(1200).Capture("02-slide-1-title-n")
                .Click("#NextSlide").Wait(1200).Capture("03-slide-1-title-content-n")
                .Click("#NextSlide").Wait(1200).Capture("04-slide-2-empty-n")
                .Click("#NextSlide").Wait(1200).Capture("05-slide-2-title-n")
                .Click("#NextSlide").Wait(1200).Capture("06-slide-2-title-chart-n" + env_suff)
                .Click("#NextSlide").Wait(1200).Capture("07-slide-3-empty-n")
                .Click("#NextSlide").Wait(1200).Capture("08-slide-3-title-n")
                .Click("#NextSlide").Wait(1200).Capture("09-slide-3-title-table-n")
                //backward
                .Click("#PrevSlide").Wait(1200).Capture("08-slide-3-title-p")
                .Click("#PrevSlide").Wait(1200).Capture("07-slide-3-empty-p")
                .Click("#PrevSlide").Wait(1200).Capture("06-slide-2-title-chart-p" + env_suff)
                .Click("#PrevSlide").Wait(1200).Capture("05-slide-2-title-p")
                .Click("#PrevSlide").Wait(1200).Capture("04-slide-2-empty-p")
                .Click("#PrevSlide").Wait(1200).Capture("03-slide-1-title-content-p")
                .Click("#PrevSlide").Wait(1200).Capture("02-slide-1-title-p")
                .Click("#PrevSlide").Wait(1200).Capture("01-slide-1-empty-p")
                //check
                .AssertMatchesBaseline();
        }
    }
}
