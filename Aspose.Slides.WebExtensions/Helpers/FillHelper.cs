// Copyright (c) 2001-2020 Aspose Pty Ltd. All Rights Reserved.

using Aspose.Slides.Export.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Aspose.Slides.WebExtensions.Helpers
{
    public static class FillHelper
    {
        static string watermark = null;
        public static string GetLicStyle<T>(TemplateContext<T> model) 
        {
            if (new License().IsLicensed()) return "";

            if (string.IsNullOrEmpty(watermark))
            {
                using (Presentation p = new Presentation())
                {
                    MasterSlide slide = model.Object as MasterSlide;
                    p.SlideSize.SetSize(slide.Presentation.SlideSize.Size.Width, slide.Presentation.SlideSize.Size.Height, SlideSizeScaleType.DoNotScale);
                    ISlide empty = p.Slides[0];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Bitmap bmp = empty.GetThumbnail(1, 1);
                        bmp.MakeTransparent(Color.White);
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        ms.Position = 0;
                        watermark = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            return string.Format(
                "background-image: url('data:image/png;base64, {0}');position:absolute;width:100%;height:100%;content:'';display:block;z-index:10;background-repeat:no-repeat;", 
                watermark);
        }

        public static string GetFillStyle<T>(IFillFormatEffectiveData format, TemplateContext<T> model)
        {
            string result = "";
            switch (format.FillType)
            {
                case FillType.Solid:
                    result = string.Format("background-color: {0};", ColorHelper.GetRrbaColorString(format.SolidFillColor));
                    break;
                case FillType.Picture:
                    IPictureFillFormatEffectiveData picFillFormat = format.PictureFillFormat;
                    Shape asShape = model.Object as Shape;
                    if (asShape != null && picFillFormat.PictureFillMode == PictureFillMode.Tile)
                    {
                        Bitmap fillImage = ImageHelper.GetShapeFillImage(asShape, format);

                        var imagesPath = model.Global.Get<string>("imagesPath");
                        string path = Path.Combine(imagesPath, string.Format("tileimage{0}.png", asShape.UniqueId));
                        fillImage.Save(path);
                        //IOutputFile outputFile;
                        //if (!model.Output.Files.ContainsKey(path))
                        //    outputFile = model.Output.Add(path, fillImage);
                        //else 
                        //    outputFile = model.Output.Files[path];
                        //model.Output.BindResource(outputFile, fillImage);

                        // TODO: This ^^ solution is better than this one vv, but it works

                        var slidesPath = model.Global.Get<string>("slidesPath");
                        result = string.Format("background-image: url(\'{0}\');", ShapeHelper.ConvertPathToRelative(path, slidesPath));
                    }
                    else
                    {
                        var picture = format.PictureFillFormat.Picture;
                        IPPImage fillImage = picture.Image;
                        if (picture.ImageTransform.Count > 0)
                        {
                            Slide slide = model.Object as Slide;
                            if (slide != null)
                            {
                                using (Presentation pres = new Presentation())
                                {
                                    pres.SlideSize.SetSize(slide.Presentation.SlideSize.Type, SlideSizeScaleType.DoNotScale);
                                    pres.Slides.RemoveAt(0);
                                    Slide clone = (Slide)pres.Slides.AddClone(slide.Presentation.Slides[slide.SlideNumber - 1]);
                                    clone.Shapes.Clear();
                                    clone.LayoutSlide.MasterSlide.Shapes.Clear();
                                    var bckg = clone.GetThumbnail(1f, 1f);
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        bckg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                        ms.Flush();
                                        result = string.Format("background-image: url(\'data:image/png;base64, {0}\');", Convert.ToBase64String(ms.ToArray()));
                                    }
                                }
                            }
                            else
                            {
                                result = string.Format("background-image: url(\'{0}\');", ImageHelper.GetImageURL(fillImage, model));
                            }
                        }
                        else
                        {
                            result = string.Format("background-image: url(\'{0}\');", ImageHelper.GetImageURL(fillImage, model));
                        }
                    }
                    break;
                case FillType.Gradient:
                    result = string.Format("background: {0};", FillHelper.GetGradientFill(format.GradientFormat));
                    break;
                case FillType.Pattern:
                    result = string.Format("background: url(\'{0}\') repeat;", PatternGenerator.GetPatternImage(format.PatternFormat.PatternStyle, format.PatternFormat.ForeColor, format.PatternFormat.BackColor));
                    break;
            }

            return result;
        }
        private static string GetGradStops(IGradientFormatEffectiveData format) 
        {
            string gradStops = "";
            var gsList = new List<IGradientStopEffectiveData>();

            foreach (IGradientStopEffectiveData gradStop in format.GradientStops)  gsList.Add(gradStop);
            gsList.Sort((x, y) => ((x.Position - y.Position) < 0) ? -1 : (((x.Position - y.Position) > 0) ? 1 : 0));

            foreach (var gradStop in gsList)
            {
                gradStops += string.Format(", {0}", ColorHelper.GetRrbaColorString(gradStop.Color));
                if (gradStop.Position > 0 && gradStop.Position < 1)
                    gradStops += string.Format(" {0}%", gradStop.Position * 100);
            }
            return gradStops;
        }
        private static string GetGradientFill(IGradientFormatEffectiveData format)
        {
            string result = "";
            string gradStops = GetGradStops(format);

            if (format.GradientShape == GradientShape.Linear)
            {
                result = string.Format("linear-gradient({0}deg{1})", format.LinearGradientAngle + 90, gradStops);
            }
            else if (format.GradientShape == GradientShape.Radial)
            {
                string centerPosition = "";
                switch (format.GradientDirection)
                {
                    case GradientDirection.FromCorner1:
                        centerPosition = "top left";
                        break;
                    case GradientDirection.FromCorner2:
                        centerPosition = "top right";
                        break;
                    case GradientDirection.FromCorner3:
                        centerPosition = "bottom left";
                        break;
                    case GradientDirection.FromCorner4:
                        centerPosition = "bottom right";
                        break;
                    default:
                        centerPosition = "center";
                        break;
                }

                result = string.Format("radial-gradient(circle at {0}{1})", centerPosition, gradStops);
            }

            return result;
        }
    }
}