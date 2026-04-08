using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Slides;
using Aspose.Slides.Export.Web;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aspose.Slides.WebExtensions.Tests
{
    internal sealed class VisualRegressionTest
    {
        private readonly List<VisualRegressionStep> _steps = new List<VisualRegressionStep>();

        public string IssueId { get; private set; }

        public string PresentationFilePath { get; private set; }

        public string TemplateSet { get; private set; }

        public string OutputName { get; private set; }

        public VisualWebDocumentOptions WebDocumentOptions { get; } = new VisualWebDocumentOptions();

        public int ViewportWidth { get; private set; } = 1280;

        public int ViewportHeight { get; private set; } = 720;

        public int PixelTolerance { get; private set; }

        public string EntryPoint { get; private set; }

        private VisualRegressionTest()
        {
        }

        public static VisualRegressionTest ForIssue(string issueId)
        {
            return new VisualRegressionTest
            {
                IssueId = issueId
            };
        }

        public VisualRegressionTest UsingPresentation(string presentationFilePath)
        {
            PresentationFilePath = presentationFilePath;
            return this;
        }

        public VisualRegressionTest UsingTemplateSet(string templateSet)
        {
            TemplateSet = templateSet;
            return this;
        }

        public VisualRegressionTest WithOutputName(string outputName)
        {
            OutputName = outputName;
            return this;
        }

        public VisualRegressionTest WithWebDocumentOptions(Action<VisualWebDocumentOptions> configure)
        {
            configure?.Invoke(WebDocumentOptions);
            return this;
        }

        public VisualRegressionTest WithViewport(int width, int height)
        {
            ViewportWidth = width;
            ViewportHeight = height;
            return this;
        }

        public VisualRegressionTest WithPixelTolerance(int pixelTolerance)
        {
            PixelTolerance = pixelTolerance;
            return this;
        }

        public VisualRegressionTest Open(string entryPoint)
        {
            EntryPoint = entryPoint;
            _steps.Add(new VisualRegressionStep(VisualRegressionStepType.Open, entryPoint));
            return this;
        }

        public VisualRegressionTest Click(string selector)
        {
            _steps.Add(new VisualRegressionStep(VisualRegressionStepType.Click, selector));
            return this;
        }

        public VisualRegressionTest Wait(int milliseconds)
        {
            _steps.Add(new VisualRegressionStep(VisualRegressionStepType.Wait, milliseconds.ToString()));
            return this;
        }

        public VisualRegressionTest Capture(string snapshotName)
        {
            _steps.Add(new VisualRegressionStep(VisualRegressionStepType.Capture, snapshotName));
            return this;
        }

        public void AssertMatchesBaseline()
        {
            AssertMatchesBaselineAsync().GetAwaiter().GetResult();
        }

        private async Task AssertMatchesBaselineAsync()
        {
            Validate();

            var rootDirectory = Path.GetFullPath("../../../");
            var visualRootPath = Path.Combine(rootDirectory, "TestData", IssueId, "visual", OutputName);
            var baselinePath = Path.Combine(visualRootPath, "baseline");
            var actualPath = Path.Combine(rootDirectory, "TestData", "Out", IssueId, OutputName, "actual");
            var diffPath = Path.Combine(rootDirectory, "TestData", "Out", IssueId, OutputName, "diff");
            var expectedPath = Path.Combine(rootDirectory, "TestData", "Out", IssueId, OutputName, "expected");

            ResetDirectory(actualPath);
            ResetDirectory(diffPath);
            ResetDirectory(expectedPath);
            Directory.CreateDirectory(baselinePath);

            var exportOutputPath = PrepareExportOutput(rootDirectory);
            var updateBaselines = string.Equals(Environment.GetEnvironmentVariable("UPDATE_VISUAL_BASELINES"), "1", StringComparison.Ordinal);

            IPlaywright playwright = null;
            IBrowser browser = null;
            IBrowserContext context = null;

            try
            {
                playwright = await Playwright.CreateAsync();
                browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = true
                });
                context = await browser.NewContextAsync(new BrowserNewContextOptions
                {
                    ViewportSize = new ViewportSize
                    {
                        Width = ViewportWidth,
                        Height = ViewportHeight
                    }
                });

                var page = await context.NewPageAsync();
                var openStep = _steps.First(s => s.Type == VisualRegressionStepType.Open);
                var entryFilePath = Path.Combine(exportOutputPath, openStep.Value);
                await page.GotoAsync(new Uri(entryFilePath).AbsoluteUri, new PageGotoOptions
                {
                    WaitUntil = WaitUntilState.NetworkIdle
                });

                var failures = new List<string>();

                foreach (var step in _steps)
                {
                    switch (step.Type)
                    {
                        case VisualRegressionStepType.Open:
                            break;
                        case VisualRegressionStepType.Click:
                            await page.ClickAsync(step.Value);
                            break;
                        case VisualRegressionStepType.Wait:
                            await page.WaitForTimeoutAsync(int.Parse(step.Value));
                            break;
                        case VisualRegressionStepType.Capture:
                            var actualScreenshotPath = Path.Combine(actualPath, step.Value + ".png");
                            var baselineScreenshotPath = Path.Combine(baselinePath, step.Value + ".png");
                            var expectedScreenshotPath = Path.Combine(expectedPath, step.Value + ".png");
                            var diffScreenshotPath = Path.Combine(diffPath, step.Value + ".png");

                            await page.ScreenshotAsync(new PageScreenshotOptions
                            {
                                Path = actualScreenshotPath,
                                FullPage = false
                            });

                            if (updateBaselines)
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(baselineScreenshotPath));
                                File.Copy(actualScreenshotPath, baselineScreenshotPath, true);
                                continue;
                            }

                            if (!File.Exists(baselineScreenshotPath))
                            {
                                failures.Add("Missing baseline screenshot: " + baselineScreenshotPath);
                                continue;
                            }

                            File.Copy(baselineScreenshotPath, expectedScreenshotPath, true);

                            if (!VisualImageComparer.ComparePng(baselineScreenshotPath, actualScreenshotPath, diffScreenshotPath, out var differenceCount))
                            {
                                if (differenceCount <= PixelTolerance)
                                {
                                    if (File.Exists(diffScreenshotPath))
                                        File.Delete(diffScreenshotPath);

                                    continue;
                                }

                                failures.Add(
                                    $"Screenshot '{step.Value}' differs from baseline. " +
                                    $"Different pixels: {differenceCount}. " +
                                    $"Expected: {baselineScreenshotPath}. Actual: {actualScreenshotPath}. Diff: {diffScreenshotPath}.");
                            }
                            break;
                        default:
                            throw new InvalidOperationException("Unknown step type: " + step.Type);
                    }
                }

                if (updateBaselines)
                {
                    Assert.Inconclusive("Visual baselines were updated. Re-run the test without UPDATE_VISUAL_BASELINES=1 to validate them.");
                }

                if (failures.Count > 0)
                {
                    Assert.Fail(string.Join(Environment.NewLine, failures));
                }
            }
            catch (PlaywrightException ex) when (ex.Message.IndexOf("Executable doesn't exist", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Assert.Fail(
                    "Playwright browser is not installed. " +
                    "Build the test project and run the generated playwright install script, for example " +
                    "'pwsh .\\bin\\Debug\\net6.0\\playwright.ps1 install chromium'. " +
                    "Original error: " + ex.Message);
            }
            finally
            {
                if (context != null)
                    await context.DisposeAsync();
                if (browser != null)
                    await browser.DisposeAsync();
                if (playwright != null)
                    playwright.Dispose();
            }
        }

        private string PrepareExportOutput(string rootDirectory)
        {
            var templateRootPath = Path.Combine(rootDirectory, "TestData", "Out", "templates-" + IssueId + "-" + OutputName);
            var exportOutputPath = Path.Combine(rootDirectory, "TestData", "Out", IssueId, OutputName, "export");

            ResetDirectory(templateRootPath);
            ResetDirectory(exportOutputPath);

            CopyTemplates(rootDirectory, templateRootPath);

            using (var presentation = new Presentation(PresentationFilePath))
            {
                var options = new WebDocumentOptions
                {
                    AnimateShapes = WebDocumentOptions.AnimateShapes,
                    AnimateTransitions = WebDocumentOptions.AnimateTransitions,
                    EmbedImages = WebDocumentOptions.EmbedImages
                };

                switch (TemplateSet)
                {
                    case "single-page":
                        var document = presentation.ToSinglePageWebDocument(options, templateRootPath, exportOutputPath);
                        document.Save();
                        break;
                    default:
                        throw new NotSupportedException("Unsupported template set: " + TemplateSet);
                }
            }

            return exportOutputPath;
        }

        private void CopyTemplates(string rootDirectory, string templateRootPath)
        {
            string sourcePath1;
            switch (TemplateSet)
            {
                case "single-page":
                    sourcePath1 = Path.GetFullPath(Path.Combine(rootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "single-page"));
                    break;
                default:
                    throw new NotSupportedException("Unsupported template set: " + TemplateSet);
            }

            var sourcePath2 = Path.GetFullPath(Path.Combine(rootDirectory, "..", "Aspose.Slides.WebExtensions", "templates", "common"));

            Directory.CreateDirectory(templateRootPath);

            foreach (string dirPath in Directory.GetDirectories(sourcePath1, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath1, templateRootPath));
            foreach (string newPath in Directory.GetFiles(sourcePath1, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath1, templateRootPath), true);

            foreach (string dirPath in Directory.GetDirectories(sourcePath2, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath2, templateRootPath));
            foreach (string newPath in Directory.GetFiles(sourcePath2, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath2, templateRootPath), true);
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(IssueId))
                throw new InvalidOperationException("IssueId is not configured.");
            if (string.IsNullOrEmpty(PresentationFilePath))
                throw new InvalidOperationException("Presentation file is not configured.");
            if (string.IsNullOrEmpty(TemplateSet))
                throw new InvalidOperationException("Template set is not configured.");
            if (string.IsNullOrEmpty(OutputName))
                throw new InvalidOperationException("Output name is not configured.");
            if (string.IsNullOrEmpty(EntryPoint))
                throw new InvalidOperationException("Entry point is not configured.");
            if (_steps.Count == 0)
                throw new InvalidOperationException("No visual regression steps were configured.");
            if (_steps.All(s => s.Type != VisualRegressionStepType.Capture))
                throw new InvalidOperationException("At least one capture step must be configured.");
        }

        private static void ResetDirectory(string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);

            Directory.CreateDirectory(path);
        }
    }

    internal sealed class VisualWebDocumentOptions
    {
        public bool AnimateShapes { get; set; }

        public bool AnimateTransitions { get; set; }

        public bool EmbedImages { get; set; }
    }

    internal sealed class VisualRegressionStep
    {
        public VisualRegressionStep(VisualRegressionStepType type, string value)
        {
            Type = type;
            Value = value;
        }

        public VisualRegressionStepType Type { get; }

        public string Value { get; }
    }

    internal enum VisualRegressionStepType
    {
        Open,
        Click,
        Wait,
        Capture
    }

    internal static class VisualImageComparer
    {
        public static bool ComparePng(string expectedPath, string actualPath, string diffPath, out int differenceCount)
        {
            using (var expected = new Bitmap(expectedPath))
            using (var actual = new Bitmap(actualPath))
            {
                if (expected.Width != actual.Width || expected.Height != actual.Height)
                {
                    differenceCount = int.MaxValue;
                    using (var diff = new Bitmap(Math.Max(expected.Width, actual.Width), Math.Max(expected.Height, actual.Height)))
                    using (var graphics = Graphics.FromImage(diff))
                    {
                        graphics.Clear(Color.Magenta);
                        diff.Save(diffPath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    return false;
                }

                differenceCount = 0;
                using (var diffImage = new Bitmap(expected.Width, expected.Height))
                {
                    for (var y = 0; y < expected.Height; y++)
                    {
                        for (var x = 0; x < expected.Width; x++)
                        {
                            var expectedPixel = expected.GetPixel(x, y);
                            var actualPixel = actual.GetPixel(x, y);

                            if (expectedPixel.ToArgb() == actualPixel.ToArgb())
                            {
                                diffImage.SetPixel(x, y, expectedPixel);
                                continue;
                            }

                            differenceCount++;
                            diffImage.SetPixel(x, y, Color.Magenta);
                        }
                    }

                    if (differenceCount > 0)
                    {
                        diffImage.Save(diffPath, System.Drawing.Imaging.ImageFormat.Png);
                        return false;
                    }
                }

                if (File.Exists(diffPath))
                    File.Delete(diffPath);

                return true;
            }
        }
    }
}
