using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Net.Mime.MediaTypeNames;

namespace Aspose.Slides.WebExtensions.Tests
{
    public class TestUtils
    {
        public delegate string CustomReplacement(string content);

        public static void CompareDir(string ethalonPath, string outputPath, CustomReplacement replacements, bool compareImagesByPixels = false)
        {
            int cnt = 0;
            string[] actualFiles = Directory.GetFiles(outputPath, "*", SearchOption.AllDirectories);
            string[] ethalonFiles = Directory.GetFiles(ethalonPath, "*", SearchOption.AllDirectories);

            foreach (string actualFile in actualFiles)
            {
                foreach (string ethalonFile in ethalonFiles)
                {
                    if (Path.GetFileName(actualFile) == Path.GetFileName(ethalonFile))
                    {
                        cnt++;
                        CompareFiles(ethalonFile, actualFile, replacements, compareImagesByPixels);
                    }
                }
            }
            Assert.AreEqual(ethalonFiles.Length, cnt);
        }
        private static void CompareFiles(string ethalonFile, string actualFile, CustomReplacement replacements, bool compareImagesByPixels = false)
        {
            byte[] ethalon = File.ReadAllBytes(ethalonFile); ;
            byte[] actual = File.ReadAllBytes(actualFile); ;
            string ext = Path.GetExtension(ethalonFile);
            if (ext == ".html" || ext == ".js" || ext == ".css")
            {
                ethalon = ReadMarkupedFile(ethalonFile, replacements);
                actual = ReadMarkupedFile(actualFile, replacements);
            }
            if (compareImagesByPixels && ext == ".png")
            {
                if (ethalonFile.Contains("thumbnail")) return;
                ethalon = ReadImageFile(ethalonFile);
                actual = ReadImageFile(actualFile);
                ComparePixels(ethalon, actual);
            }
            else
                CompareBytes(ethalon, actual);
        }

        private static byte[] ReadImageFile(string filename)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bmp = new Bitmap(filename))
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
        }
        private static byte[] ReadMarkupedFile(string filename, CustomReplacement replacements)
        {
            string content = File.ReadAllText(filename);

            content = content.Replace("\r", " ");
            content = content.Replace("\n", " ");
            content = content.Replace("\t", " ");

            while (content.IndexOf("  ") >= 0)
                content = content.Replace("  ", " ");

            content = content.Replace(" <", "<");
            content = content.Replace("> ", ">");
            content = content.Replace("><", ">\n<");
            content = content.Replace("; \">", ";\">");

            content = replacements(content);

            //File.WriteAllText(filename + ".tst", content);
            return Encoding.UTF8.GetBytes(content);
        }
        private static void CompareBytes(byte[] ethalon, byte[] actual)
        {
            Assert.AreEqual(ethalon.Length, actual.Length);
            for (int i = 0; i < ethalon.Length; i++)
            {
                Assert.AreEqual(ethalon[i], actual[i], "at position {0}", i);
            }
        }
        private static void ComparePixels(byte[] ethalon, byte[] actual)
        {
            using (Bitmap eth = new Bitmap(new MemoryStream(ethalon)))
            {
                using (Bitmap act = new Bitmap(new MemoryStream(actual)))
                {
                    for (int y = 1; y < eth.Height - 1; y++)
                        for (int x = 1; x < eth.Width - 1; x++)
                            Assert.AreEqual(eth.GetPixel(x, y), act.GetPixel(x, y));
                }
            }
        }
    }
}