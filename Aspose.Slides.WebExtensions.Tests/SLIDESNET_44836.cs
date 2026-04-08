using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aspose.Slides.WebExtensions.Tests
{
    [TestClass]
    public class SLIDESNET_44836
    {
        [TestMethod]
        public void Test_Try1_RandomBarsTextAnimation_Visual()
        {
            string env_suff =
#if NET472
            "-net472";
#else
            "";
#endif
            var rootDirectory = Path.GetFullPath("../../../");
            var presentationFilePath = Path.Combine(rootDirectory, "TestData", "SLIDESNET_44836", "try1.pptx");

            VisualRegressionTest
                .ForIssue("SLIDESNET_44836")
                .UsingPresentation(presentationFilePath)
                .UsingTemplateSet("single-page")
                .WithOutputName("try1-randombars-text-animation")
                .WithWebDocumentOptions(options =>
                {
                    options.AnimateShapes = true;
                    options.AnimateTransitions = true;
                    options.EmbedImages = true;
                })
                .WithViewport(640, 480)
                .WithPixelTolerance(1000)
                // forward
                .Open("index.html").Wait(2000).Capture("01-slide-1" + env_suff)
                .Click("#slide-1").Wait(1200).Capture("02-slide-2-empty" + env_suff)
                .Click("#NextSlide").Wait(250).Capture("03-slide-2-paragraph-1-randombars" + env_suff).Wait(950).Capture("04-slide-2-paragraph-1" + env_suff)
                .Click("#NextSlide").Wait(250).Capture("05-slide-2-paragraph-2-randombars" + env_suff).Wait(950).Capture("06-slide-2-paragraph-2" + env_suff)
                .Click("#NextSlide").Wait(250).Capture("07-slide-2-paragraph-3-randombars" + env_suff).Wait(950).Capture("08-slide-2-paragraph-3" + env_suff)
                .Click("#NextSlide").Wait(250).Capture("09-slide-2-paragraph-4-randombars" + env_suff).Wait(950).Capture("10-slide-2-paragraph-4" + env_suff)
                // backward
                .Click("#PrevSlide").Wait(1200).Capture("11-slide-2-paragraph-3-p" + env_suff)
                .Click("#PrevSlide").Wait(1200).Capture("12-slide-2-paragraph-2-p" + env_suff)
                .Click("#PrevSlide").Wait(1200).Capture("13-slide-2-paragraph-1-p" + env_suff)
                .Click("#PrevSlide").Wait(1200).Capture("14-slide-2-empty-p" + env_suff)
                .Click("#PrevSlide").Wait(1200).Capture("15-slide-1-p" + env_suff)
                // check
                .AssertMatchesBaseline();
        }
    }
}
