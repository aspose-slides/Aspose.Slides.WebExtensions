// Copyright (c) 2001-2025 Aspose Pty Ltd. All Rights Reserved.

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Aspose.Slides.WebExtensions.Helpers
{
    internal static class MetafileRasterizer
    {
        internal static byte[] RasterizeToPng(IPPImage image)
        {
            using (MemoryStream source = new MemoryStream(image.BinaryData))
            using (Metafile metafile = (Metafile)Image.FromStream(source))
            using (Bitmap bitmap = new Bitmap(metafile.Width, metafile.Height))
            using (MemoryStream output = new MemoryStream())
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.Transparent);
                    graphics.SmoothingMode = SmoothingMode.None;
                    graphics.DrawImage(metafile, 0, 0, metafile.Width, metafile.Height);
                }

                bitmap.Save(output, System.Drawing.Imaging.ImageFormat.Png);
                return output.ToArray();
            }
        }
    }
}
